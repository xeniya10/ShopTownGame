using System.Collections.Generic;
using UnityEngine;

namespace ShopTown.SpriteContainer
{
[CreateAssetMenu(fileName = "GameCellSprites", menuName = "GameCellSprites")]
public class GameCellContainer : ScriptableObject
{
    public List<Sprite> BusinessSprites = new List<Sprite>();
    public List<Sprite> BackgroundSprites = new List<Sprite>();
}
}
