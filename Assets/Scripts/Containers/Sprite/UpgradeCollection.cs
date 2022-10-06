using System.Collections.Generic;
using UnityEngine;

namespace ShopTown.SpriteContainer
{
public class UpgradeCollection : ScriptableObject
{
    public List<Sprite> FirstLevelSprites = new List<Sprite>();
    public List<Sprite> SecondLevelSprites = new List<Sprite>();
    public List<Sprite> ThirdLevelSprites = new List<Sprite>();
}
}
