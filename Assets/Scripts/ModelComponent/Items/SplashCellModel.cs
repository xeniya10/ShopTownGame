using UnityEngine;

namespace ShopTown.ModelComponent
{
public class SplashCellModel
{
    public float Size;
    public int SpriteNumber;
    public Vector2 StartPosition;
    public Vector2 TargetPosition;

    public SplashCellModel(int spriteNumber, float size, Vector2 startPosition, Vector2 targetPosition)
    {
        SpriteNumber = spriteNumber;
        Size = size;
        StartPosition = startPosition;
        TargetPosition = targetPosition;
    }
}
}
