using System;
using UnityEngine;

namespace ShopTown.ModelComponent
{
public enum CellState { Lock, Unlock, Active, InProgress }

[Serializable]
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
    public DateTime StartTime;
    public TimeSpan TotalTime;

    // State Parameters
    public CellState State;
    public bool IsManagerActivated;
    public int ActivatedUpgradeLevel;
    public bool AreAllUpgradeLevelsActivated;

    // Monetary Parameters
    public MoneyModel Cost;
    public MoneyModel Profit;

    public void SetDefaultData(GameCellModel defaultModel)
    {
        Level = defaultModel.Level;
        BackgroundNumber = defaultModel.BackgroundNumber;
        State = defaultModel.State;
        IsManagerActivated = defaultModel.IsManagerActivated;
        ActivatedUpgradeLevel = defaultModel.ActivatedUpgradeLevel;
        AreAllUpgradeLevelsActivated = defaultModel.AreAllUpgradeLevelsActivated;
    }
}

public class SplashCellModel
{
    public float Size;
    public int SpriteNumber;
    public Vector2 StartPosition;
    public Vector2 TargetPosition;

    public SplashCellModel(int spriteNumber, float size, Vector2 startPosition, Vector2 targetPosition)
    {
        SpriteNumber = spriteNumber;
        Size = size;
        StartPosition = startPosition;
        TargetPosition = targetPosition;
    }
}
}
