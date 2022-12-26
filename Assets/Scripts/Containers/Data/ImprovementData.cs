using System;
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
        if (_businessNames == null)
        {
            throw new NullReferenceException($"{GetType().Name}.{nameof(GetBusinessName)}: Object is null");
        }

        return _businessNames.GetName(level);
    }

    public ImprovementModel GetDefaultModel()
    {
        if (_defaultModel == null)
        {
            throw new NullReferenceException($"{GetType().Name}.{nameof(GetDefaultModel)}: Object is null");
        }

        return _defaultModel;
    }

    public string GetManagerName(int level)
    {
        if (_managerNames == null || _managerNames.Count == 0 || level > _managerNames.Count)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetManagerName)}: List is null/empty or input level: {level} greater, then count of list");
        }

        return _managerNames[level - 1];
    }

    public MoneyModel GetManagerCost(int level)
    {
        if (_managerCost == null || _managerCost.Count == 0 || level > _managerCost.Count)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetManagerCost)}: List is null/empty or input level: {level} greater, then count of list");
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

            default:
                throw new ArgumentException(
                    $"{GetType().Name}.{nameof(GetUpgradeNames)}: Unknown improvement level: {improvementLevel}");
        }
    }

    private string GetFirstLevelUpgradeNames(int level)
    {
        if (_firstLevelUpgradeNames == null || _firstLevelUpgradeNames.Count == 0 ||
            level > _firstLevelUpgradeNames.Count)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetFirstLevelUpgradeNames)}: List is null/empty or input level: {level} greater, then count of list");
        }

        return _firstLevelUpgradeNames[level - 1];
    }

    private string GetSecondLevelUpgradeNames(int level)
    {
        if (_secondLevelUpgradeNames == null || _secondLevelUpgradeNames.Count == 0 ||
            level > _secondLevelUpgradeNames.Count)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetSecondLevelUpgradeNames)}: List is null/empty or input level: {level} greater, then count of list");
        }

        return _secondLevelUpgradeNames[level - 1];
    }

    private string GetThirdLevelUpgradeNames(int level)
    {
        if (_thirdLevelUpgradeNames == null || _thirdLevelUpgradeNames.Count == 0 ||
            level > _thirdLevelUpgradeNames.Count)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetThirdLevelUpgradeNames)}: List is null/empty or input level: {level} greater, then count of list");
        }

        return _thirdLevelUpgradeNames[level - 1];
    }

    public MoneyModel GetUpgradeCost(int level)
    {
        if (_upgradeCost == null || _upgradeCost.Count == 0 || level > _upgradeCost.Count)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetUpgradeCost)}: List is null/empty or input level: {level} greater, then count of list");
        }

        return _upgradeCost[level - 1];
    }
}
}
