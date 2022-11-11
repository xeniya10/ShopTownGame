using System.Collections.Generic;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using VContainer;
using VContainer.Unity;

namespace ShopTown.ControllerComponent
{
public class GameplayController : IInitializable
{
    [Inject] private readonly ICreatable<GameCellPresenter, GameCellModel> _gameCell;
    [Inject] private readonly ICreatable<ManagerRowPresenter, ManagerRowModel> _manager;
    [Inject] private readonly ICreatable<UpgradeRowPresenter, UpgradeRowModel> _upgrade;

    [Inject] private readonly GameScreenPresenter _gameScreenPresenter;
    [Inject] private readonly DataController _data;

    private List<GameCellPresenter> _cells = new List<GameCellPresenter>();
    private List<ManagerRowPresenter> _managers = new List<ManagerRowPresenter>();
    private List<UpgradeRowPresenter> _upgrades = new List<UpgradeRowPresenter>();
    private List<GameCellPresenter> _selectedCells = new List<GameCellPresenter>();

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
        foreach (var model in _data.GameBoard)
        {
            var cell = _gameCell.Create(model);
            cell.SetCost(_data.GameData.ActivationNumber);
            cell.SubscribeToBuyButton(TryBuy);
            cell.SubscribeToClick(Select);
            _cells.Add(cell);
        }
    }

    private void CreateManagers()
    {
        foreach (var model in _data.Managers)
        {
            var row = _manager.Create(model);
            row.ModelChangeEvent += () => _data.Save(ModelComponent.Data.Manager);
            row.SubscribeToHireButton(TryBuy);
            _managers.Add(row);
        }
    }

    private void CreateUpgrades()
    {
        foreach (var model in _data.Upgrades)
        {
            var row = _upgrade.Create(model);
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
            Save(ModelComponent.Data.GameBoard);
        }
    }

    private void TryBuy(ManagerRowPresenter manager)
    {
        if (_data.GameData.CanBuy(manager.Model.Cost))
        {
            manager.Activate();
            FindAllCells(manager.Model.Level).ForEach(InitializeManager);
            Save(ModelComponent.Data.Manager);
        }
    }

    private void TryBuy(UpgradeRowPresenter upgrade)
    {
        if (_data.GameData.CanBuy(upgrade.Model.Cost))
        {
            upgrade.Activate();
            FindAllCells(upgrade.Model.Level).ForEach(cell => cell.InitializeUpgrade(upgrade.Model));
            Save(ModelComponent.Data.Upgrade);
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

        Save(ModelComponent.Data.GameBoard);
        _selectedCells.ForEach(cell => cell.SetActiveSelector(false));
        _selectedCells.Clear();
    }

    private void Merge(GameCellPresenter oneCell, GameCellPresenter otherCell)
    {
        if (oneCell.Model.Level < _data.GameData.MaxLevel)
        {
            CountActivation();
            oneCell.LevelUp();
            otherCell.SetState(CellState.Unlock);
            ShowNewLevelProfiler(oneCell.Model.Level);
            InitializeImprovements(oneCell);
            CheckImprovements(oneCell.Model.Level);
            CheckImprovements(oneCell.Model.Level - 1);
        }
    }

    private void CheckImprovements(int level)
    {
        CheckUpgrade(level);
        CheckManager(level);
        Save(ModelComponent.Data.Manager);
        Save(ModelComponent.Data.Upgrade);
    }

    private void CheckManager(int level)
    {
        var gameCell = FindCell(level);
        if (gameCell == null || gameCell.Model.IsManagerActivated)
        {
            FindManager(level)?.SetState(ImprovementState.Lock);
            return;
        }

        FindManager(level)?.SetState(ImprovementState.Unlock);
    }

    private void CheckUpgrade(int level)
    {
        var gameCell = FindCell(level);
        var model = gameCell?.Model;
        var maxUpgradeLevel = _data.GameData.MaxUpgradeLevel;
        if (gameCell == null || model.ActivatedUpgradeLevel == maxUpgradeLevel && model.AreAllUpgradeLevelsActivated)
        {
            FindUpgrade(level)?.SetState(ImprovementState.Lock);
            return;
        }

        FindUpgrade(level)?.SetState(ImprovementState.Unlock);
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
            neighbors.ForEach(cell => cell.SetState(CellState.Unlock));
        }
    }

    private void Save(ModelComponent.Data data)
    {
        switch (data)
        {
            case ModelComponent.Data.GameBoard:
                _data.GameBoard.Clear();
                _cells.ForEach(cell => _data.GameBoard.Add(cell.Model));
                break;

            case ModelComponent.Data.Manager:
                _data.Managers.Clear();
                _managers.ForEach(manager => _data.Managers.Add(manager.Model));
                break;

            case ModelComponent.Data.Upgrade:
                _data.Upgrades.Clear();
                _upgrades.ForEach(upgrade => _data.Upgrades.Add(upgrade.Model));
                break;
        }

        _data.Save(data);
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
        _cells.ForEach(cell => cell.SetCost(_data.GameData.ActivationNumber));
    }
}
}
