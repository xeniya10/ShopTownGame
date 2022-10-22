using System.Collections.Generic;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using ShopTown.ViewComponent;
using UnityEngine;
using VContainer.Unity;

namespace ShopTown.ControllerComponent
{
public class GameplayController : IInitializable
{
    private readonly GameCellPresenter _gameCellPresenter;
    private readonly ManagerRowPresenter _managerRowPresenter;
    private readonly UpgradeRowPresenter _upgradeRowPresenter;

    private readonly GameScreenPresenter _gameScreenPresenter;
    private readonly GameScreenView _gameScreenView;
    private readonly DataController _dataController;

    private readonly List<GameCellPresenter> _gameCells = new List<GameCellPresenter>();
    private readonly List<ManagerRowPresenter> _managerRows = new List<ManagerRowPresenter>();
    private readonly List<UpgradeRowPresenter> _upgradeRows = new List<UpgradeRowPresenter>();
    private readonly List<GameCellPresenter> _selectedCells = new List<GameCellPresenter>();

    private int _activationCounter;

    public GameplayController(GameCellPresenter gameCellPresenter, ManagerRowPresenter managerRowPresenter, UpgradeRowPresenter upgradeRowPresenter,
        DataController dataController, GameScreenView gameScreenView, GameScreenPresenter gameScreenPresenter)
    {
        _gameCellPresenter = gameCellPresenter;
        _managerRowPresenter = managerRowPresenter;
        _upgradeRowPresenter = upgradeRowPresenter;
        _dataController = dataController;
        _gameScreenView = gameScreenView;
        _gameScreenPresenter = gameScreenPresenter;
    }

    public void Initialize()
    {
        _dataController.CreateDefaultGameData();
        _gameScreenPresenter.Initialize();

        CreateBoard();
        CreateManagers();
        CreateUpgrades();
    }

    private void CreateBoard()
    {
        var data = _dataController.GameData;
        foreach (var model in data.Businesses)
        {
            var cell = _gameCellPresenter.Create(_gameScreenView.GameBoard, model);
            cell.SubscribeToBuyButton(TryBuy);
            cell.SubscribeToClick(Select);
            _gameCells.Add(cell);
        }
    }

    private void CreateManagers()
    {
        var data = _dataController.GameData;
        foreach (var model in data.Managers)
        {
            var row = _managerRowPresenter.Create(_gameScreenView.ManagerBoard, model);
            row.SubscribeToHireButton(TryBuy);
            _managerRows.Add(row);
        }

        var lockedManager = _managerRows.Find(manager => manager.ManagerRowModel.State == ManagerState.Lock);
        var unlockedManager = _managerRows.Find(manager => manager.ManagerRowModel.State == ManagerState.Unlock);

        if (lockedManager == null && unlockedManager == null)
        {
            var manager = _managerRows.Find(manager => manager.ManagerRowModel.Level == data.MinOpenedLevel);
            manager.SetState(ManagerState.Lock);
        }
    }

    private void CreateUpgrades()
    {
        var data = _dataController.GameData;
        foreach (var model in data.Upgrades)
        {
            var row = _upgradeRowPresenter.Create(_gameScreenView.UpgradeBoard, model);
            row.SubscribeToBuyButton(TryBuy);
            _upgradeRows.Add(row);
        }

        var lockedUpgrade = _upgradeRows.Find(upgrade => upgrade.UpgradeRowModel.State == UpgradeState.Lock);
        var unlockedUpgrade = _upgradeRows.Find(upgrade => upgrade.UpgradeRowModel.State == UpgradeState.Unlock);

        if (lockedUpgrade == null && unlockedUpgrade == null)
        {
            var upgrade = _upgradeRows.Find(upgrade => upgrade.UpgradeRowModel.Level == data.MinOpenedLevel);
            upgrade.SetState(UpgradeState.Lock);
        }
    }

    private void TryBuy(GameCellPresenter cell)
    {
        var data = _dataController.GameData;
        if (data.CanBuy(cell.CellModel.Cost))
        {
            cell.CellModel.Level = data.MinOpenedLevel;
            cell.Activate(() =>
            {
                var manager = _managerRows.Find(manager => manager.ManagerRowModel.Level == data.MinOpenedLevel);
                if (manager.ManagerRowModel.IsActivated)
                {
                    cell.SetActiveManager(manager.ManagerRowModel);
                    cell.GetInProgress(() => data.AddToBalance(cell.CellModel.Profit));
                }

                var upgrade = _upgradeRows.Find(upgrade => upgrade.UpgradeRowModel.Level == data.MinOpenedLevel);
                cell.SetActiveUpgrade(upgrade.UpgradeRowModel);
            });

            ShowNewLevelProfiler(data.MinOpenedLevel);
            UnlockNeighbors(cell);
        }
    }

    private void TryBuy(ManagerRowPresenter manager)
    {
        var data = _dataController.GameData;
        if (data.CanBuy(manager.ManagerRowModel.Cost))
        {
            manager.PlaySalute();
            manager.SetState(ManagerState.Lock);
            manager.ManagerRowModel.IsActivated = true;
            var cells = _gameCells.FindAll(cell => cell.CellModel.Level == manager.ManagerRowModel.Level);
            cells.ForEach(cell =>
            {
                cell.GetInProgress(() => data.AddToBalance(cell.CellModel.Profit));
                cell.SetActiveManager(manager.ManagerRowModel);
            });
        }
    }

    private void TryBuy(UpgradeRowPresenter upgrade)
    {
        var data = _dataController.GameData;
        if (data.CanBuy(upgrade.UpgradeRowModel.Cost))
        {
            upgrade.PlaySalute();
            upgrade.LevelUp();
            var cells = _gameCells.FindAll(cell => cell.CellModel.Level == upgrade.UpgradeRowModel.Level);
            cells.ForEach(cell => cell.SetActiveUpgrade(upgrade.UpgradeRowModel));
        }
    }

    private void Select(GameCellPresenter selectedCell)
    {
        var data = _dataController.GameData;
        _selectedCells.Add(selectedCell);
        selectedCell.SetActiveSelector(true);

        if (_selectedCells.Count < 2)
        {
            return;
        }

        var oneCell = _selectedCells[0];
        var otherCell = _selectedCells[1];

        if (oneCell.Equals(otherCell) && oneCell.CellModel.State == CellState.Active)
        {
            selectedCell.GetInProgress(() =>
            {
                data.AddToBalance(selectedCell.CellModel.Profit);
                selectedCell.CellModel.State = CellState.Active;
            });
        }

        if (oneCell.IsNeighborOf(otherCell) && oneCell.HasSameLevelAs(otherCell))
        {
            Merge(oneCell, otherCell);
        }

        oneCell.SetActiveSelector(false);
        otherCell.SetActiveSelector(false);
        _selectedCells.Clear();
    }

    private void Merge(GameCellPresenter oneCell, GameCellPresenter otherCell)
    {
        var data = _dataController.GameData;
        _activationCounter++;
        var cellUp = otherCell;
        var cellUnlock = oneCell;
        if (oneCell.IsActivatedEarlierThen(otherCell))
        {
            cellUp = oneCell;
            cellUnlock = otherCell;
        }

        var level = cellUnlock.CellModel.Level;
        cellUp.LevelUp(FindManager(level), FindUpgrade(level));
        cellUnlock.Unlock(_activationCounter);
        LockNeedlessManager(level);
        LockNeedlessUpgrade(level);
        ShowNewLevelProfiler(cellUp.CellModel.Level);

        if (FindManager(level).IsActivated)
        {
            cellUp.GetInProgress(() => data.AddToBalance(cellUp.CellModel.Profit));
        }
    }

    private ManagerRowModel FindManager(int level)
    {
        var activatedManager = _managerRows.Find(manager => manager.ManagerRowModel.Level == level + 1);
        return activatedManager.ManagerRowModel;
    }

    private UpgradeRowModel FindUpgrade(int level)
    {
        var activatedUpgrade = _upgradeRows.Find(upgrade => upgrade.UpgradeRowModel.Level == level + 1);
        return activatedUpgrade.UpgradeRowModel;
    }

    private void LockNeedlessManager(int level)
    {
        var gameCell = _gameCells.Find(cell => cell.CellModel.Level == level);
        var manager = _managerRows.Find(manager => manager.ManagerRowModel.Level == level);
        if (gameCell == null || gameCell.CellModel.IsActivatedManager)
        {
            manager?.SetState(ManagerState.Lock);
        }
        else
        {
            manager?.SetState(ManagerState.Unlock);
        }
    }

    private void LockNeedlessUpgrade(int level)
    {
        var gameCell = _gameCells.Find(cell => cell.CellModel.Level == level);
        var upgrade = _upgradeRows.Find(upgrade => upgrade.UpgradeRowModel.Level == level);
        var isActivatedUpgrades = gameCell?.CellModel.IsActivatedUpgrades;
        if (gameCell == null || isActivatedUpgrades[isActivatedUpgrades.Length - 1])
        {
            upgrade?.SetState(UpgradeState.Lock);
        }
        else
        {
            upgrade?.SetState(UpgradeState.Unlock);
        }
    }

    private void ShowNewLevelProfiler(int level)
    {
        var data = _dataController.GameData;
        if (level > data.MaxOpenedLevel)
        {
            _gameScreenPresenter.ShowNewBusinessScreen(level);
            data.MaxOpenedLevel = level;
        }

        LockNeedlessUpgrade(level);
        LockNeedlessManager(level);
    }

    private void UnlockNeighbors(GameCellPresenter activatedCell)
    {
        var neighbors = new List<GameCellPresenter>();
        foreach (var cell in _gameCells)
        {
            if (cell.IsNeighborOf(activatedCell) && cell.CellModel.State == CellState.Lock)
            {
                neighbors.Add(cell);
            }
        }

        if (neighbors.Count > 0)
        {
            _activationCounter++;
            foreach (var cell in neighbors)
            {
                cell.Unlock(_activationCounter);
            }
        }
    }
}
}
