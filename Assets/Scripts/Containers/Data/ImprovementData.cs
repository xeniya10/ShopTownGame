using System.Collections.Generic;
using ShopTown.ModelComponent;
using UnityEngine;

namespace ShopTown.Data
{
[CreateAssetMenu(fileName = "ImprovementData", menuName = "ImprovementData")]
public class ImprovementData : ScriptableObject, IImprovementData
{
    [SerializeField] private BusinessData _businessNames;
    [SerializeField] private ImprovementModel _defaultModel = new ImprovementModel();

    [Header("Manager Data")]
    [SerializeField] private List<string> _managerNames = new List<string>();
    [SerializeField] private List<MoneyModel> _managerCost = new List<MoneyModel>();

    [Header("Upgrade Data")]
    [SerializeField] private List<string> _firstLevelUpgradeNames = new List<string>();
    [SerializeField] private List<string> _secondLevelUpgradeNames = new List<string>();
    [SerializeField] private List<string> _thirdLevelUpgradeNames = new List<string>();
    [SerializeField] private List<MoneyModel> _upgradeCost = new List<MoneyModel>();

    public string GetBusinessName(int level)
    {
        return _businessNames.GetName(level);
    }

    public ImprovementModel GetDefaultModel()
    {
        return _defaultModel;
    }

    public string GetManagerName(int level)
    {
        if (_managerNames.Count == 0 || level > _managerNames.Count)
        {
            Debug.Log("List of Manager Names is empty or input level greater, then count of list");
            return null;
        }

        return _managerNames[level - 1];
    }

    public MoneyModel GetManagerCost(int level)
    {
        if (_managerCost.Count == 0 || level > _managerCost.Count)
        {
            Debug.Log("List of Manager Cost is empty or input level greater, then count of list");
            return null;
        }

        return _managerCost[level - 1];
    }

    public string GetUpgradeNames(int level, int improvementLevel)
    {
        switch (improvementLevel)
        {
            case 1: return GetFirstLevelUpgradeNames(level);

            case 2: return GetSecondLevelUpgradeNames(level);

            case 3: return GetThirdLevelUpgradeNames(level);

            default: return null;
        }
    }

    private string GetFirstLevelUpgradeNames(int level)
    {
        if (_firstLevelUpgradeNames.Count == 0 || level > _firstLevelUpgradeNames.Count)
        {
            Debug.Log("List of Upgrade Names (1 lvl) is empty or input level greater, then count of list");
            return null;
        }

        return _firstLevelUpgradeNames[level - 1];
    }

    private string GetSecondLevelUpgradeNames(int level)
    {
        if (_secondLevelUpgradeNames.Count == 0 || level > _secondLevelUpgradeNames.Count)
        {
            Debug.Log("List of Upgrade Names (2 lvl) is empty or input level greater, then count of list");
            return null;
        }

        return _secondLevelUpgradeNames[level - 1];
    }

    private string GetThirdLevelUpgradeNames(int level)
    {
        if (_thirdLevelUpgradeNames.Count == 0 || level > _thirdLevelUpgradeNames.Count)
        {
            Debug.Log("List of Upgrade Names (3 lvl) is empty or input level greater, then count of list");
            return null;
        }

        return _thirdLevelUpgradeNames[level - 1];
    }

    public MoneyModel GetUpgradeCost(int level)
    {
        if (_upgradeCost.Count == 0 || level > _upgradeCost.Count)
        {
            Debug.Log("List of Upgrade Cost is empty or input level greater, then count of list");
            return null;
        }

        return _upgradeCost[level - 1];
    }
}
}
