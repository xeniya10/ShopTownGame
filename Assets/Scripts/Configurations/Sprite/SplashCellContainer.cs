using System.Collections.Generic;
using UnityEngine;

namespace ShopTown.SpriteContainer
{
[CreateAssetMenu(fileName = "SplashCellSprites", menuName = "SplashCellSprites")]
public class SplashCellContainer : ScriptableObject
{
    public List<Sprite> Sprites = new List<Sprite>();
}
}
