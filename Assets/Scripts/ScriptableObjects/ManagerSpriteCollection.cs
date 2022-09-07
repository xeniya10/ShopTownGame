using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManagerSpriteCollection", menuName = "ManagerSpritesScriptableObject")]

public class ManagerSpriteCollection : ScriptableObject
{
    public List<Sprite> ManagerSprites = new List<Sprite>();
}