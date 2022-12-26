using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShopTown.SpriteContainer
{
[CreateAssetMenu(fileName = "ImprovementSprites", menuName = "ImprovementSprites")]
public class ImprovementContainer : ScriptableObject, IImprovementSprites
{
    [Header("Manager Sprites")]
    [SerializeField] private List<Sprite> _managerSprites = new List<Sprite>();

    [Header("Upgrade Sprites")]
    [SerializeField] private List<Sprite> _firstLevelUpgradeSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> _secondLevelUpgradeSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> _thirdLevelUpgradeSprites = new List<Sprite>();

    public Sprite GetManagerSprites(int level)
    {
        if (_managerSprites == null || _managerSprites.Count == 0 || level > _managerSprites.Count)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetManagerSprites)}: List is null/empty or input level: {level} greater, then count of list");
        }

        return _managerSprites[level - 1];
    }

    public Sprite GetUpgradeSprites(int level, int improvementLevel)
    {
        switch (improvementLevel)
        {
            case 1: return GetFirstLevelUpgradeSprites(level);

            case 2: return GetSecondLevelUpgradeSprites(level);

            case 3: return GetThirdLevelUpgradeSprites(level);

            default:
                throw new ArgumentException(
                    $"{GetType().Name}.{nameof(GetUpgradeSprites)}: Unknown improvement level: {improvementLevel}");
        }
    }

    private Sprite GetFirstLevelUpgradeSprites(int level)
    {
        if (_firstLevelUpgradeSprites == null || _firstLevelUpgradeSprites.Count == 0 ||
            level > _firstLevelUpgradeSprites.Count)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetFirstLevelUpgradeSprites)}: List is null/empty or input level: {level} greater, then count of list");
        }

        return _firstLevelUpgradeSprites[level - 1];
    }

    private Sprite GetSecondLevelUpgradeSprites(int level)
    {
        if (_secondLevelUpgradeSprites == null || _secondLevelUpgradeSprites.Count == 0 ||
            level > _secondLevelUpgradeSprites.Count)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetSecondLevelUpgradeSprites)}: List is null/empty or input level: {level} greater, then count of list");
        }

        return _secondLevelUpgradeSprites[level - 1];
    }

    private Sprite GetThirdLevelUpgradeSprites(int level)
    {
        if (_thirdLevelUpgradeSprites == null || _thirdLevelUpgradeSprites.Count == 0 ||
            level > _thirdLevelUpgradeSprites.Count)
        {
            throw new NullReferenceException(
                $"{GetType().Name}.{nameof(GetThirdLevelUpgradeSprites)}: List is null/empty or input level: {level} greater, then count of list");
        }

        return _thirdLevelUpgradeSprites[level - 1];
    }
}
}
