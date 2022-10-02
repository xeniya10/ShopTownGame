using System.Collections.Generic;
using VContainer.Unity;

public class GameplayController : IStartable
{
    private readonly GameCellPresenter _gameCellPresenter;
    private readonly ManagerRowPresenter _managerRowPresenter;
    private readonly UpgradeRowPresenter _upgradeRowPresenter;

    private readonly GameScreenPresenter _gameScreenPresenter;

    private readonly DataController _dataController;
    private readonly GameScreenView _gameScreenView;

    private readonly List<GameCellPresenter> _gameCells = new List<GameCellPresenter>();
    private readonly List<ManagerRowPresenter> _managerRows = new List<ManagerRowPresenter>();
    private readonly List<UpgradeRowPresenter> _upgradeRows = new List<UpgradeRowPresenter>();

    private int _activationCounter = 0;

    public GameplayController(GameCellPresenter gameCellPresenter,
    ManagerRowPresenter managerRowPresenter, UpgradeRowPresenter upgradeRowPresenter,
    DataController dataController, GameScreenView gameScreenView,
    GameScreenPresenter gameScreenPresenter)
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

        for (int i = 0; i < data.Businesses.Count; i++)
        {
            var cell = _gameCellPresenter.Create(_gameScreenView.GameBoard, data.Businesses[i]);
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

        for (int i = 0; i < data.Managers.Count; i++)
        {
            var row = _managerRowPresenter.Create(_gameScreenView.ManagerBoard, data.Managers[i]);
            _managerRows.Add(row);
        }
    }

    private void CreateUpgradeList()
    {
        var data = _dataController.GameData;

        for (int i = 0; i < data.Upgrades.Count; i++)
        {
            var row = _upgradeRowPresenter.Create(_gameScreenView.UpgradeBoard, data.Upgrades[i]);
            _upgradeRows.Add(row);
        }
    }
}