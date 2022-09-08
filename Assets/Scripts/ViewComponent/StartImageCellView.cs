using UnityEngine;
using UnityEngine.UI;

public class StartImageCellView : MonoBehaviour
{
    public RectTransform CellRectTransform;
    public Image CellImage;
    public CellSpriteCollection CellSpriteCollection;
    public Vector2 StartPosition { get; set; }
    public Vector2 TargetPosition { get; set; }

    public void SetSprite(int i)
    {
        CellImage.sprite = CellSpriteCollection.CellSprites[i];
    }

    public void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public void SetSize(float size)
    {
        CellRectTransform.sizeDelta = new Vector2(size, size);
    }
}
