using UnityEngine;

namespace ShopTown.SpriteContainer
{
public interface IImprovementSprites
{
    public Sprite GetManagerSprites(int level);

    public Sprite GetUpgradeSprites(int level, int improvementLevel);
}
}
