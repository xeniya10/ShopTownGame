using System;
using UnityEngine;

public enum CellState
{
    Lock,
    Unlock,
    Active
}

[Serializable]
public class GameCellModel
{
    public int Level;
    public string BusinessName { get { return _business.LevelNames[Level - 1]; } }
    public int BackgroundNumber;

    public double Cost = 2;
    public double BaseProfit { get { return _cellData.BaseProfit[Level - 1]; } }
    public double Profit { get { return BaseProfit * ProfitMultiplier; } }
    public float CellSize;
    public Vector2 GridIndexes;
    public Vector2 Position;

    public DateTime ActivatingDate;
    public CellState State = CellState.Lock;

    public bool IsUnlockedManager = false;
    public bool IsUnlockedUpgrade = false;
    public int UpgradeLevel = 0;

    public TimeSpan ProcessTime { get { return _cellData.ProcessTime[Level - 1]; } }
    public float ProfitMultiplier { get { return UpgradeLevel + 1; } }

    private readonly BusinessData _business;
    private readonly GameCellData _cellData;

    public GameCellModel(BusinessData business, GameCellData cellData)
    {
        _business = business;
        _cellData = cellData;
    }

    public void SetGridInexes(int rowIndex, int columnIndex)
    {
        GridIndexes = new Vector2(columnIndex, rowIndex);
    }

    public void SetCost(int unlockCountNumber)
    {
        Cost = _cellData.Cost[unlockCountNumber];

        if (unlockCountNumber > _cellData.Cost.Length)
        {
            Cost = _cellData.Cost[_cellData.Cost.Length - 1] * 2;
        }
    }
}