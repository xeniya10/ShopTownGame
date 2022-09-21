using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

public class GameplayController : IStartable
{
    private readonly GameCellView _gameCellPrefab;
    private readonly ManagerRowView _managerRowPrefab;
    private readonly UpgradeRowView _upgradeRowPrefab;
    private readonly GameScreenView _gameScreenView;

    private readonly GameBoardModel _gameBoardModel;
    private readonly DataController _dataController;

    private readonly List<GameCellView> _gameCells = new List<GameCellView>();
    private readonly List<ManagerRowView> _managerRows = new List<ManagerRowView>();
    private readonly List<UpgradeRowView> _upgradeRows = new List<UpgradeRowView>();

    public GameplayController(GameCellView gameCellPrefab,
    ManagerRowView managerRowPrefab, UpgradeRowView upgradeRowPrefab,
    GameScreenView gameScreenView, GameBoardModel gameBoardModel,
    DataController dataController)
    {
        _gameCellPrefab = gameCellPrefab;
        _managerRowPrefab = managerRowPrefab;
        _upgradeRowPrefab = upgradeRowPrefab;
        _gameScreenView = gameScreenView;
        _gameBoardModel = gameBoardModel;
        _dataController = dataController;
    }

    public void Start()
    {
        _dataController.CreateDefaultGameData();

        CreateBoard();
        CreateManagerList();
        CreateUpgradeList();
    }

    private void CreateBoard()
    {
        var cellSize = _gameBoardModel.CalculateCellSize();
        _gameCellPrefab.SetSize(cellSize);

        for (int i = 0; i < _gameBoardModel.Rows; i++)
        {
            for (int j = 0; j < _gameBoardModel.Columns; j++)
            {
                var cell = _gameCellPrefab.Create(_gameScreenView.GameBoardContent);
                _gameCells.Add(cell);

                cell.SetRandomBackgroundSprite();
                cell.StartPosition = _gameBoardModel.CalculateCellPosition(j, i, cellSize);
                cell.SetPosition(cell.StartPosition);
            }
        }
    }

    private void CreateManagerList()
    {
        var data = _dataController.GameData;

        for (int i = 0; i < _dataController.GameData.NumberOfLevels - 1; i++)
        {
            var level = i + 1;
            var row = _managerRowPrefab.Create(_gameScreenView.ManagerBoardContent);
            _managerRows.Add(row);

            row.SetSprite(level);
            row.SetMoneyPrice(data.Managers[i].MoneyCost);
            if (data.Managers[i].MoneyCost == 0)
            {
                row.SetGoldPrice(data.Managers[i].GoldCost);
            }
            row.SetName(data.Managers[i].Name);
            row.SetDescription(data.Managers[i].Description);
        }
    }

    private void CreateUpgradeList()
    {
        var data = _dataController.GameData;

        for (int i = 0; i < _dataController.GameData.NumberOfLevels - 1; i++)
        {
            var level = i + 1;
            var row = _upgradeRowPrefab.Create(_gameScreenView.UpgradeBoardContent);
            _upgradeRows.Add(row);

            row.SetFirstUpgradeSprite(level);
            row.SetMoneyPrice(data.FirstUpgrades[i].MoneyCost);
            if (data.FirstUpgrades[i].MoneyCost == 0)
            {
                row.SetGoldPrice(data.FirstUpgrades[i].GoldCost);
            }
            row.SetName(data.FirstUpgrades[i].Name);
            row.SetDescription(data.FirstUpgrades[i].Description);
        }
    }
}
