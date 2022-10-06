using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;
using DG.Tweening;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;

public class GameCellView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private GameObject _lockObject;
    [SerializeField] private RectTransform _cellRect;

    [Space]
    public Button CellButton;
    public Button BuyButton;

    [Space]
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _businessImage;
    [SerializeField] private Image _progressImage;
    [SerializeField] private Image _unlockImage;
    [SerializeField] private Image _lockImage;
    public Image _selectorImage;

    [Space]
    [SerializeField] private TextMeshProUGUI _progressTimeText;
    [SerializeField] private TextMeshProUGUI _priceText;

    [Header("Collections")]
    [SerializeField] private BackgroundCollection _backgroundCollection;
    [SerializeField] private BusinessCollection _businessCollection;

    [Header("Animation Duration")]
    [SerializeField] private float _fadeTime;

    public int RandomBackgroundNumber()
    {
        var randomNumber = Random.Range(0, _backgroundCollection.Sprites.Count);
        return randomNumber;
    }

    private void SetBackgroundSprite(int i)
    {
        _backgroundImage.sprite = _backgroundCollection.Sprites[i];
    }

    private void SetBusinessSprite(int level)
    {
        _businessImage.sprite = _businessCollection.Sprites[level - 1];
    }

    public void SetCost(double cost)
    {
        _priceText.text = MoneyFormatUtility.Default(cost);
    }

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public void SetSize(float size)
    {
        _cellRect.sizeDelta = new Vector2(size, size);
    }

    public GameCellView Create(Transform parent)
    {
        var cell = Instantiate(this, parent);
        return cell;
    }

    public void Initialize(GameCellModel model)
    {
        SetBusinessSprite(model.Level);
        SetBackgroundSprite(model.BackgroundNumber);
        SetPosition(model.Position);
        SetCost(model.Cost);
        ChangeState(model.State);
    }

    public void HideBusiness(Action callBack)
    {
        AnimationUtility.Fade(_businessImage, 0, _fadeTime, null, () => callBack?.Invoke());
    }

    public void SetLockState()
    {
        _progressBar.SetActive(false);
        _lockObject.SetActive(true);
        HideBusiness(null);
        AnimationUtility.Fade(_lockImage, 1, _fadeTime, null, null);
    }

    public void SetUnlockState()
    {
        var sequence = DOTween.Sequence();

        AnimationUtility.Fade(_businessImage, 0, _fadeTime, sequence, null);
        AnimationUtility.Fade(_lockImage, 0, _fadeTime, sequence, () => _lockObject.SetActive(false));
        AnimationUtility.Fade(_unlockImage, 1, _fadeTime, sequence, null);

        _unlockImage.gameObject.SetActive(true);
        sequence.Play();
    }

    public void SetActiveState()
    {
        var sequence = DOTween.Sequence();

        AnimationUtility.Fade(_unlockImage, 0, _fadeTime, sequence, () => _unlockImage.gameObject.SetActive(false));
        AnimationUtility.Fade(_businessImage, 1, _fadeTime, sequence, null);

        sequence.Play();
    }

    private void ChangeState(CellState state)
    {
        switch (state)
        {
            case CellState.Lock:
                SetLockState();
                break;

            case CellState.Unlock:
                SetUnlockState();
                break;

            case CellState.Active:
                SetActiveState();
                break;
        }
    }

    public void SetInProgressState(double totalTime, Action callBack)
    {
        _progressBar.SetActive(true);
        AnimationUtility.Fill(_progressImage, _progressTimeText, (float)totalTime, null, () =>
        {
            _progressImage.fillAmount = 1;
            _progressBar.SetActive(false);
            callBack?.Invoke();
        });
    }
}
