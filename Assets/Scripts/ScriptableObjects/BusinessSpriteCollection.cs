using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BusinessSpriteCollection", menuName = "BusinessSpritesScriptableObject")]

public class BusinessSpriteCollection : ScriptableObject
{
    public List<Sprite> BusinessSprites = new List<Sprite>();
}