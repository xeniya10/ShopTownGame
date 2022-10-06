using System.Collections.Generic;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using ShopTown.ViewComponent;
using VContainer.Unity;

namespace ShopTown.ControllerComponent
{
public class GameplayController : IStartable
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

    public void Start()
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
            _managerRows.Add(row);
        }
    }

    private void CreateUpgrades()
    {
        var data = _dataController.GameData;
        foreach (var model in data.Upgrades)
        {
            var row = _upgradeRowPresenter.Create(_gameScreenView.UpgradeBoard, model);
            _upgradeRows.Add(row);
        }
    }

    // TODO more universal method
    private void TryBuy(GameCellPresenter cell)
    {
        if (_dataController.GameData.CanBuy(cell.Cost))
        {
            cell.Activate();
            UnlockNeighbors(cell);
        }
    }

    // private void TryBuy(ManagerRowPresenter manager)
    // {
    //     if (_dataController.GameData.CanBuy())
    //     {}
    // }
    //
    // private void TryBuy(UpgradeRowModel upgrade)
    // {
    //     if (_dataController.GameData.CanBuy(manager))
    //     {}
    // }

    private void Select(GameCellPresenter selectedCell)
    {
        var data = _dataController.GameData;
        _selectedCells.Add(selectedCell);
        selectedCell.SetActiveSelector(true);

        if (_selectedCells.Count < 2)
        {
            return;
        }

        if (_selectedCells[0] == _selectedCells[1])
        {
            var profit = selectedCell.Profit;
            selectedCell.GetInProgress(() => data.AddToMoneyBalance(profit));
        }

        // var startPosition1 = _selectedCells[0].GetPosition();
        // var startPosition2 = _selectedCells[1].GetPosition();

        if (_selectedCells[0].IsNeighborOf(_selectedCells[1]) && _selectedCells[0].HasSameLevelAs(_selectedCells[1]))
        {
            Merge(_selectedCells[0], _selectedCells[1]);
        }

        _selectedCells[0].SetActiveSelector(false);
        _selectedCells[1].SetActiveSelector(false);
        _selectedCells.Clear();
    }

    private void Merge(GameCellPresenter oneCell, GameCellPresenter otherCell)
    {
        _activationCounter++;
        if (oneCell.IsActivatedEarlierThen(otherCell))
        {
            oneCell.LevelUp();
            otherCell.Unlock(_activationCounter);
            return;
        }

        otherCell.LevelUp();
        oneCell.Unlock(_activationCounter);
    }

    private void UnlockNeighbors(GameCellPresenter activatedCell)
    {
        var neighbors = new List<GameCellPresenter>();
        foreach (var cell in _gameCells)
        {
            if (cell.IsNeighborOf(activatedCell) && cell.State == CellState.Lock)
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
