using System;

public class GameDataModel
{
    public float CurrentBalance { get; set; }
    public float TotalBalance { get; set; }
    public int MaxOpenedLevel { get; set; }
    public DateTime TimeStamp { get; set; }
    public GameSettingModel Settings { get; set; }

    // row, column

    // List Businesses
    // List Managers
    // List Upgrades

    public void SetBalance(float amount)
    {
        CurrentBalance = amount;
        // Update text field
    }

    public void AddToBalance(float amount)
    {
        CurrentBalance = +amount;
        // Update text field
    }
    public void SubtractFromBalance(float amount)
    {
        CurrentBalance = -amount;
        // Update text field
    }
    public bool CanBuy(float amount)
    {
        if (amount > CurrentBalance)
        {
            return false;
        }

        return true;
    }
}
