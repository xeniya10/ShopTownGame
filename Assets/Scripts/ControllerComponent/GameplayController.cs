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

    private readonly List<GameCellPresenter> _gameCells = new();
    private readonly List<ManagerRowPresenter> _managerRows = new();
    private readonly List<UpgradeRowPresenter> _upgradeRows = new();

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
        CreateManagerList();
        CreateUpgradeList();
    }

    private void CreateBoard()
    {
        var data = _dataController.GameData;

        foreach (var model in data.Businesses)
        {
            var cell = _gameCellPresenter.Create(_gameScreenView.GameBoard, model);
            cell.Buy(() => CheckPurchase(cell));
            _gameCells.Add(cell);
        }
    }

    private void CheckPurchase(GameCellPresenter cell)
    {
        var cost = cell.GetCost();
        if (_dataController.GameData.CanBuy(cost))
        {
            cell.Activate();
            CheckNeighbors(cell);
        }
    }

    private void CheckNeighbors(GameCellPresenter activatedCell)
    {
        var neighbors = new List<GameCellPresenter>();
        foreach (var cell in _gameCells)
        {
            if (cell.IsNeighbor(activatedCell.GetPosition()))
            {
                neighbors.Add(cell);
            }
        }

        if (neighbors.Count > 0)
        {
            _activationCounter++;
            foreach (var cell in neighbors)
            {
                cell.SetCost(_activationCounter);
                cell.Unlock();
            }
        }
    }

    private void CreateManagerList()
    {
        var data = _dataController.GameData;
        foreach (var model in data.Managers)
        {
            var row = _managerRowPresenter.Create(_gameScreenView.ManagerBoard, model);
            _managerRows.Add(row);
        }
    }

    private void CreateUpgradeList()
    {
        var data = _dataController.GameData;
        foreach (var model in data.Upgrades)
        {
            var row = _upgradeRowPresenter.Create(_gameScreenView.UpgradeBoard, model);
            _upgradeRows.Add(row);
        }
    }
}
}
