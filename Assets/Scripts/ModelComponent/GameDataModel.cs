using System;

namespace ShopTown.ModelComponent
{
[Serializable]
public class GameDataModel
{
    // Monetary Parameters
    public MoneyModel DollarBalance;
    public MoneyModel GoldBalance;
    public MoneyModel OfflineDollarBalance;

    // Level Parameters
    public int MinLevel;
    public int MaxLevel;
    public int MaxOpenedLevel;
    public int MaxUpgradeLevel;
    public int ActivatedCellsNumber;

    public SettingModel Settings;

    [NonSerialized] public Action ChangeEvent;

    public void SetDefaultData(GameDataModel defaultData)
    {
        DollarBalance = new MoneyModel(defaultData.DollarBalance.Value, defaultData.DollarBalance.Currency);
        GoldBalance = new MoneyModel(defaultData.GoldBalance.Value, defaultData.GoldBalance.Currency);
        MinLevel = defaultData.MinLevel;
        MaxLevel = defaultData.MaxLevel;
        MaxOpenedLevel = defaultData.MaxOpenedLevel;
        MaxUpgradeLevel = defaultData.MaxUpgradeLevel;
        ActivatedCellsNumber = defaultData.ActivatedCellsNumber;

        Settings = new SettingModel();
        Settings.SetDefaultData(defaultData.Settings);
    }

    public void AddToOfflineBalance(MoneyModel profit)
    {
        if (OfflineDollarBalance == null)
        {
            OfflineDollarBalance = new MoneyModel(profit.Value, profit.Currency);
            return;
        }

        OfflineDollarBalance.Value += profit.Value;
    }

    public void AddToBalance(MoneyModel number)
    {
        if (number.Currency == CurrencyType.Dollar)
        {
            DollarBalance.Value += number.Value;
            ChangeEvent?.Invoke();
            return;
        }

        GoldBalance.Value += number.Value;
        ChangeEvent?.Invoke();
    }

    private void SubtractFromBalance(MoneyModel number)
    {
        if (number.Currency == CurrencyType.Dollar)
        {
            DollarBalance.Value -= number.Value;
            return;
        }

        GoldBalance.Value -= number.Value;
    }

    public bool CanBuy(MoneyModel number)
    {
        if (number.Value > DollarBalance.Value)
        {
            return false;
        }

        SubtractFromBalance(number);
        ChangeEvent?.Invoke();
        return true;
    }

    public void SetActivationNumber(int number)
    {
        ActivatedCellsNumber = number;
        ChangeEvent?.Invoke();
    }

    public void SetMaxOpenedLevel(int maxLevel)
    {
        MaxOpenedLevel = maxLevel;
        ChangeEvent?.Invoke();
    }
}
}
