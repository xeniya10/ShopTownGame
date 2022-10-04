using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;
using DG.Tweening;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;
using UnityEngine.Serialization;

public class GameCellView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _progressBar;
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

    public void Initialize(int level, int spriteNumber, Vector2 position,
        double cost, CellState state)
    {
        SetBusinessSprite(level);
        SetBackgroundSprite(spriteNumber);
        SetPosition(position);
        SetCost(cost);
        ChangeState(state);
    }

    public void SetLockState()
    {
        _progressBar.SetActive(false);
        _lockImage.gameObject.SetActive(true);
        _unlockImage.gameObject.SetActive(false);
    }

    public void SetUnlockState()
    {
        var sequence = DOTween.Sequence();

        AnimationUtility.Fade(_lockImage, 0, _fadeTime, sequence, () => _lockImage.gameObject.SetActive(false));
        AnimationUtility.Fade(_unlockImage, 1, _fadeTime, sequence, null);

        _unlockImage.gameObject.SetActive(true);
        sequence.Play();
    }

    public void SetActiveState()
    {
        AnimationUtility.Fade(_unlockImage, 0, _fadeTime, null, () => _unlockImage.gameObject.SetActive(false));
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

    public void Click(Action callBack)
    {
        CellButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void SetProcessState(double totalTime)
    {
        _progressBar.SetActive(true);
        AnimationUtility.Fill(_progressImage, _progressTimeText, (float)totalTime, null, () =>
        {
            _progressImage.fillAmount = 1;
            _progressBar.SetActive(false);
        });
    }
}
