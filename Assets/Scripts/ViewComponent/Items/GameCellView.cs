using System;
using System.Collections.Generic;
using DG.Tweening;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace ShopTown.ViewComponent
{
public class GameCellView : MonoBehaviour, IGameCellView
{
    [SerializeField] private RectTransform _cellRect;

    [Header("Game Objects")]
    [SerializeField] private GameObject _manager;
    [SerializeField] private List<GameObject> _upgrades = new List<GameObject>();

    [Header("Buttons")]
    [SerializeField] private Button _cellButton;
    [SerializeField] private Button _buyButton;

    [Header("Images")]
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _businessImage;
    [SerializeField] private Image _selectorImage;
    [SerializeField] private Image _progressImage;
    [SerializeField] private Image _unlockImage;
    [SerializeField] private Image _lockImage;
    [SerializeField] private Image _managerImage;
    [SerializeField] private List<Image> _upgradeImages = new List<Image>();

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _progressText;
    [SerializeField] private TextMeshProUGUI _priceText;

    [Header("Collections")]
    [SerializeField] private GameCellContainer _cellContainer;
    [SerializeField] private ImprovementContainer _improvementContainer;

    [Header("Animation Duration")]
    [SerializeField] private float _fadeTime;

    private Sequence _inProgressAnimation;

    public IGameCellView Instantiate(Transform parent)
    {
        return Instantiate(this, parent);
    }

    public void Initialize(GameCellModel model)
    {
        SetSize(model.Size);
        SetBusinessSprite(model.Level);
        SetImprovementSprites(model.Level);
        SetBackgroundSprite(model);

        SetPosition(model.Position);
        SetCost(model.Cost);
        InitializeImprovements(model);
    }

    public void InitializeImprovements(GameCellModel model)
    {
        SetActivateManager(model.IsManagerActivated);

        for (var i = 0; i < model.ActivatedUpgradeLevel; i++)
        {
            _upgrades[i].SetActive(true);
        }
    }

    public void StartAnimation(GameCellModel model, Action onCompleteAnimation = null)
    {
        Initialize(model);

        switch (model.State)
        {
            case CellState.Lock:
                LockAnimation();
                break;

            case CellState.Unlock:
                UnlockAnimation();
                break;

            case CellState.Active:
                ActivateAnimation(onCompleteAnimation);
                break;

            case CellState.InProgress:
                InProgressAnimation(model, onCompleteAnimation);
                break;
        }
    }

    public void StartLevelUpAnimation(GameCellModel model)
    {
        StopInProgressAnimation();
        HideImprovements();
        _businessImage.Fade(0, _fadeTime, null, () => StartAnimation(model));
    }

    public void SetActiveSelector(bool isActivated)
    {
        _selectorImage.gameObject.SetActive(isActivated);
    }

    public Button GetBuyButton()
    {
        return _buyButton;
    }

    public Button GetCellButton()
    {
        return _cellButton;
    }

    private void SetBackgroundSprite(GameCellModel model)
    {
        if (model.BackgroundNumber < 0)
        {
            model.BackgroundNumber = Random.Range(0, _cellContainer.GetBackgroundSpritesCount());
        }

        _backgroundImage.sprite = _cellContainer.GetBackgroundSprites(model.BackgroundNumber);
    }

    private void SetBusinessSprite(int level)
    {
        if (level == 0)
        {
            return;
        }

        _businessImage.sprite = _cellContainer.GetBusinessSprites(level);
    }

    private void SetImprovementSprites(int level)
    {
        if (level == 0)
        {
            return;
        }

        _managerImage.sprite = _improvementContainer.GetManagerSprites(level);

        for (var i = 0; i < 3; i++)
        {
            _upgradeImages[i].sprite = _improvementContainer.GetUpgradeSprites(level, i + 1);
        }
    }

    private void SetActivateManager(bool isActivated)
    {
        _manager.SetActive(isActivated);
    }

    private void SetCost(MoneyModel cost)
    {
        if (cost == null)
        {
            return;
        }

        _priceText.text = cost.ToFormattedString();
    }

    private void SetPosition(float[] position)
    {
        transform.localPosition = new Vector2(position[0], position[1]);
    }

    private void SetSize(float size)
    {
        _cellRect.sizeDelta = new Vector2(size, size);
    }

    private void LockAnimation()
    {
        _progressImage.gameObject.SetActive(false);
        _lockImage.gameObject.SetActive(true);
        _lockImage.Fade(1, _fadeTime);
    }

    private void UnlockAnimation()
    {
        StopInProgressAnimation();
        var sequence = DOTween.Sequence();

        _businessImage.Fade(0, _fadeTime, sequence);
        _lockImage.Fade(0, _fadeTime, sequence, () => _lockImage.gameObject.SetActive(false));
        _unlockImage.Fade(1, _fadeTime, sequence);

        HideImprovements();
        _unlockImage.gameObject.SetActive(true);
        sequence.Play();
    }

    private void ActivateAnimation(Action callBack)
    {
        _progressImage.gameObject.SetActive(false);
        _lockImage.gameObject.SetActive(false);
        var sequence = DOTween.Sequence();

        _unlockImage.Fade(0, _fadeTime, sequence, () => _unlockImage.gameObject.SetActive(false));
        _businessImage.Fade(1, _fadeTime, sequence, () => callBack?.Invoke());

        sequence.Play();
    }

    private void InProgressAnimation(GameCellModel model, Action callBack)
    {
        _lockImage.gameObject.SetActive(false);
        _unlockImage.gameObject.SetActive(false);
        _businessImage.Fade(1, _fadeTime);
        _progressImage.gameObject.SetActive(true);

        var startTime = TimeSpan.Zero;
        _progressImage.fillAmount = 1;

        if (DateTime.Now.CompareTo(model.StartTime) > 0)
        {
            startTime = DateTime.Now.Subtract(model.StartTime);
            var filledAmount = (float)(startTime.TotalSeconds / model.TotalTime.TotalSeconds);

            if (filledAmount < 1)
            {
                _progressImage.fillAmount = 1 - filledAmount;
            }
        }

        _inProgressAnimation = DOTween.Sequence();
        _progressImage.Fill(_progressText, startTime, model.TotalTime, _inProgressAnimation, () =>
        {
            _progressImage.gameObject.SetActive(false);
            callBack?.Invoke();
        });
    }

    private void StopInProgressAnimation()
    {
        _inProgressAnimation.Kill();
        _progressImage.gameObject.SetActive(false);
    }

    private void HideImprovements()
    {
        SetActivateManager(false);
        foreach (var upgrade in _upgrades)
        {
            upgrade.SetActive(false);
        }
    }
}
}
