using System;
using System.Collections.Generic;
using DG.Tweening;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameCellView : MonoBehaviour
{
    [SerializeField] private RectTransform _cellRect;

    [Header("Game Objects")]
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private GameObject _lockObject;
    [SerializeField] private GameObject _manager;
    [SerializeField] private List<GameObject> _upgrades = new List<GameObject>();

    [Header("Buttons")]
    public Button CellButton;
    public Button BuyButton;

    [Header("Images")]
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _businessImage;
    [SerializeField] private Image _progressImage;
    [SerializeField] private Image _unlockImage;
    [SerializeField] private Image _lockImage;
    [SerializeField] private Image _managerImage;
    [SerializeField] private List<Image> _upgradeImages = new List<Image>();
    public Image _selectorImage;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _progressTimeText;
    [SerializeField] private TextMeshProUGUI _priceText;

    [Header("Collections")]
    [SerializeField] private BackgroundCollection _backgroundCollection;
    [SerializeField] private BusinessCollection _businessCollection;
    [SerializeField] private ManagerCollection _managerCollection;
    [SerializeField] private UpgradeCollection _upgradeCollection;

    [Header("Animation Duration")]
    [SerializeField] private float _fadeTime;
    private Sequence _inProgressAnimation;

    private void SetBackgroundSprite(int i)
    {
        _backgroundImage.sprite = _backgroundCollection.Sprites[i];
    }

    private void SetBusinessSprite(int level)
    {
        if (level == 0)
        {
            return;
        }

        _businessImage.sprite = _businessCollection.Sprites[level - 1];
    }

    private void SetImprovementSprites(int level)
    {
        if (level == 0)
        {
            return;
        }

        _managerImage.sprite = _managerCollection.AvatarSprites[level - 1];
        _upgradeImages[0].sprite = _upgradeCollection.FirstLevelSprites[level - 1];
        _upgradeImages[1].sprite = _upgradeCollection.SecondLevelSprites[level - 1];
        _upgradeImages[2].sprite = _upgradeCollection.ThirdLevelSprites[level - 1];
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
        SetImprovementSprites(model.Level);
        if (model.BackgroundNumber < 0)
        {
            model.BackgroundNumber = Random.Range(0, _backgroundCollection.Sprites.Count);
        }

        SetBackgroundSprite(model.BackgroundNumber);

        SetPosition(model.Position);
        SetCost(model.Cost);
        SetActiveImprovements(model);
    }

    public void SetActiveImprovements(GameCellModel model)
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

    private void LockAnimation()
    {
        _progressBar.SetActive(false);
        _lockObject.SetActive(true);
        _lockImage.Fade(1, _fadeTime);
    }

    private void UnlockAnimation()
    {
        StopInProgressAnimation();
        var sequence = DOTween.Sequence();

        _businessImage.Fade(0, _fadeTime, sequence);
        _lockImage.Fade(0, _fadeTime, sequence, () => _lockObject.SetActive(false));
        _unlockImage.Fade(1, _fadeTime, sequence);

        HideImprovements();
        _unlockImage.gameObject.SetActive(true);
        sequence.Play();
    }

    private void ActivateAnimation(Action callBack)
    {
        _lockObject.SetActive(false);
        var sequence = DOTween.Sequence();

        _unlockImage.Fade(0, _fadeTime, sequence, () => _unlockImage.gameObject.SetActive(false));
        _businessImage.Fade(1, _fadeTime, sequence, () => callBack?.Invoke());

        sequence.Play();
    }

    private void InProgressAnimation(GameCellModel model, Action callBack)
    {
        _lockObject.SetActive(false);
        _unlockImage.gameObject.SetActive(false);
        _businessImage.Fade(1, _fadeTime);
        _progressBar.SetActive(true);

        var startTime = TimeSpan.Zero;
        _progressImage.fillAmount = 1;

        if (DateTime.Now.CompareTo(model.StartTime) > 0)
        {
            var fr = (float)(DateTime.Now.Subtract(model.StartTime).TotalSeconds / model.TotalTime.TotalSeconds);
            startTime = DateTime.Now.Subtract(model.StartTime);
            _progressImage.fillAmount = 1 - fr;
        }

        _inProgressAnimation = DOTween.Sequence();
        _progressImage.Fill(_progressTimeText, startTime, model.TotalTime, _inProgressAnimation, () =>
        {
            _progressBar.SetActive(false);
            callBack?.Invoke();
        });
    }

    private void StopInProgressAnimation()
    {
        _inProgressAnimation.Kill();
        _progressBar.SetActive(false);
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
