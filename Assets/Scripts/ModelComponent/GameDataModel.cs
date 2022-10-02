using System;
using System.Collections.Generic;

[Serializable]
public class GameDataModel
{
    public double CurrentMoneyBalance;
    public double TotalMoneyBalance;
    public int CurrentGoldBalance;
    public int TotalGoldBalance;
    public int MaxOpenedLevel;
    public int NumberOfLevels;

    public DateTime TimeStamp;
    public GameSettingModel Settings;
    public GameBoardModel GameBoardModel; // to save row and column numbers

    public List<GameCellModel> Businesses;
    public List<ManagerRowModel> Managers;
    public List<UpgradeRowModel> Upgrades;

    public Action BalanceChangeEvent;

    public void SetBalance(double amount)
    {
        CurrentMoneyBalance = amount;
        // Update text field
    }

    public void AddToBalance(double amount)
    {
        CurrentMoneyBalance += amount;
        // Update text field
    }
    public void SubtractFromBalance(double amount)
    {
        CurrentMoneyBalance -= amount;
        // Update text field
    }
    public bool CanBuy(double amount)
    {
        if (amount > CurrentMoneyBalance)
        {
            return false;
        }

        SubtractFromBalance(amount);
        return true;
    }
}
