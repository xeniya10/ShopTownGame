using System.Collections.Generic;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using ShopTown.ViewComponent;
using VContainer.Unity;

namespace ShopTown.ControllerComponent
{
public class GameplayController : IInitializable
{
    private readonly GameCellPresenter _gameCell;
    private readonly ICreatable<ManagerRowPresenter, ManagerRowModel> _manager;
    private readonly UpgradeRowPresenter _upgradeRow;

    private readonly GameScreenPresenter _gameScreenPresenter;
    private readonly GameScreenView _gameScreenView;
    private readonly DataController _data;
    private readonly GameCellData _cellData;

    private readonly List<GameCellPresenter> _cells = new List<GameCellPresenter>();
    private readonly List<ManagerRowPresenter> _managers = new List<ManagerRowPresenter>();
    private readonly List<UpgradeRowPresenter> _upgrades = new List<UpgradeRowPresenter>();
    private readonly List<GameCellPresenter> _selectedCells = new List<GameCellPresenter>();

    private GameplayController(GameCellPresenter gameCell, UpgradeRowPresenter upgradeRow, DataController data,
        GameScreenView gameScreenView, GameScreenPresenter gameScreenPresenter, GameCellData cellData,
        ICreatable<ManagerRowPresenter, ManagerRowModel> manager)
    {
        _manager = manager;
        _gameCell = gameCell;
        _upgradeRow = upgradeRow;
        _data = data;
        _gameScreenView = gameScreenView;
        _gameScreenPresenter = gameScreenPresenter;
        _cellData = cellData;
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
            model.Inject(_cellData);
            var cell = _gameCell.Create(model);
            cell.SubscribeToBuyButton(TryBuy);
            cell.SubscribeToClick(Select);
            _cells.Add(cell);
        }
    }

    private void CreateManagers()
    {
        foreach (var model in _data.GameData.Managers)
        {
            var row = _manager.Create(model);
            row.SubscribeToHireButton(TryBuy);
            _managers.Add(row);
        }
    }

    private void CreateUpgrades()
    {
        foreach (var model in _data.GameData.Upgrades)
        {
            var row = _upgradeRow.Create(model);
            row.SubscribeToBuyButton(TryBuy);
            _upgrades.Add(row);
        }
    }

    private void TryBuy(GameCellPresenter cell)
    {
        if (_data.GameData.CanBuy(cell.Model.Cost))
        {
            cell.Model.Level = _data.GameData.MinLevel;
            cell.SetState(CellState.Active, () =>
            {
                InitializeImprovements(cell);
                CheckImprovements(cell.Model.Level);
            });

            ShowNewLevelProfiler(_data.GameData.MinLevel);
            UnlockNeighbors(cell);
            SaveModels();
        }
    }

    private void TryBuy(ManagerRowPresenter manager)
    {
        if (_data.GameData.CanBuy(manager.Model.Cost))
        {
            manager.Activate();
            FindAllCells(manager.Model.Level).ForEach(InitializeManager);
            SaveModels();
        }
    }

    private void TryBuy(UpgradeRowPresenter upgrade)
    {
        if (_data.GameData.CanBuy(upgrade.Model.Cost))
        {
            upgrade.Activate();
            FindAllCells(upgrade.Model.Level).ForEach(cell => cell.InitializeUpgrade(upgrade.Model));

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

        if (oneCell.Equals(otherCell) && oneCell.Model.State == CellState.Active)
        {
            selectedCell.SetState(CellState.InProgress, () =>
            {
                _data.GameData.AddToBalance(selectedCell.Model.Profit);
                selectedCell.Model.State = CellState.Active;
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
        if (oneCell.Model.Level < _data.GameData.MaxLevel)
        {
            CountActivation();
            oneCell.LevelUp();
            otherCell.SetState(CellState.Unlock, null);
            ShowNewLevelProfiler(oneCell.Model.Level);
            InitializeImprovements(oneCell);
            CheckImprovements(oneCell.Model.Level);
            CheckImprovements(oneCell.Model.Level - 1);
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
        if (gameCell == null || gameCell.Model.IsManagerActivated)
        {
            FindManager(level)?.SetState(ManagerState.Lock);
            return;
        }

        FindManager(level)?.SetState(ManagerState.Unlock);
    }

    private void CheckUpgrade(int level)
    {
        var gameCell = FindCell(level);
        var model = gameCell?.Model;
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
        var level = cell.Model.Level;
        var managerModel = FindManager(level).Model;
        cell.InitializeManager(managerModel);
        if (managerModel.IsActivated)
        {
            cell.SetState(CellState.InProgress, () => _data.GameData.AddToBalance(cell.Model.Profit));
        }
    }

    private void InitializeUpgrade(GameCellPresenter cell)
    {
        var upgradeModel = FindUpgrade(cell.Model.Level).Model;
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
        var neighbors = _cells.FindAll(cell => cell.IsNeighborOf(activatedCell) && cell.Model.State == CellState.Lock);

        if (neighbors.Count > 0)
        {
            CountActivation();
            neighbors.ForEach(cell => cell.SetState(CellState.Unlock, null));
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
            _data.GameData.Businesses.Add(_cells[i].Model);
        }
    }

    private void SaveManagers()
    {
        _data.GameData.Managers.Clear();
        for (var i = 0; i < _managers.Count; i++)
        {
            _data.GameData.Managers.Add(_managers[i].Model);
        }
    }

    private void SaveUpgrades()
    {
        _data.GameData.Upgrades.Clear();
        for (var i = 0; i < _upgrades.Count; i++)
        {
            _data.GameData.Upgrades.Add(_upgrades[i].Model);
        }
    }

    private GameCellPresenter FindCell(int level)
    {
        return _cells.Find(cell => cell.Model.Level == level);
    }

    private List<GameCellPresenter> FindAllCells(int level)
    {
        return _cells.FindAll(cell => cell.Model.Level == level);
    }

    private ManagerRowPresenter FindManager(int level)
    {
        return _managers.Find(manager => manager.Model.Level == level);
    }

    private UpgradeRowPresenter FindUpgrade(int level)
    {
        return _upgrades.Find(upgrade => upgrade.Model.Level == level);
    }

    private void CountActivation()
    {
        _data.GameData.ActivationNumber++;
        _cells.ForEach(cell => cell.Model.ActivationNumber = _data.GameData.ActivationNumber);
    }
}
}
