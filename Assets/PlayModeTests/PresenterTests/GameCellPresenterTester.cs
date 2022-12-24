using System;
using NSubstitute;
using NUnit.Framework;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using ShopTown.ViewComponent;
using Random = UnityEngine.Random;

public class GameCellPresenterTester
{
    private GameCellModel _model;
    private IGameCellView _view;
    private IBoardData _boardData;
    private IGameCell _presenter;

    [SetUp] public void CreateTestObjects()
    {
        _model = Substitute.For<GameCellModel>();
        _view = Substitute.For<IGameCellView>();
        _boardData = Substitute.For<IBoardData>();
        _presenter = new GameCellPresenter(_model, _view);
    }

    private void PrepareCaseTest(bool isMangerActivated, int timeMultiplier = 1)
    {
        _boardData.GetProfit(Arg.Any<int>()).Returns(new MoneyModel(Random.Range(1, 100000)));
        _boardData.GetTime(Arg.Any<int>())
            .Returns(new TimeModel(Random.Range(0, 3), Random.Range(0, 60), Random.Range(0, 60), Random.Range(0, 60)));

        _boardData.GetCost(Arg.Any<int>()).Returns(new MoneyModel(Random.Range(1, 100000)));
        _boardData.GetCostCount().Returns(1);

        _presenter.Model.Level = 1;
        _presenter.Model.IsManagerActivated = isMangerActivated;
        var processTimeInSeconds = _boardData.GetTime(1).ToTimeSpan().TotalSeconds * timeMultiplier;

        var processTime = TimeSpan.FromSeconds(processTimeInSeconds);
        _presenter.Model.StartTime = DateTime.Now.Subtract(processTime);
    }

    [Test] public void SetState_OnUnlock_CallsChangeEvent()
    {
        var wasCalled = false;
        _boardData.GetDefaultCell().Returns(new GameCellModel());
        _presenter.ChangeEvent += () => wasCalled = true;
        _presenter.SetState(CellState.Unlock, _boardData);
        Assert.True(wasCalled);
    }

    [Test] public void SetState_OnUnlock_SetsUnlock()
    {
        _boardData.GetDefaultCell().Returns(new GameCellModel());
        _presenter.Model.State = CellState.Active;
        _presenter.SetState(CellState.Unlock, _boardData);
        Assert.AreEqual(CellState.Unlock, _presenter.Model.State);
    }

    [Test] public void SetState_OnUnlock_SetsActive()
    {
        _presenter.Model.State = CellState.Unlock;
        _presenter.SetState(CellState.Active, _boardData);
        Assert.AreEqual(CellState.Active, _presenter.Model.State);
    }

    [Test] public void SetState_InProgressByClick_SetsInProgress()
    {
        _presenter.Model.StartTime = DateTime.MaxValue;
        _presenter.Model.State = CellState.Active;
        _presenter.SetState(CellState.InProgress, _boardData);
        Assert.AreEqual(CellState.InProgress, _presenter.Model.State);
    }

    [Test] public void SetState_InProgressWithoutManager_SetsActive()
    {
        PrepareCaseTest(false);
        _presenter.SetState(CellState.InProgress, _boardData);
        Assert.AreEqual(CellState.Active, _presenter.Model.State);
    }

    [Test] public void SetState_InProgressWithoutManager_CallsInProgressEndEvent()
    {
        var wasCalled = false;
        PrepareCaseTest(false);
        _presenter.InProgressEndEvent += (x) => wasCalled = true;
        _presenter.SetState(CellState.InProgress, _boardData);
        Assert.True(wasCalled);
    }

    [Test] public void SetState_InProgressWithoutManager_CallsGetOfflineProfitEvent()
    {
        var profit = new MoneyModel(0);
        PrepareCaseTest(false);
        _presenter.GetOfflineProfitEvent += (offlineProfit) => profit.Value = offlineProfit.Value;
        _presenter.SetState(CellState.InProgress, _boardData);
        Assert.AreEqual(_presenter.Model.Profit.Value, profit.Value);
    }

    [Test] public void SetState_InProgressWithManager_SetsActive()
    {
        PrepareCaseTest(true);
        _presenter.SetState(CellState.InProgress, _boardData);
        Assert.AreEqual(CellState.InProgress, _presenter.Model.State);
    }

    [Test] public void SetState_InProgressWithManager_CallsInProgressEndEvent()
    {
        var wasCalled = false;
        PrepareCaseTest(true);
        _presenter.InProgressEndEvent += (x) => wasCalled = true;
        _presenter.SetState(CellState.InProgress, _boardData);
        Assert.True(wasCalled);
    }

    [Test] public void SetState_InProgressWithManager_CallsGetOfflineProfitEvent()
    {
        var multiplier = 3;
        var profit = new MoneyModel(0);
        PrepareCaseTest(true, multiplier);
        _presenter.GetOfflineProfitEvent += (offlineProfit) => profit.Value = offlineProfit.Value;
        _presenter.SetState(CellState.InProgress, _boardData);
        Assert.AreEqual(_presenter.Model.Profit.Value * multiplier, profit.Value);
    }

    [Test] public void SetState_InProgress_InvokesInProgressEndEventByCallBack()
    {
        var wasCalled = false;
        _presenter.Model.StartTime = DateTime.MaxValue;
        _view.StartAnimation(Arg.Any<GameCellModel>(), Arg.Invoke());
        _presenter.InProgressEndEvent += (x) => wasCalled = true;
        _presenter.SetState(CellState.InProgress, _boardData);
        Assert.True(wasCalled);
    }

    [Test] public void SetState_InProgress_SetsActiveByCallBack()
    {
        _presenter.Model.StartTime = DateTime.MaxValue;
        _view.StartAnimation(Arg.Any<GameCellModel>(), Arg.Invoke());
        _presenter.SetState(CellState.InProgress, _boardData);
        Assert.AreEqual(CellState.Active, _presenter.Model.State);
    }

    [Test] public void SetCost_InRange_SetsTheSameCostValue()
    {
        var activationNumber = 0;
        PrepareCaseTest(false);
        _presenter.SetCost(activationNumber, _boardData);
        Assert.AreEqual(_boardData.GetCost(1).Value, _presenter.Model.Cost.Value);
    }

    [Test] public void SetCost_OutOfRange_SetsTheSameCostValue()
    {
        var activationNumber = 1;
        PrepareCaseTest(false);
        _presenter.SetCost(activationNumber, _boardData);
        Assert.AreEqual(_boardData.GetCost(1).Value * (activationNumber - _boardData.GetCostCount() + 2),
            _presenter.Model.Cost.Value);
    }

    [Test] public void SetProfit_InRange_SetsTheSameProfitValue()
    {
        PrepareCaseTest(false);
        _presenter.SetState(CellState.Active, _boardData);
        Assert.AreEqual(_boardData.GetProfit(1).Value, _presenter.Model.Profit.Value);
    }

    [Test] public void SetProfit_OutOfRange_SetsTheSameProfitValue()
    {
        PrepareCaseTest(false);
        _presenter.Model.Level = 0;
        _presenter.SetState(CellState.Active, _boardData);
        Assert.Null(_presenter.Model.Profit);
    }

    [Test] public void SetTime_InRange_SetsTheSameProcessTimeValue()
    {
        PrepareCaseTest(false);
        _presenter.SetState(CellState.Active, _boardData);
        Assert.AreEqual(_boardData.GetTime(1).TotalSeconds, _presenter.Model.TotalTime.TotalSeconds);
    }

    [Test] public void SetTime_OutOfRange_SetsTheSameProcessTimeValue()
    {
        PrepareCaseTest(false);
        _presenter.Model.Level = 0;
        _presenter.SetState(CellState.Active, _boardData);
        Assert.AreEqual(TimeSpan.Zero, _presenter.Model.TotalTime);
    }

    [Test] public void LevelUp_ZeroLevel_SetsTheFirstLevel()
    {
        PrepareCaseTest(false);
        _presenter.Model.Level = 0;
        _presenter.LevelUp(_boardData);
        Assert.AreEqual(1, _presenter.Model.Level);
    }

    [Test] public void InitializeManager_NotActivatedManager_SetsActive()
    {
        var manager = Substitute.For<ImprovementModel>();
        manager.IsActivated = false;
        PrepareCaseTest(true);
        _presenter.Model.State = CellState.Active;
        _presenter.InitializeManager(manager, _boardData);
        Assert.AreEqual(CellState.Active, _presenter.Model.State);
    }

    [Test] public void InitializeManager_ActivatedManager_SetsInProgress()
    {
        var manager = Substitute.For<ImprovementModel>();
        manager.IsActivated = true;
        PrepareCaseTest(false);
        _presenter.Model.State = CellState.Active;
        _presenter.InitializeManager(manager, _boardData);
        Assert.AreEqual(CellState.InProgress, _presenter.Model.State);
    }

    [Test] public void InitializeManager_ActivatedManager_SetsManagerState()
    {
        var manager = Substitute.For<ImprovementModel>();
        manager.IsActivated = true;
        PrepareCaseTest(false);
        _presenter.InitializeManager(manager, _boardData);
        Assert.True(_presenter.Model.IsManagerActivated);
    }

    [Test] public void InitializeUpgrade_NotActivatedUpgrade_SetsUpgradeLevel()
    {
        var upgrade = Substitute.For<ImprovementModel>();
        upgrade.ImprovementLevel = 2;
        PrepareCaseTest(false);
        _presenter.InitializeUpgrade(upgrade, _boardData);
        Assert.AreEqual(upgrade.ImprovementLevel - 1, _presenter.Model.ActivatedUpgradeLevel);
    }

    [Test] public void InitializeUpgrade_NotActivatedUpgrade_SetsProfit()
    {
        var upgrade = Substitute.For<ImprovementModel>();
        upgrade.ImprovementLevel = 2;
        PrepareCaseTest(false);
        _presenter.InitializeUpgrade(upgrade, _boardData);
        Assert.AreEqual(upgrade.ImprovementLevel, _presenter.Model.Profit.Value / _boardData.GetProfit(1).Value);
    }

    [Test] public void InitializeUpgrade_ActivatedUpgrade_SetsUpgradeState()
    {
        var upgrade = Substitute.For<ImprovementModel>();
        upgrade.ImprovementLevel = 3;
        upgrade.IsActivated = true;
        PrepareCaseTest(false);
        _presenter.InitializeUpgrade(upgrade, _boardData);
        Assert.True(_presenter.Model.AreAllUpgradeLevelsActivated);
    }

    [Test] public void InitializeUpgrade_ActivatedUpgrade_SetsUpgradeLevel()
    {
        var upgrade = Substitute.For<ImprovementModel>();
        upgrade.ImprovementLevel = 3;
        upgrade.IsActivated = true;
        PrepareCaseTest(false);
        _presenter.InitializeUpgrade(upgrade, _boardData);
        Assert.AreEqual(upgrade.ImprovementLevel, _presenter.Model.ActivatedUpgradeLevel);
    }

    [Test] public void InitializeUpgrade_ActivatedUpgrade_SetsProfit()
    {
        var upgrade = Substitute.For<ImprovementModel>();
        upgrade.ImprovementLevel = 3;
        upgrade.IsActivated = true;
        PrepareCaseTest(false);
        _presenter.InitializeUpgrade(upgrade, _boardData);
        Assert.AreEqual(upgrade.ImprovementLevel + 1, _presenter.Model.Profit.Value / _boardData.GetProfit(1).Value);
    }

    [Test] public void IsNeighborOf_CheckingOfTwoNeighbors_ReturnsTrue()
    {
        var model = Substitute.For<GameCellModel>();
        var view = Substitute.For<IGameCellView>();
        var presenter = new GameCellPresenter(model, view);
        presenter.Model.GridIndex = new[] {1, 1};
        _presenter.Model.GridIndex = new[] {1, 2};
        Assert.True(_presenter.IsNeighborOf(presenter));
    }

    [Test] public void IsNeighborOf_CheckingOfTwoNotNeighbors_ReturnsFalse()
    {
        var model = Substitute.For<GameCellModel>();
        var view = Substitute.For<IGameCellView>();
        var presenter = new GameCellPresenter(model, view);
        presenter.Model.GridIndex = new[] {1, 1};
        _presenter.Model.GridIndex = new[] {2, 2};
        Assert.False(_presenter.IsNeighborOf(presenter));
    }

    [Test] public void HasSameLevelAs_CheckingOfCellsWithTheSameLevel_ReturnsTrue()
    {
        var model = Substitute.For<GameCellModel>();
        var view = Substitute.For<IGameCellView>();
        var presenter = new GameCellPresenter(model, view);
        presenter.Model.Level = 1;
        _presenter.Model.Level = 1;
        Assert.True(_presenter.HasSameLevelAs(presenter));
    }

    [Test] public void HasSameLevelAs_CheckingOfCellsWithDifferentLevels_ReturnsFalse()
    {
        var model = Substitute.For<GameCellModel>();
        var view = Substitute.For<IGameCellView>();
        var presenter = new GameCellPresenter(model, view);
        presenter.Model.Level = 1;
        _presenter.Model.Level = 2;
        Assert.False(_presenter.HasSameLevelAs(presenter));
    }
}
