using System;
using System.Collections.Generic;

namespace ShopTown.ModelComponent
{
public class GameDataModel
{
    // Monetary Parameters
    public MoneyModel CurrentMoneyBalance;
    // public MoneyModel TotalMoneyBalance;
    public MoneyModel CurrentGoldBalance;

    // Level Parameters
    public int MinLevel;
    public int MaxLevel;
    public int MaxOpenedLevel;
    public int MaxUpgradeLevel;
    public int ActivationNumber;

    // Time
    public DateTime TimeStamp;

    // Models
    public GameSettingModel Settings;
    public GameBoardModel GameBoardModel; // to save row and column numbers

    // Lists
    public List<GameCellModel> Businesses;
    public List<ManagerRowModel> Managers;
    public List<UpgradeRowModel> Upgrades;

    [NonSerialized]
    public Action BalanceChangeEvent;

    public void AddToBalance(MoneyModel number)
    {
        if (number.Value == Currency.Dollar)
        {
            CurrentMoneyBalance.Number += number.Number;
            BalanceChangeEvent?.Invoke();
            return;
        }

        CurrentGoldBalance.Number += number.Number;
        BalanceChangeEvent?.Invoke();
    }

    private void SubtractFromBalance(MoneyModel number)
    {
        if (number.Value == Currency.Dollar)
        {
            CurrentMoneyBalance.Number -= number.Number;
            return;
        }

        CurrentGoldBalance.Number -= number.Number;
    }

    public bool CanBuy(MoneyModel number)
    {
        if (number.Number > CurrentMoneyBalance.Number)
        {
            return false;
        }

        SubtractFromBalance(number);
        BalanceChangeEvent?.Invoke();
        return true;
    }
}
}
