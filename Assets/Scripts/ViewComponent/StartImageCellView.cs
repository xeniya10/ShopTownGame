using UnityEngine;
using UnityEngine.UI;

public class StartImageCellView : MonoBehaviour
{
    [SerializeField] private RectTransform _cellRectTransform;
    [SerializeField] private Image _cellImage;
    [SerializeField] private CellSpriteCollection _cellSpriteCollection;
    [HideInInspector] public Vector2 StartPosition { get; set; }
    [HideInInspector] public Vector2 TargetPosition { get; set; }

    public void SetSprite(int i)
    {
        _cellImage.sprite = _cellSpriteCollection.CellSprites[i];
    }

    public void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public void SetSize(float size)
    {
        _cellRectTransform.sizeDelta = new Vector2(size, size);
    }

    public StartImageCellView Create(Transform parent)
    {
        var cell = Instantiate(this, parent);
        cell.SetPosition(cell.StartPosition);

        return cell;
    }
}
