using System;
using System.Collections.Generic;

namespace ShopTown.ModelComponent
{
public enum Currency { Dollar, Gold }

[Serializable]
public class MoneyModel
{
    public double Number;
    public Currency Value;

    public MoneyModel(double number, Currency value)
    {
        Number = number;
        Value = value;
    }
}

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

    public void SetBalance(MoneyModel number)
    {
        if (number.Value == Currency.Dollar)
        {
            CurrentMoneyBalance = number.Number;
        }

        else
        {
            CurrentGoldBalance = number.Number;
        }

        BalanceChangeEvent.Invoke();
    }

    public void AddToBalance(MoneyModel number)
    {
        if (number.Value == Currency.Dollar)
        {
            CurrentMoneyBalance += number.Number;
        }

        else
        {
            CurrentGoldBalance += number.Number;
        }

        BalanceChangeEvent.Invoke();
    }

    private void SubtractFromBalance(MoneyModel number)
    {
        if (number.Value == Currency.Dollar)
        {
            CurrentMoneyBalance -= number.Number;
        }

        else
        {
            CurrentGoldBalance -= number.Number;
        }

        BalanceChangeEvent.Invoke();
    }

    public bool CanBuy(MoneyModel number)
    {
        if (number.Number > CurrentMoneyBalance)
        {
            return false;
        }

        SubtractFromBalance(number);
        return true;
    }
}
}
