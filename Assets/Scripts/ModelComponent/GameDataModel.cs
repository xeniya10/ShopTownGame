using System;

public class GameDataModel
{
    public float CurrentBalance;
    public float TotalBalance;
    public int MaxOpenedLevel;
    public DateTime TimeStamp;
    public GameSettingModel Settings;

    // List Businesses
    // List Managers
    // List Upgrades

    public void AddToBalance(float number)
    {
        CurrentBalance = CurrentBalance + number;
    }
    public void SubtractFromBalance(float number)
    {
        CurrentBalance = CurrentBalance - number;
    }
    public bool CanBuy()
    {
        return true;
    }
}
