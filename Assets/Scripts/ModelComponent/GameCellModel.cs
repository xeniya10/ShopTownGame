using System;

public class GameCellModel
{
    public string Name;
    public int Level;
    public int BackgroundNumber;

    public double BaseCost;
    public double Cost;
    public double BaseProfit;
    public double Profit;

    public DateTime UnlockedCellData;
    public bool CellLocked = false;
    public bool CellUnlocked = false;

    public bool ManagerUnlocked = false;
    public bool FirstUpgradeUnlocked = false;
    public bool SecondUpgradeUnlocked = false;
    public bool ThirdUpgradeUnlocked = false;

    public float ProfitMultiplier = 1;
    public float TimeMultiplier = 1;
    public float TimerCurrent = 0;
    public float TimerInSeconds;

}
