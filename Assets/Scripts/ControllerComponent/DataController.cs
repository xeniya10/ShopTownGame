using System;
using System.Collections.Generic;
using VContainer.Unity;

public class DataController : IInitializable
{
    public GameDataModel GameData;

    private readonly BusinessData _businessData;
    private readonly GameCellData _gameCellData;
    private readonly ManagerRowData _managerRowData;
    private readonly UpgradeRowData _upgradeRowData;

    public DataController(GameDataModel gameData,
    BusinessData businessData, GameCellData gameCellData,
    ManagerRowData managerRowData, UpgradeRowData upgradeRowData)
    {
        GameData = gameData;
        _businessData = businessData;
        _gameCellData = gameCellData;
        _managerRowData = managerRowData;
        _upgradeRowData = upgradeRowData;
    }

    public void Initialize()
    {
        if (GameData == null)
        {
            CreateDefaultGameData();
            return;
        }
    }

    public void CreateDefaultGameData()
    {
        GameData = new GameDataModel
        {
            CurrentMoneyBalance = 2,
            CurrentGoldBalance = 0,
            TotalMoneyBalance = 0,
            TotalGoldBalance = 0,
            MaxOpenedLevel = 1,
            NumberOfLevels = 27,
            TimeStamp = DateTime.Now,

            Settings = new GameSettingModel()
            {
                Music = true,
                Sound = true,
                Notifications = true,
                Ads = true
            },

            GameBoardModel = new GameBoardModel()
            {
                Rows = 4,
                Columns = 3,
            },

            Businesses = new List<GameCellModel>(),
            Managers = new List<ManagerRowModel>(),
            Upgrades = new List<UpgradeRowModel>()
        };

        var board = GameData.GameBoardModel;

        for (int i = 0; i < board.Rows; i++)
        {
            for (int j = 0; j < board.Columns; j++)
            {
                var cell = new GameCellModel(_businessData, _gameCellData);
                cell.Level = 1;
                cell.SetGridInexes(i, j);
                cell.CellSize = GameData.GameBoardModel.CalculateCellSize();
                cell.Position = GameData.GameBoardModel.CalculateCellPosition(j, i, cell.CellSize);
                GameData.Businesses.Add(cell);

                if (i == board.Rows - 2 && j == board.Columns - 2)
                {
                    cell.State = CellState.Unlock;
                }
            }
        }

        for (int i = 0; i < GameData.NumberOfLevels; i++)
        {
            var manager = new ManagerRowModel(_businessData, _managerRowData);
            manager.Level = i + 1;
            GameData.Managers.Add(manager);

            var upgrade = new UpgradeRowModel(_businessData, _upgradeRowData);
            upgrade.Level = i + 1;
            GameData.Upgrades.Add(upgrade);
        }
    }
}