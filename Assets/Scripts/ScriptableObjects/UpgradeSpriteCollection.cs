using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FirstUpgradeSpriteCollection", menuName = "FirstUpgradeSpritesScriptableObject")]

public class FirstUpgradeSpriteCollection : ScriptableObject
{
    public List<Sprite> UpgradeSprites = new List<Sprite>();
}

[CreateAssetMenu(fileName = "SecondUpgradeSpriteCollection", menuName = "SecondUpgradeSpritesScriptableObject")]

public class SecondUpgradeSpriteCollection : ScriptableObject
{
    public List<Sprite> UpgradeSprites = new List<Sprite>();
}

[CreateAssetMenu(fileName = "ThirdUpgradeSpriteCollection", menuName = "ThirdUpgradeSpritesScriptableObject")]

public class ThirdUpgradeSpriteCollection : ScriptableObject
{
    public List<Sprite> UpgradeSprites = new List<Sprite>();
}