using System.Collections.Generic;
using UnityEngine;

namespace ShopTown.SpriteContainer
{
public class UpgradeCollection : ScriptableObject
{
    public List<Sprite> FirstLevelSprites = new();
    public List<Sprite> SecondLevelSprites = new();
    public List<Sprite> ThirdLevelSprites = new();
}
}
