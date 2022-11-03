using System.Collections.Generic;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using ShopTown.ViewComponent;
using VContainer.Unity;

namespace ShopTown.ControllerComponent
{
public class GameplayController : IInitializable
{
    private readonly GameCellPresenter _gameCell;
    private readonly ManagerRowPresenter _managerRow;
    private readonly UpgradeRowPresenter _upgradeRow;

    private readonly GameScreenPresenter _gameScreenPresenter;
    private readonly GameScreenView _gameScreenView;
    private readonly DataController _data;

    private readonly List<GameCellPresenter> _cells = new List<GameCellPresenter>();
    private readonly List<ManagerRowPresenter> _managers = new List<ManagerRowPresenter>();
    private readonly List<UpgradeRowPresenter> _upgrades = new List<UpgradeRowPresenter>();
    private readonly List<GameCellPresenter> _selectedCells = new List<GameCellPresenter>();

    private GameplayController(GameCellPresenter gameCell, ManagerRowPresenter managerRow, UpgradeRowPresenter upgradeRow,
        DataController data, GameScreenView gameScreenView, GameScreenPresenter gameScreenPresenter)
    {
        _gameCell = gameCell;
        _managerRow = managerRow;
        _upgradeRow = upgradeRow;
        _data = data;
        _gameScreenView = gameScreenView;
        _gameScreenPresenter = gameScreenPresenter;
    }

    public void Initialize()
    {
        _data.Initialize();
        _gameScreenPresenter.Initialize();

        CreateBoard();
        CreateManagers();
        CreateUpgrades();
    }

    private void CreateBoard()
    {
        foreach (var model in _data.GameData.Businesses)
        {
            var cell = _gameCell.Create(_gameScreenView.GameBoard, model);
            cell.SubscribeToBuyButton(TryBuy);
            cell.SubscribeToClick(Select);
            _cells.Add(cell);
        }
    }

    private void CreateManagers()
    {
        foreach (var model in _data.GameData.Managers)
        {
            var row = _managerRow.Create(_gameScreenView.ManagerBoard, model);
            row.SubscribeToHireButton(TryBuy);
            _managers.Add(row);
        }

        FindManager(_data.GameData.MinLevel)?.SetState(ManagerState.Lock);
    }

    private void CreateUpgrades()
    {
        foreach (var model in _data.GameData.Upgrades)
        {
            var row = _upgradeRow.Create(_gameScreenView.UpgradeBoard, model);
            row.SubscribeToBuyButton(TryBuy);
            _upgrades.Add(row);
        }

        FindUpgrade(_data.GameData.MinLevel)?.SetState(UpgradeState.Lock);
    }

    private void TryBuy(GameCellPresenter cell)
    {
        if (_data.GameData.CanBuy(cell.CellModel.Cost))
        {
            cell.CellModel.Level = _data.GameData.MinLevel;
            cell.Activate(() =>
            {
                InitializeImprovements(cell);
                CheckImprovements(cell.CellModel.Level);
            });

            ShowNewLevelProfiler(_data.GameData.MinLevel);
            UnlockNeighbors(cell);
            SaveModels();
        }
    }

    private void TryBuy(ManagerRowPresenter manager)
    {
        if (_data.GameData.CanBuy(manager.ManagerRowModel.Cost))
        {
            manager.Activate();
            FindAllCells(manager.ManagerRowModel.Level).ForEach(InitializeManager);
            SaveModels();
        }
    }

    private void TryBuy(UpgradeRowPresenter upgrade)
    {
        if (_data.GameData.CanBuy(upgrade.UpgradeRowModel.Cost))
        {
            upgrade.Activate();
            FindAllCells(upgrade.UpgradeRowModel.Level).ForEach(cell => cell.InitializeUpgrade(upgrade.UpgradeRowModel));
            SaveModels();
        }
    }

    private void Select(GameCellPresenter selectedCell)
    {
        selectedCell.SetActiveSelector(true);
        _selectedCells.Add(selectedCell);

        if (_selectedCells.Count < 2)
        {
            return;
        }

        var oneCell = _selectedCells[1];
        var otherCell = _selectedCells[0];

        if (oneCell.Equals(otherCell) && oneCell.CellModel.State == CellState.Active)
        {
            selectedCell.GetInProgress(() =>
            {
                _data.GameData.AddToBalance(selectedCell.CellModel.Profit);
                selectedCell.CellModel.State = CellState.Active;
            });
        }

        if (oneCell.IsNeighborOf(otherCell) && oneCell.HasSameLevelAs(otherCell))
        {
            Merge(oneCell, otherCell);
        }

        _selectedCells.ForEach(cell => cell.SetActiveSelector(false));
        _selectedCells.Clear();
    }

    private void Merge(GameCellPresenter oneCell, GameCellPresenter otherCell)
    {
        if (oneCell.CellModel.Level < _data.GameData.MaxLevel)
        {
            CountActivation();
            oneCell.LevelUp();
            otherCell.Unlock();
            ShowNewLevelProfiler(oneCell.CellModel.Level);
            InitializeImprovements(oneCell);
            CheckImprovements(oneCell.CellModel.Level);
            CheckImprovements(oneCell.CellModel.Level - 1);
            SaveModels();
        }
    }

    private void CheckImprovements(int level)
    {
        CheckUpgrade(level);
        CheckManager(level);
    }

    private void CheckManager(int level)
    {
        var gameCell = FindCell(level);
        if (gameCell == null || gameCell.CellModel.IsManagerActivated)
        {
            FindManager(level)?.SetState(ManagerState.Lock);
            return;
        }

        FindManager(level)?.SetState(ManagerState.Unlock);
    }

    private void CheckUpgrade(int level)
    {
        var gameCell = FindCell(level);
        var model = gameCell?.CellModel;
        var maxUpgradeLevel = _data.GameData.MaxUpgradeLevel;
        if (gameCell == null || model.ActivatedUpgradeLevel == maxUpgradeLevel && model.AreAllUpgradeLevelsActivated)
        {
            FindUpgrade(level)?.SetState(UpgradeState.Lock);
            return;
        }

        FindUpgrade(level)?.SetState(UpgradeState.Unlock);
    }

    private void InitializeImprovements(GameCellPresenter cell)
    {
        InitializeUpgrade(cell);
        InitializeManager(cell);
    }

    private void InitializeManager(GameCellPresenter cell)
    {
        var level = cell.CellModel.Level;
        var managerModel = FindManager(level).ManagerRowModel;
        cell.InitializeManager(managerModel);
        if (managerModel.IsActivated)
        {
            cell.GetInProgress(() => _data.GameData.AddToBalance(cell.CellModel.Profit));
        }
    }

    private void InitializeUpgrade(GameCellPresenter cell)
    {
        var upgradeModel = FindUpgrade(cell.CellModel.Level).UpgradeRowModel;
        cell.InitializeUpgrade(upgradeModel);
    }

    private void ShowNewLevelProfiler(int level)
    {
        if (level > _data.GameData.MaxOpenedLevel)
        {
            _gameScreenPresenter.ShowNewBusinessScreen(level);
            _data.GameData.MaxOpenedLevel = level;
        }
    }

    private void UnlockNeighbors(GameCellPresenter activatedCell)
    {
        var neighbors = _cells.FindAll(cell => cell.IsNeighborOf(activatedCell) && cell.CellModel.State == CellState.Lock);
        if (neighbors.Count > 0)
        {
            CountActivation();
            neighbors.ForEach(cell => cell.Unlock());
        }
    }

    private void SaveModels()
    {
        SaveCells();
        SaveManagers();
        SaveUpgrades();
        _data.Save();
    }

    private void SaveCells()
    {
        _data.GameData.Businesses.Clear();
        for (var i = 0; i < _cells.Count; i++)
        {
            _data.GameData.Businesses.Add(_cells[i].CellModel);
        }
    }

    private void SaveManagers()
    {
        _data.GameData.Managers.Clear();
        for (var i = 0; i < _managers.Count; i++)
        {
            _data.GameData.Managers.Add(_managers[i].ManagerRowModel);
        }
    }

    private void SaveUpgrades()
    {
        _data.GameData.Upgrades.Clear();
        for (var i = 0; i < _upgrades.Count; i++)
        {
            _data.GameData.Upgrades.Add(_upgrades[i].UpgradeRowModel);
        }
    }

    private GameCellPresenter FindCell(int level)
    {
        return _cells.Find(cell => cell.CellModel.Level == level);
    }

    private List<GameCellPresenter> FindAllCells(int level)
    {
        return _cells.FindAll(cell => cell.CellModel.Level == level);
    }

    private ManagerRowPresenter FindManager(int level)
    {
        return _managers.Find(manager => manager.ManagerRowModel.Level == level);
    }

    private UpgradeRowPresenter FindUpgrade(int level)
    {
        return _upgrades.Find(upgrade => upgrade.UpgradeRowModel.Level == level);
    }

    private void CountActivation()
    {
        _data.GameData.ActivationNumber++;
        _cells.ForEach(cell => cell.CellModel.ActivationNumber = _data.GameData.ActivationNumber);
    }
}
}
