using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellBackgroundSpriteCollection", menuName = "CellBackgroundSpritesScriptableObject")]

public class CellBackgroundSpriteCollection : ScriptableObject
{
    public List<Sprite> CellBackgroundSprites = new List<Sprite>();
}