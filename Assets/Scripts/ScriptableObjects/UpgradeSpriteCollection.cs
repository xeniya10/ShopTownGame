using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeSpriteCollection", menuName = "UpgradeSpritesScriptableObject")]

public class UpgradeSpriteCollection : ScriptableObject
{
    public List<Sprite> FirstLevelSprites = new List<Sprite>();
    public List<Sprite> SecondLevelSprites = new List<Sprite>();
    public List<Sprite> ThirdLevelSprites = new List<Sprite>();
}