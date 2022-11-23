using ShopTown.SpriteContainer;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class SplashCellView : MonoBehaviour
{
    [SerializeField] private RectTransform _cellRectTransform;
    [SerializeField] private Image _cellImage;
    [SerializeField] private SplashCellCollection _splashCellCollection;

    [HideInInspector] public Vector2 StartPosition;
    [HideInInspector] public Vector2 TargetPosition;

    private void SetSprite(int i)
    {
        _cellImage.sprite = _splashCellCollection.Sprites[i];
    }

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public void SetSize(float size)
    {
        _cellRectTransform.sizeDelta = new Vector2(size, size);
    }

    public SplashCellView Create(Transform parent)
    {
        var cell = Instantiate(this, parent);
        return cell;
    }

    public void Initialize(int spriteNumber, Vector2 startPosition, Vector2 targetPosition)
    {
        SetSprite(spriteNumber);
        StartPosition = startPosition;
        TargetPosition = targetPosition;
        SetPosition(StartPosition);
    }
}
}
