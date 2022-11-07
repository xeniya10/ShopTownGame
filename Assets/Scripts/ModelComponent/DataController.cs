using System;
using System.Collections.Generic;
using Newtonsoft.Json;
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

    private const string _key = "GameData";

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
        GameData = Load();

        if (GameData == null)
        {
            CreateDefaultGameData();
        }

        GameData.BalanceChangeEvent += Save;
    }

    private void CreateDefaultGameData()
    {
        var settings = new GameSettingModel
        {
            MusicOn = true,
            SoundOn = true,
            NotificationsOn = true,
            AdsOn = true
        };

        var boardModel = new GameBoardModel
        {
            Rows = 4,
            Columns = 3
        };

        GameData = new GameDataModel
        {
            CurrentMoneyBalance = new MoneyModel(1000000, Currency.Dollar),
            CurrentGoldBalance = new MoneyModel(0, Currency.Gold),
            // TotalMoneyBalance = 0,
            MinLevel = 1,
            MaxLevel = 27,
            MaxOpenedLevel = 0,
            MaxUpgradeLevel = 3,
            ActivationNumber = 0,
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
                var cell = new GameCellModel();
                cell.Inject(_gameCellData);
                cell.ActivationNumber = GameData.ActivationNumber;
                cell.BackgroundNumber = int.MinValue;
                cell.SetState(CellState.Lock);
                cell.SetGridIndex(i, j);
                cell.Size = GameData.GameBoardModel.CalculateCellSize();
                cell.Position = GameData.GameBoardModel.CalculateCellPosition(j, i, cell.Size);
                GameData.Businesses.Add(cell);

                if (i == boardModel.Rows - 2 && j == boardModel.Columns - 2)
                {
                    cell.Level = GameData.MinLevel;
                    cell.SetState(CellState.Unlock);
                }
            }
        }

        // Creation default manager and upgrade models
        for (var i = 0; i < GameData.MaxLevel; i++)
        {
            var manager = new ManagerRowModel(_businessData, _managerRowData);
            manager.Initialize(i + 1, false, ManagerState.Hide);
            GameData.Managers.Add(manager);
        }

        for (var i = 0; i < GameData.MaxLevel; i++)
        {
            var upgrade = new UpgradeRowModel(_businessData, _upgradeRowData);
            upgrade.Initialize(i + 1, 1, false, UpgradeState.Hide);
            GameData.Upgrades.Add(upgrade);
        }
    }

    public void Save()
    {
        PlayerPrefs.DeleteKey(_key);
        var gameData = JsonConvert.SerializeObject(GameData, Formatting.Indented);
        PlayerPrefs.SetString(_key, gameData);
    }

    private GameDataModel Load()
    {
        return JsonConvert.DeserializeObject<GameDataModel>(PlayerPrefs.GetString(_key));
    }
}
}
