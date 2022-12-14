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
    public int ActivationNumber;

    public SettingModel Settings;

    [NonSerialized] public Action ChangeEvent;

    public void SetDefaultData(GameDataModel defaultData)
    {
        DollarBalance = new MoneyModel(defaultData.DollarBalance.Number, defaultData.DollarBalance.Value);
        GoldBalance = new MoneyModel(defaultData.GoldBalance.Number, defaultData.GoldBalance.Value);
        MinLevel = defaultData.MinLevel;
        MaxLevel = defaultData.MaxLevel;
        MaxOpenedLevel = defaultData.MaxOpenedLevel;
        MaxUpgradeLevel = defaultData.MaxUpgradeLevel;
        ActivationNumber = defaultData.ActivationNumber;

        Settings = new SettingModel();
        Settings.SetDefaultData(defaultData.Settings);
    }

    public void AddToOfflineBalance(MoneyModel profit)
    {
        if (OfflineDollarBalance == null)
        {
            OfflineDollarBalance = new MoneyModel(profit.Number, profit.Value);
            return;
        }

        OfflineDollarBalance.Number += profit.Number;
    }

    public void AddToBalance(MoneyModel number)
    {
        if (number.Value == Currency.Dollar)
        {
            DollarBalance.Number += number.Number;
            ChangeEvent?.Invoke();
            return;
        }

        GoldBalance.Number += number.Number;
        ChangeEvent?.Invoke();
    }

    private void SubtractFromBalance(MoneyModel number)
    {
        if (number.Value == Currency.Dollar)
        {
            DollarBalance.Number -= number.Number;
            return;
        }

        GoldBalance.Number -= number.Number;
    }

    public bool CanBuy(MoneyModel number)
    {
        if (number.Number > DollarBalance.Number)
        {
            return false;
        }

        SubtractFromBalance(number);
        ChangeEvent?.Invoke();
        return true;
    }

    public void SetActivationNumber(int number)
    {
        ActivationNumber = number;
        ChangeEvent?.Invoke();
    }

    public void SetMaxOpenedLevel(int maxLevel)
    {
        MaxOpenedLevel = maxLevel;
        ChangeEvent?.Invoke();
    }
}
}
