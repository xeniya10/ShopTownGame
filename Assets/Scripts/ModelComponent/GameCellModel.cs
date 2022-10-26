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
    InProgress
}

public class GameCellModel
{
    // Description
    public int Level;
    public int BackgroundNumber = int.MinValue;

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
    public bool IsActivatedManager;
    public int UpgradeLevel;
    public bool[] IsUpgradeActivated;

    // Monetary Parameters
    private float ProfitMultiplier { get { return UpgradeLevel + 1; } }
    private MoneyModel BaseProfit { get { return _cellData.BaseProfit[Level - 1]; } }
    public MoneyModel Profit { get { return new MoneyModel(BaseProfit.Number * ProfitMultiplier, BaseProfit.Value); } }
    public MoneyModel Cost;

    // Data Containers
    private readonly GameCellData _cellData;

    public GameCellModel(GameCellData cellData)
    {
        _cellData = cellData;
    }

    public void Reset()
    {
        Level = 0;
    }

    public void SetGridIndex(int rowIndex, int columnIndex)
    {
        GridIndex = new Vector2(columnIndex, rowIndex);
    }

    public void SetCost(int unlockCountNumber)
    {
        var costData = _cellData.Cost;

        if (unlockCountNumber > costData.Length - 1)
        {
            var lastElement = _cellData.Cost[costData.Length - 1];
            Cost = new MoneyModel(lastElement.Number * (unlockCountNumber - costData.Length + 2), lastElement.Value);
            return;
        }

        Cost = costData[unlockCountNumber];
    }
}
}
