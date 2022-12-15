using NSubstitute;
using NUnit.Framework;
using ShopTown.ControllerComponent;
using ShopTown.Data;
using ShopTown.PresenterComponent;
using ShopTown.ViewComponent;
using UnityEngine;
using VContainer;

public class GameBoardControllerTester
{
    private IGameData _data;
    private IStorageManager _storage;
    private IBoard _board;
    private IPresenterFactory<IGameCell> _factory;
    private IGameCellView _view;
    private BoardData _defaultData;
    private IGameBoardController _controller;

    [SetUp] public void CreateTestObjects()
    {
        _data = Substitute.For<IGameData>();
        _storage = Substitute.For<IStorageManager>();
        _board = Substitute.For<IBoard>();
        _factory = Substitute.For<IPresenterFactory<IGameCell>>();
        _view = Substitute.For<IGameCellView>();
        _defaultData = ScriptableObject.CreateInstance<BoardData>();

        var builder = new ContainerBuilder();
        builder.RegisterInstance(_data).As<IGameData>();
        builder.RegisterInstance(_storage).As<IStorageManager>();
        builder.RegisterInstance(_board).As<IBoard>();
        builder.RegisterInstance(_factory).As<IPresenterFactory<IGameCell>>();
        builder.RegisterInstance(_view).As<IGameCellView>();
        builder.RegisterInstance(_defaultData).AsSelf();
        builder.Register<GameBoardController>(Lifetime.Scoped).As<IGameBoardController>();

        var container = builder.Build();
        _controller = container.Resolve<IGameBoardController>();
    }

    [Test] public void Initialize_AnyArg_SetsData()
    {
        _controller.Initialize();
        _storage.Received(1).Load(ref Arg.Any<object>(), Arg.Any<string>());
    }

    [Test] public void Initialize_ByCallingOfCreateBoard_InvokesSetOfflineProfitEvent()
    {
        var wasCalled = false;
        _controller.SetOfflineProfitEvent += () => wasCalled = true;
        _controller.Initialize();
        Assert.True(wasCalled);
    }

    [Test] public void Dispose_AnyArg_CallsSave()
    {
        _controller.Dispose();
        _storage.Received(1).Save(Arg.Any<string>(), Arg.Any<object>());
    }
}
