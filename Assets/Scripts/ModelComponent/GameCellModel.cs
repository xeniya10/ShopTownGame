using System;

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
    public int[] GridIndex;
    public float[] Position;

    // In Progress Time Parameters
    public TimeSpan StartTime;
    public TimeSpan TotalTime;

    // State Parameters
    public CellState State;
    public bool IsManagerActivated;
    public int ActivatedUpgradeLevel;
    public bool AreAllUpgradeLevelsActivated;

    // Monetary Parameters
    public MoneyModel Cost;
    public MoneyModel Profit;

    public void Initialize(int level, TimeSpan startTime, bool isManagerActivated = false, int upgradeLevel = 0,
        bool areAllUpgradeLevelsActivated = false)
    {
        Level = level;
        StartTime = startTime;
        IsManagerActivated = isManagerActivated;
        ActivatedUpgradeLevel = upgradeLevel;
        AreAllUpgradeLevelsActivated = areAllUpgradeLevelsActivated;
    }

    public void SetState(CellState state)
    {
        State = state;
        switch (State)
        {
            case CellState.Lock:
                Initialize(0, TimeSpan.Zero);
                break;

            case CellState.Unlock:
                Initialize(0, TimeSpan.Zero);
                break;
        }
    }

    public void SetGridIndex(int rowIndex, int columnIndex)
    {
        GridIndex = new[] {columnIndex, rowIndex};
    }
}
}
