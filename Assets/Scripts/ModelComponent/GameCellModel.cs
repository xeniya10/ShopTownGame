using System;
using ShopTown.Data;
using UnityEngine;

namespace ShopTown.ModelComponent
{
public enum CellState
{
    Lock,
    Unlock,
    Active,
    Process
}

public class GameCellModel
{
    // Description
    public int Level;
    public int BackgroundNumber = int.MinValue;
    public string BusinessName { get { return _businessData.LevelNames[Level - 1]; } }

    // Space Parameters
    public float Size;
    public Vector2 GridIndex;
    public Vector2 Position;

    // Time Parameters
    public TimeSpan CurrentTime;
    public TimeSpan TotalTime { get { return _cellData.ProcessTime[Level - 1]; } }
    public DateTime ActivatingDate;

    // State Parameters
    public CellState State = CellState.Lock;
    public bool IsUnlockedManager;
    public bool IsUnlockedUpgrade;
    public int UpgradeLevel;

    // Monetary Parameters
    private float ProfitMultiplier { get { return UpgradeLevel + 1; } }
    public double Profit { get { return _cellData.BaseProfit[Level - 1] * ProfitMultiplier; } }
    public double Cost = 2;

    // Data Containers
    private readonly BusinessData _businessData;
    private readonly GameCellData _cellData;

    public GameCellModel(BusinessData businessData, GameCellData cellData)
    {
        _businessData = businessData;
        _cellData = cellData;
    }

    public void SetGridIndex(int rowIndex, int columnIndex)
    {
        GridIndex = new Vector2(columnIndex, rowIndex);
    }

    public void SetCost(int unlockCountNumber)
    {
        var costData = _cellData.Cost;
        Cost = costData[unlockCountNumber];

        if (unlockCountNumber > costData.Length)
        {
            Cost = _cellData.Cost[costData.Length - 1] * 2;
        }
    }
}
}
