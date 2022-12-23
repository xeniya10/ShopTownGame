using UnityEngine;

namespace ShopTown.SpriteContainer
{
public interface IGameCellSprites
{
    public Sprite GetBusinessSprites(int level);

    public Sprite GetBackgroundSprites(int number);

    public int GetBackgroundSpritesCount();
}
}
