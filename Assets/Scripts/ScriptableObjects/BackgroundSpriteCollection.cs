using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundSpriteCollection", menuName = "BackgroundSpritesScriptableObject")]

public class BackgroundSpriteCollection : ScriptableObject
{
    public List<Sprite> BackgroundSprites = new List<Sprite>();
}
