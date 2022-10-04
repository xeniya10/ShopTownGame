using System;
using System.Collections.Generic;

namespace ShopTown.ModelComponent
{
public class GameDataModel
{
    // Monetary Parameters
    public double CurrentMoneyBalance;
    public double TotalMoneyBalance;
    public double CurrentGoldBalance;
    public double TotalGoldBalance;

    // Level Parameters
    public int MaxOpenedLevel;
    public int NumberOfLevels;

    // Models
    public GameSettingModel Settings;
    public GameBoardModel GameBoardModel; // to save row and column numbers

    public List<GameCellModel> Businesses;
    public List<ManagerRowModel> Managers;
    public List<UpgradeRowModel> Upgrades;

    public Action BalanceChangeEvent;

    public DateTime TimeStamp;

    public void SetMoneyBalance(double amount)
    {
        CurrentMoneyBalance = amount;
        BalanceChangeEvent.Invoke();
        // Update text field
    }

    public void AddToMoneyBalance(double amount)
    {
        CurrentMoneyBalance += amount;
        BalanceChangeEvent.Invoke();
        // Update text field
    }

    public void SubtractFromBalance(double amount)
    {
        CurrentMoneyBalance -= amount;
        BalanceChangeEvent.Invoke();
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
}
