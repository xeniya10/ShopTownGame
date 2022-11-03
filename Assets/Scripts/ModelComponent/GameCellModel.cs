using System;
using ShopTown.Data;
using UnityEngine;

namespace ShopTown.ModelComponent
{
public enum CellState { Lock, Unlock, Active, InProgress }

public class GameCellModel
{
    // Description
    public int Level;
    public int BackgroundNumber;

    // Space Parameters
    public float Size;
    public Vector2 GridIndex;
    public Vector2 Position;

    // Time Parameters
    public TimeSpan CurrentTime { get; private set; }
    public TimeSpan TotalTime { get; private set; }

    // State Parameters
    public CellState State;
    public bool IsManagerActivated;
    public int ActivatedUpgradeLevel;
    public bool AreAllUpgradeLevelsActivated;

    // Monetary Parameters
    public int ActivationNumber;
    public MoneyModel Cost { get; private set; }
    public MoneyModel Profit { get; private set; }

    // Data Containers
    private readonly GameCellData _cellData;

    public GameCellModel(GameCellData cellData)
    {
        _cellData = cellData;
    }

    public void Initialize(int level, TimeSpan currentTime, bool isManagerActivated,
        int activatedUpgradeLevel, bool areAllUpgradeLevelsActivated)
    {
        Level = level;
        CurrentTime = currentTime;
        IsManagerActivated = isManagerActivated;
        ActivatedUpgradeLevel = activatedUpgradeLevel;
        AreAllUpgradeLevelsActivated = areAllUpgradeLevelsActivated;
        SetTime();
        SetProfit();
        SetCost();
    }

    public void Reset()
    {
        Initialize(0, TimeSpan.Zero, false, 0, false);
    }

    public void Lock()
    {
        State = CellState.Lock;
        Reset();
    }

    public void Unlock()
    {
        State = CellState.Unlock;
        Reset();
        SetCost();
    }

    public void Activate()
    {
        State = CellState.Active;
        SetTime();
        SetProfit();
    }

    public void LevelUp()
    {
        Level += 1;
        Activate();
    }

    public void SetGridIndex(int rowIndex, int columnIndex)
    {
        GridIndex = new Vector2(columnIndex, rowIndex);
    }

    private void SetTime()
    {
        if (Level < 1)
        {
            TotalTime = TimeSpan.Zero;
            return;
        }

        TotalTime = _cellData.ProcessTime[Level - 1];
    }

    private void SetCost()
    {
        var costData = _cellData.Cost;

        if (ActivationNumber > costData.Count - 1)
        {
            var lastElement = _cellData.Cost[costData.Count - 1];
            Cost = new MoneyModel(lastElement.Number * (ActivationNumber - costData.Count + 2), lastElement.Value);
            return;
        }

        Cost = costData[ActivationNumber];
    }

    private void SetProfit()
    {
        if (Level < 1)
        {
            Profit = null;
            return;
        }

        var profitMultiplier = ActivatedUpgradeLevel + 1;
        var baseProfit = _cellData.BaseProfit[Level - 1];
        Profit = new MoneyModel(baseProfit.Number * profitMultiplier, baseProfit.Value);
    }
}
}
