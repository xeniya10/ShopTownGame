using System;
using ShopTown.Data;
using VContainer;

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
    // public Vector2 GridIndex;
    // public Vector2 Position;    
    public int[] GridIndex;
    public float[] Position;

    // Time Parameters
    public TimeSpan CurrentTime;
    public TimeSpan TotalTime;

    // State Parameters
    public CellState State;
    public bool IsManagerActivated;
    public int ActivatedUpgradeLevel;
    public bool AreAllUpgradeLevelsActivated;

    // Monetary Parameters
    public int ActivationNumber;
    public MoneyModel Cost;
    public MoneyModel Profit;

    // Data Containers
    private GameCellData _cellData;

    [Inject]
    public void Inject(GameCellData cellData)
    {
        _cellData = cellData;
    }

    public void SetState(CellState state)
    {
        State = state;
        switch (State)
        {
            case CellState.Lock:
                Reset();
                break;

            case CellState.Unlock:
                Reset();
                break;

            case CellState.Active:
                SetParameters();
                break;

            case CellState.InProgress:
                break;
        }
    }

    private void Reset()
    {
        Level = 0;
        CurrentTime = TimeSpan.Zero;
        IsManagerActivated = false;
        ActivatedUpgradeLevel = 0;
        AreAllUpgradeLevelsActivated = false;
        SetParameters();
    }

    private void SetParameters()
    {
        SetTime();
        SetProfit();
        SetCost();
    }

    public void SetGridIndex(int rowIndex, int columnIndex)
    {
        // GridIndex = new Vector2(columnIndex, rowIndex);
        GridIndex = new[] {columnIndex, rowIndex};
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
