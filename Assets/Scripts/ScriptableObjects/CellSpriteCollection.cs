using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellSpriteCollection", menuName = "CellSpritesScriptableObject")]

public class CellSpriteCollection : ScriptableObject
{
    public List<Sprite> CellSprites = new List<Sprite>();
}