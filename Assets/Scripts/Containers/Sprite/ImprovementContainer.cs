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
        if (_managerSprites.Count == 0 || level > _managerSprites.Count)
        {
            Debug.Log("List of Manager Sprites is empty or input level greater, then count of list");
            return null;
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

            default: return null;
        }
    }

    private Sprite GetFirstLevelUpgradeSprites(int level)
    {
        if (_firstLevelUpgradeSprites.Count == 0 || level > _firstLevelUpgradeSprites.Count)
        {
            Debug.Log("List of Upgrade Sprites (1 lvl) is empty or input level greater, then count of list");
            return null;
        }

        return _firstLevelUpgradeSprites[level - 1];
    }

    private Sprite GetSecondLevelUpgradeSprites(int level)
    {
        if (_secondLevelUpgradeSprites.Count == 0 || level > _secondLevelUpgradeSprites.Count)
        {
            Debug.Log("List of Upgrade Sprites (2 lvl) is empty or input level greater, then count of list");
            return null;
        }

        return _secondLevelUpgradeSprites[level - 1];
    }

    private Sprite GetThirdLevelUpgradeSprites(int level)
    {
        if (_thirdLevelUpgradeSprites.Count == 0 || level > _thirdLevelUpgradeSprites.Count)
        {
            Debug.Log("List of Upgrade Sprites (3 lvl) is empty or input level greater, then count of list");
            return null;
        }

        return _thirdLevelUpgradeSprites[level - 1];
    }
}
}
