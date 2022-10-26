using System;
using System.Collections.Generic;

namespace ShopTown.ModelComponent
{
public class GameDataModel
{
    // Monetary Parameters
    public MoneyModel CurrentMoneyBalance;
    public MoneyModel TotalMoneyBalance;
    public MoneyModel CurrentGoldBalance;
    public MoneyModel TotalGoldBalance;

    // Level Parameters
    public int MinLevel;
    public int MaxLevel;
    public int MaxOpenedLevel;
    public int MaxUpgradeLevel;

    // Time
    public DateTime TimeStamp;

    // Models
    public GameSettingModel Settings;
    public GameBoardModel GameBoardModel; // to save row and column numbers

    public List<GameCellModel> Businesses;
    public List<ManagerRowModel> Managers;
    public List<UpgradeRowModel> Upgrades;

    public Action BalanceChangeEvent;

    public void SetBalance(MoneyModel number)
    {
        if (number.Value == Currency.Dollar)
        {
            CurrentMoneyBalance = new MoneyModel(number.Number, number.Value);
        }

        else
        {
            CurrentGoldBalance = new MoneyModel(number.Number, number.Value);
        }

        BalanceChangeEvent.Invoke();
    }

    public void AddToBalance(MoneyModel number)
    {
        if (number.Value == Currency.Dollar)
        {
            CurrentMoneyBalance.Number += number.Number;
        }

        else
        {
            CurrentGoldBalance.Number += number.Number;
        }

        BalanceChangeEvent.Invoke();
    }

    private void SubtractFromBalance(MoneyModel number)
    {
        if (number.Value == Currency.Dollar)
        {
            CurrentMoneyBalance.Number -= number.Number;
        }

        else
        {
            CurrentGoldBalance.Number -= number.Number;
        }

        BalanceChangeEvent.Invoke();
    }

    public bool CanBuy(MoneyModel number)
    {
        if (number.Number > CurrentMoneyBalance.Number)
        {
            return false;
        }

        SubtractFromBalance(number);
        return true;
    }
}
}
