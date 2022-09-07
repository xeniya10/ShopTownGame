using System;

public class GameCellModel
{
    public string Name { get; set; }
    public int Id { get; set; }
    public int Cost { get; set; }
    public int Profit { get; set; }
    public DateTime UnlockedCellData { get; set; }
    public bool CellUnlocked { get; set; }
    public bool ManagerUnlocked { get; set; }
    public bool UpgradeUnlocked { get; set; }
    public float ProfitMultiplier { get; set; }
    public float TimeMultiplier { get; set; }
    public float TimerInSeconds { get; set; }
    public float TimerCurrent { get; set; }
}
