using System.Collections.Generic;
using UnityEngine;

namespace ShopTown.SpriteContainer
{
[CreateAssetMenu(fileName = "SplashCellSprites", menuName = "SplashCellSprites")]
public class SplashCellCollection : ScriptableObject
{
    public List<Sprite> Sprites = new List<Sprite>();
}
}
