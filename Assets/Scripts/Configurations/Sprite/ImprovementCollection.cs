using System.Collections.Generic;
using UnityEngine;

namespace ShopTown.SpriteContainer
{
[CreateAssetMenu(fileName = "ImprovementSprites", menuName = "ImprovementSprites")]
public class ImprovementCollection : ScriptableObject
{
    [Header("Manager Sprites")]
    public List<Sprite> ManagerSprites = new List<Sprite>();

    [Header("Upgrade Sprites")]
    public List<Sprite> FirstLevelUpgradeSprites = new List<Sprite>();
    public List<Sprite> SecondLevelUpgradeSprites = new List<Sprite>();
    public List<Sprite> ThirdLevelUpgradeSprites = new List<Sprite>();
}
}
