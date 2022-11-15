using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShopTown.ModelComponent
{
public enum Data { Game, Settings, GameBoard, Manager, Upgrade }

public class DataController : IInitializable
{
    public GameDataModel GameData;
    public GameSettingModel Settings;

    public List<GameCellModel> GameBoard;
    public List<ManagerRowModel> Managers;
    public List<UpgradeRowModel> Upgrades;

    [Inject] private readonly GameBoardModel _gameBoardModel;

    public void Initialize()
    {
        // LoadData();

        if (GameData == null)
        {
            CreateDefaultDataConfig();
        }

        GameData.BalanceChangeEvent += () => Save(Data.Game);
        GameData.ActivationNumberChangeEvent += () => Save(Data.Game);
        GameData.MaxOpenedLevelChangeEvent += () => Save(Data.Game);
    }

    private void LoadData()
    {
        GameData = Load<GameDataModel>(Data.Game);
        Settings = Load<GameSettingModel>(Data.Settings);
        GameBoard = Load<List<GameCellModel>>(Data.GameBoard);
        Managers = Load<List<ManagerRowModel>>(Data.Manager);
        Upgrades = Load<List<UpgradeRowModel>>(Data.Upgrade);
    }

    private T Load<T>(Data dataType)
    {
        var key = dataType.ToString();
        return JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(key));
    }

    public void Save(Data dataType)
    {
        var key = dataType.ToString();
        var json = JsonConvert.SerializeObject(GetObject(dataType), Formatting.Indented);
        PlayerPrefs.SetString(key, json);
    }

    private object GetObject(Data dataType)
    {
        switch (dataType)
        {
            case Data.Game: return GameData;

            case Data.Settings: return Settings;

            case Data.GameBoard: return GameBoard;

            case Data.Manager: return Managers;

            case Data.Upgrade: return Upgrades;
        }

        return null;
    }

    private void CreateDefaultDataConfig()
    {
        CreateDefaultGameData();
        CreateDefaultSettings();
        CreateDefaultGameBoard();
        CreateDefaultManagers();
        CreateDefaultUpgrades();
    }

    private void CreateDefaultGameData()
    {
        GameData = new GameDataModel
        {
            CurrentMoneyBalance = new MoneyModel(1000000, Currency.Dollar),
            CurrentGoldBalance = new MoneyModel(0, Currency.Gold),
            MinLevel = 1,
            MaxLevel = 27,
            MaxUpgradeLevel = 3
        };

        GameData.SetMaxOpenedLevel(0);
        GameData.SetActivationNumber(0);
        Save(Data.Game);
    }

    private void CreateDefaultSettings()
    {
        Settings = new GameSettingModel
        {
            MusicOn = true,
            SoundOn = true,
            NotificationsOn = true,
            AdsOn = true
        };

        Save(Data.Settings);
    }

    private void CreateDefaultGameBoard()
    {
        GameBoard = new List<GameCellModel>();
        for (var i = 0; i < _gameBoardModel.Rows; i++)
        {
            for (var j = 0; j < _gameBoardModel.Columns; j++)
            {
                var cell = new GameCellModel();
                cell.Initialize(0, DateTime.MaxValue);
                cell.BackgroundNumber = int.MinValue;
                cell.SetState(CellState.Lock);
                cell.SetGridIndex(i, j);
                cell.Size = _gameBoardModel.CalculateCellSize();
                cell.Position = _gameBoardModel.CalculateCellPosition(j, i, cell.Size);
                GameBoard.Add(cell);

                if (i == _gameBoardModel.Rows - 2 && j == _gameBoardModel.Columns - 2)
                {
                    cell.Level = GameData.MinLevel;
                    cell.SetState(CellState.Unlock);
                }
            }
        }

        Save(Data.GameBoard);
    }

    private void CreateDefaultManagers()
    {
        Managers = new List<ManagerRowModel>();
        for (var i = 0; i < GameData.MaxLevel; i++)
        {
            var manager = new ManagerRowModel();
            manager.Initialize(i + 1);
            Managers.Add(manager);
            if (i == 0)
            {
                manager.SetState(ImprovementState.Lock);
            }
        }

        Save(Data.Manager);
    }

    private void CreateDefaultUpgrades()
    {
        Upgrades = new List<UpgradeRowModel>();
        for (var i = 0; i < GameData.MaxLevel; i++)
        {
            var upgrade = new UpgradeRowModel();
            upgrade.Initialize(i + 1);
            Upgrades.Add(upgrade);
            if (i == 0)
            {
                upgrade.SetState(ImprovementState.Lock);
            }
        }

        Save(Data.Upgrade);
    }
}
}
