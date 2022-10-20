using System;
using System.Collections.Generic;
using ShopTown.Data;
using UnityEngine;
using VContainer.Unity;

namespace ShopTown.ModelComponent
{
public class DataController : IInitializable
{
    public GameDataModel GameData;

    // Data Containers
    private readonly BusinessData _businessData;
    private readonly GameCellData _gameCellData;
    private readonly ManagerRowData _managerRowData;
    private readonly UpgradeRowData _upgradeRowData;

    public DataController(GameDataModel gameData, BusinessData businessData, GameCellData gameCellData,
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
        }
    }

    public void CreateDefaultGameData()
    {
        var settings = new GameSettingModel
        {
            Music = true,
            Sound = true,
            Notifications = true,
            Ads = true
        };

        var boardModel = new GameBoardModel
        {
            Rows = 4,
            Columns = 3
        };

        GameData = new GameDataModel
        {
            CurrentMoneyBalance = new MoneyModel(300000, Currency.Dollar),
            CurrentGoldBalance = new MoneyModel(0, Currency.Gold),
            // TotalMoneyBalance = 0,
            // TotalGoldBalance = 0,
            MaxOpenedLevel = 0,
            NumberOfLevels = 27,
            TimeStamp = DateTime.Now,

            Settings = settings,
            GameBoardModel = boardModel,

            Businesses = new List<GameCellModel>(),
            Managers = new List<ManagerRowModel>(),
            Upgrades = new List<UpgradeRowModel>()
        };

        // Creation default game cell models
        for (var i = 0; i < boardModel.Rows; i++)
        {
            for (var j = 0; j < boardModel.Columns; j++)
            {
                var cell = new GameCellModel(_gameCellData);
                cell.UpgradeLevel = 0;
                cell.SetCost(0);
                cell.SetGridIndex(i, j);
                cell.Size = GameData.GameBoardModel.CalculateCellSize();
                cell.Position = GameData.GameBoardModel.CalculateCellPosition(j, i, cell.Size);
                GameData.Businesses.Add(cell);

                if (i == boardModel.Rows - 2 && j == boardModel.Columns - 2)
                {
                    cell.Level = 1;
                    cell.State = CellState.Unlock;
                }
            }
        }

        // Creation default manager and upgrade models
        for (var i = 0; i < GameData.NumberOfLevels; i++)
        {
            var manager = new ManagerRowModel(_businessData, _managerRowData);
            manager.Level = i + 1;
            manager.State = ManagerState.Hide;
            manager.IsActivated = false;
            GameData.Managers.Add(manager);
        }

        for (var i = 0; i < GameData.NumberOfLevels; i++)
        {
            var upgrade = new UpgradeRowModel(_businessData, _upgradeRowData);
            upgrade.Level = i + 1;
            upgrade.State = UpgradeState.Hide;
            upgrade.IsActivated = false;
            GameData.Upgrades.Add(upgrade);
        }
    }
}
}
