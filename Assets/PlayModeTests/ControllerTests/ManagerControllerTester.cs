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

public class ManagerControllerTester
{
    private IGameData _data;
    private IStorageManager _storage;
    private IImprovementView _view;
    private IBoard _board;
    private IPresenterFactory<IManager> _factory;
    private ImprovementData _improvementData;
    private ImprovementContainer _improvementSprites;
    private IImprovementController<ManagerPresenter> _controller;

    [SetUp] public void CreateTestObjects()
    {
        _data = Substitute.For<IGameData>();
        _storage = Substitute.For<IStorageManager>();
        _view = Substitute.For<IImprovementView>();
        _board = Substitute.For<IBoard>();
        _factory = Substitute.For<IPresenterFactory<IManager>>();
        _improvementData = ScriptableObject.CreateInstance<ImprovementData>();
        _improvementSprites = ScriptableObject.CreateInstance<ImprovementContainer>();

        var builder = new ContainerBuilder();
        builder.RegisterInstance(_data).As<IGameData>();
        builder.RegisterInstance(_storage).As<IStorageManager>();
        builder.RegisterInstance(_view).As<IImprovementView>();
        builder.RegisterInstance(_board).As<IBoard>();
        builder.Register<ManagerFactory>(Lifetime.Scoped).As<IPresenterFactory<IManager>>();
        builder.RegisterInstance(_improvementData).AsSelf();
        builder.RegisterInstance(_improvementSprites).AsSelf();
        builder.Register<ManagerController>(Lifetime.Scoped).As<IImprovementController<ManagerPresenter>>();

        var container = builder.Build();
        _controller = container.Resolve<IImprovementController<ManagerPresenter>>();
    }

    [Test] public void Initialize_AnyArg_SetsData()
    {
        _controller.Initialize();
        _storage.Received(1).Load(ref Arg.Any<object>(), Arg.Any<string>());
    }

    [Test] public void Unlock_ActivatedCell_SetsUnlock()
    {
        var model = new ImprovementModel();
        var presenter = new ManagerPresenter(model, _view);
        _controller.FindImprovement(Arg.Any<int>()).Returns(presenter);
        _controller.Unlock(Arg.Any<int>(), true);
        Assert.AreEqual(ImprovementState.Unlock, presenter.Model.State);
    }

    [Test] public void Unlock_NotActivatedCell_SetsLock()
    {
        // var model = new ImprovementModel();
        // model.Level = 1;
        // var presenter = new ManagerPresenter(model, _view);
        // _factory.Create(Arg.Any<IModel>(), Arg.Any<IView>()).Returns(presenter);

        _improvementData.BusinessNames = Substitute.For<BusinessData>();
        // _improvementData.BusinessNames.Names.Add(Arg.Any<string>());
        // _improvementData.ManagerNames.Add(Arg.Any<string>());
        // _improvementData.ManagerBaseCost.Add(Arg.Any<MoneyModel>());
        // _improvementSprites.ManagerSprites.Add(Arg.Any<Sprite>());
        _improvementData.BusinessNames.Names.Add(string.Empty);
        _improvementData.ManagerNames.Add(string.Empty);
        _improvementData.ManagerBaseCost.Add(new MoneyModel(2.0f));
        _improvementSprites.ManagerSprites.Add(Substitute.For<Sprite>());
        // var data = new GameDataModel();
        // data.MaxLevel = 1;
        // _data.GameData.Returns(new GameDataModel());

        _controller.Initialize();
        var presenter = _controller.FindImprovement(1);
        _controller.Unlock(1, false);
        Assert.AreEqual(ImprovementState.Lock, presenter.Model.State);
    }

    [Test] public void Dispose_AnyArg_CallsSave()
    {
        _controller.Dispose();
        _storage.Received(1).Save(Arg.Any<string>(), Arg.Any<object>());
    }
}
