using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using ShopTown.ControllerComponent;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using ShopTown.SpriteContainer;
using ShopTown.ViewComponent;
using UnityEngine;
using VContainer;

public class UpgradeControllerTester
{
    private IGameData _data;
    private IStorageManager _storage;
    private IImprovementView _view;
    private IButtonSubscriber _subscriber;
    private IBoard _board;
    private IPresenterFactory<IUpgrade> _factory;
    private IImprovementData _improvementData;
    private IImprovementSprites _improvementSprites;
    private IImprovementController<UpgradePresenter> _controller;

    [SetUp] public void CreateTestObjects()
    {
        CreateSubstitutes();
        TuneSubstitutes();
        CreateContainer();
    }

    private void CreateSubstitutes()
    {
        _data = Substitute.For<IGameData>();
        _storage = Substitute.For<IStorageManager>();
        _view = Substitute.For<IImprovementView>();
        _subscriber = Substitute.For<IButtonSubscriber>();
        _board = Substitute.For<IBoard>();
        _factory = Substitute.For<IPresenterFactory<IUpgrade>>();
        _improvementData = Substitute.For<IImprovementData>();
        _improvementSprites = Substitute.For<IImprovementSprites>();
    }

    private void TuneSubstitutes()
    {
        var data = Substitute.For<GameDataModel>();
        data.MaxLevel = 1;
        _data.GameData.Returns(data);
        var presenter = Substitute.For<IUpgrade>();
        presenter.SetState(Arg.Do<ImprovementState>(x => presenter.Model.State = x));
        var model = Substitute.For<ImprovementModel>();
        model.Level = 1;
        presenter.Model.Returns(model);
        _factory.Create(Arg.Any<IModel>(), Arg.Any<IView>()).Returns(presenter);

        _improvementData.GetDefaultModel().Returns(new ImprovementModel());
        _improvementData.GetBusinessName(Arg.Any<int>()).Returns("name");
        _improvementData.GetManagerName(Arg.Any<int>()).Returns("name");
        _improvementData.GetManagerCost(Arg.Any<int>()).Returns(new MoneyModel(2));
        _improvementSprites.GetManagerSprites(Arg.Any<int>())
            .Returns(Sprite.Create(Texture2D.blackTexture, Rect.zero, Vector2.one));
    }

    private void CreateContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterInstance(_data).As<IGameData>();
        builder.RegisterInstance(_storage).As<IStorageManager>();
        builder.RegisterInstance(_view).As<IImprovementView>();
        builder.RegisterInstance(_subscriber).As<IButtonSubscriber>();
        builder.RegisterInstance(_board).As<IBoard>();
        builder.RegisterInstance(_factory).As<IPresenterFactory<IUpgrade>>();
        builder.RegisterInstance(_improvementData).As<IImprovementData>();
        builder.RegisterInstance(_improvementSprites).As<IImprovementSprites>();
        builder.Register<UpgradeController>(Lifetime.Scoped).As<IImprovementController<UpgradePresenter>>();

        var container = builder.Build();
        _controller = container.Resolve<IImprovementController<UpgradePresenter>>();
    }

    [Test] public void Initialize_AnyArg_CallsLoad()
    {
        _controller.Initialize();
        _storage.Received(1).Load<List<ImprovementModel>>(Arg.Any<string>());
    }

    [Test] public void Initialize_ByInvokingChangeEvent_CallsSave()
    {
        _controller.Initialize();
        var presenter = _controller.FindImprovement(1);
        presenter.ChangeEvent += Raise.Event<Action>();
        _storage.Received(2).Save(Arg.Any<string>(), Arg.Any<object>());
    }

    [Test] public void Initialize_ByCreateDefaultModels_CallsSave()
    {
        _controller.Initialize();
        _storage.Received(1).Save(Arg.Any<string>(), Arg.Any<object>());
    }

    [Test] public void Initialize_ByCreateBoard_CallsInstantiate()
    {
        _controller.Initialize();
        _view.Received(_data.GameData.MaxLevel).Instantiate(Arg.Any<Transform>());
    }

    [Test] public void Initialize_ByCreateBoard_CallsCreate()
    {
        _controller.Initialize();
        _factory.Received(_data.GameData.MaxLevel).Create(Arg.Any<IModel>(), Arg.Any<IView>());
    }

    [Test] public void FindImprovement_WithAnyLevel_ReturnsImprovement()
    {
        _controller.Initialize();
        var improvement = _controller.FindImprovement(1);
        Assert.NotNull(improvement);
    }

    [Test] public void Unlock_ActivatedCell_SetsUnlock()
    {
        _controller.Initialize();
        var presenter = _controller.FindImprovement(1);
        _controller.UnlockImprovement(1, true);
        Assert.AreEqual(ImprovementState.Unlock, presenter.Model.State);
    }

    [Test] public void Unlock_NotActivatedCell_SetsLock()
    {
        _controller.Initialize();
        var presenter = _controller.FindImprovement(1);
        _controller.UnlockImprovement(1, false);
        Assert.AreEqual(ImprovementState.Lock, presenter.Model.State);
    }

    [Test] public void Dispose__CallsSave()
    {
        _controller.Dispose();
        _storage.Received(1).Save(Arg.Any<string>(), Arg.Any<object>());
    }
}
