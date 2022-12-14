using System;
using NSubstitute;
using NUnit.Framework;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using ShopTown.ViewComponent;
using UnityEngine;
using UnityEngine.UI;

public class GameCellPresenterTester
{
    private GameCellModel _model;
    private IGameCellView _view;
    private BoardData _boardData;
    private IGameCell _presenter;
    private Action _testCallBack;

    [SetUp] public void CreateTestObjects()
    {
        _model = Substitute.For<GameCellModel>();
        _view = Substitute.For<IGameCellView>();
        _boardData = ScriptableObject.CreateInstance<BoardData>();
        _presenter = new GameCellPresenter(_model, _view);
    }

    [Test] public void UnlockingTest()
    {
        _presenter.SetState(CellState.Unlock, _boardData);
        Assert.AreEqual(CellState.Unlock, _presenter.Model.State);
    }

    [Test] public void ActivationTest()
    {
        _presenter.SetState(CellState.Active, _boardData);
        Assert.AreEqual(CellState.Active, _presenter.Model.State);
    }

    [Test] public void GetInProgressByClickTest()
    {
        _presenter.Model.StartTime = DateTime.MaxValue;
        _presenter.SetState(CellState.InProgress, _boardData);
        Assert.AreEqual(CellState.InProgress, _presenter.Model.State);
    }

    private void PrepareGetInProgressCaseTest(bool isMangerActivated, int timeMultiplier = 1)
    {
        _presenter.Model.Level = 1;
        _presenter.Model.IsManagerActivated = isMangerActivated;
        _boardData.BaseProfit.Add(new MoneyModel(1));
        _boardData.ProcessTime.Add(new TimeModel(0, 0, 0, 40));
        var processTime =
            TimeSpan.FromSeconds(_boardData.ProcessTime[_presenter.Model.Level - 1].Seconds * timeMultiplier);

        _presenter.Model.StartTime = DateTime.Now.Subtract(processTime);
    }

    [Test] public void GetInProgressNotActivatedManagerCaseTest()
    {
        var profit = new MoneyModel(0);
        PrepareGetInProgressCaseTest(false);

        _presenter.GetOfflineProfitEvent += (offlineProfit) => profit.Number = offlineProfit.Number;
        _presenter.SetState(CellState.InProgress, _boardData);

        Assert.AreEqual(_presenter.Model.Profit.Number, profit.Number);
        Assert.AreEqual(CellState.Active, _presenter.Model.State);
    }

    [Test] public void GetInProgressActivatedManagerCaseTest()
    {
        var multiplier = 3;
        var profit = new MoneyModel(0);
        PrepareGetInProgressCaseTest(true, multiplier);

        _presenter.GetOfflineProfitEvent += (offlineProfit) => profit.Number = offlineProfit.Number;
        _presenter.SetState(CellState.InProgress, _boardData);

        Assert.AreEqual(_presenter.Model.Profit.Number * multiplier, profit.Number);
        Assert.AreEqual(CellState.InProgress, _presenter.Model.State);
    }

    [Test] public void SettingParametersTest()
    {
        _boardData.BaseProfit.Add(new MoneyModel(1));
        _boardData.ProcessTime.Add(new TimeModel(0, 0, 0, 1));
        _boardData.Cost.Add(new MoneyModel(1));

        _presenter.Model.Level = 1;
        _presenter.SetState(CellState.Active, _boardData);
        _presenter.SetCost(0, _boardData);

        Assert.AreEqual(1, _presenter.Model.Profit.Number);
        Assert.AreEqual(1, _presenter.Model.TotalTime.TotalSeconds);
        Assert.AreEqual(1, _presenter.Model.Cost.Number);

        _presenter.SetCost(1, _boardData);
        Assert.AreEqual(2, _presenter.Model.Cost.Number);
    }

    [Test] public void LevelUpTest()
    {
        _presenter.Model.Level = -1;
        _presenter.LevelUp(_boardData);
        Assert.AreEqual(0, _presenter.Model.Level);
    }

    [Test] public void InitializingNotActivatedManagerTest()
    {
        var manager = Substitute.For<ImprovementModel>();
        manager.IsActivated = false;
        PrepareGetInProgressCaseTest(true);
        _presenter.Model.State = CellState.Active;
        _presenter.InitializeManager(manager, _boardData);
        Assert.AreEqual(false, _presenter.Model.IsManagerActivated);
        Assert.AreEqual(CellState.Active, _presenter.Model.State);
    }

    [Test] public void InitializingActivatedManagerTest()
    {
        var manager = Substitute.For<ImprovementModel>();
        manager.IsActivated = true;
        _presenter.Model.State = CellState.Active;
        _presenter.InitializeManager(manager, _boardData);
        Assert.AreEqual(true, _presenter.Model.IsManagerActivated);
        Assert.AreEqual(CellState.InProgress, _presenter.Model.State);
    }

    [Test] public void InitializingNotActivatedUpgradeTest()
    {
        var upgrade = Substitute.For<ImprovementModel>();
        upgrade.ImprovementLevel = 2;
        _presenter.InitializeUpgrade(upgrade, _boardData);
        Assert.AreEqual(1, _presenter.Model.ActivatedUpgradeLevel);
    }

    [Test] public void InitializingActivatedUpgradeTest()
    {
        var upgrade = Substitute.For<ImprovementModel>();
        upgrade.ImprovementLevel = 3;
        upgrade.IsActivated = true;
        _presenter.InitializeUpgrade(upgrade, _boardData);
        Assert.AreEqual(3, _presenter.Model.ActivatedUpgradeLevel);
        Assert.AreEqual(true, _presenter.Model.AreAllUpgradeLevelsActivated);
    }

    [Test] public void SubscriptionToBuyButtonTest()
    {
        _presenter.Model.State = CellState.Unlock;
        var button = Substitute.For<Button>();
        _view.GetBuyButton().Returns(button);
        _presenter.SubscribeToBuyButton((cell) => cell.Model.State = CellState.Active);
        _view.GetBuyButton().onClick?.Invoke();
        Assert.AreEqual(CellState.Active, _presenter.Model.State);
    }

    [Test] public void SubscriptionToCellButtonTest()
    {
        _presenter.Model.State = CellState.Unlock;
        var button = Substitute.For<Button>();
        _view.GetCellButton().Returns(button);
        _presenter.SubscribeToCellClick((cell) => cell.Model.State = CellState.Active);
        _view.GetCellButton().onClick?.Invoke();
        Assert.AreEqual(CellState.Active, _presenter.Model.State);
    }

    [Test] public void SuccessfulCheckingOfNeighborTest()
    {
        var model = Substitute.For<GameCellModel>();
        var view = Substitute.For<IGameCellView>();
        var presenter = new GameCellPresenter(model, view);
        presenter.Model.GridIndex = new[] {1, 1};
        _presenter.Model.GridIndex = new[] {1, 2};
        Assert.AreEqual(true, _presenter.IsNeighborOf(presenter));
    }

    [Test] public void FailedCheckingOfNeighborTest()
    {
        var model = Substitute.For<GameCellModel>();
        var view = Substitute.For<IGameCellView>();
        var presenter = new GameCellPresenter(model, view);
        presenter.Model.GridIndex = new[] {1, 1};
        _presenter.Model.GridIndex = new[] {2, 2};
        Assert.AreEqual(false, _presenter.IsNeighborOf(presenter));
    }

    [Test] public void SuccessfulCheckingOfLevelTest()
    {
        var model = Substitute.For<GameCellModel>();
        var view = Substitute.For<IGameCellView>();
        var presenter = new GameCellPresenter(model, view);
        presenter.Model.Level = 1;
        _presenter.Model.Level = 1;
        Assert.AreEqual(true, _presenter.HasSameLevelAs(presenter));
    }

    [Test] public void FailedCheckingOfLevelTest()
    {
        var model = Substitute.For<GameCellModel>();
        var view = Substitute.For<IGameCellView>();
        var presenter = new GameCellPresenter(model, view);
        presenter.Model.Level = 1;
        _presenter.Model.Level = 2;
        Assert.AreEqual(false, _presenter.HasSameLevelAs(presenter));
    }
}
