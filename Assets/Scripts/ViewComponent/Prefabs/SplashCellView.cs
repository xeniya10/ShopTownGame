using DG.Tweening;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class SplashCellView : MonoBehaviour, ISplashCellView
{
    [SerializeField] private RectTransform _cellRectTransform;
    [SerializeField] private Image _cellImage;
    [SerializeField] private SplashCellContainer _splashCellContainer;

    [Header("Animation Durations")]
    [SerializeField] private float _scaleTime;
    [SerializeField] private float _moveTime;

    private Vector2 _startPosition;
    private Vector2 _targetPosition;

    public ISplashCellView Create(Transform parent)
    {
        var cell = Instantiate(this, parent);
        return cell;
    }

    public void Initialize(SplashCellModel model)
    {
        SetSprite(model.SpriteNumber);
        SetSize(model.Size);
        _startPosition = model.StartPosition;
        _targetPosition = model.TargetPosition;
        SetPosition(_startPosition);
    }

    public void AppearAnimation(Sequence sequence)
    {
        transform.Move(_targetPosition, _moveTime * 0.5f, sequence);
    }

    public void DisappearAnimation(Sequence sequence)
    {
        var scale = new Vector2(0, 0);
        transform.Scale(scale, _scaleTime, sequence);
    }

    private void SetSprite(int i)
    {
        _cellImage.sprite = _splashCellContainer.Sprites[i];
    }

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    private void SetSize(float size)
    {
        _cellRectTransform.sizeDelta = new Vector2(size, size);
    }
}

public interface ISplashCellView : IInitializable<SplashCellModel>, ICreatable<ISplashCellView>, ISplashAnimation
{}

public interface ISplashAnimation
{
    void AppearAnimation(Sequence sequence);

    void DisappearAnimation(Sequence sequence);
}
}
