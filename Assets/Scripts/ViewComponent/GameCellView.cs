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
    [SerializeField] private RectTransform _cellRect;

    [Header("Game Objects")]
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private GameObject _lockObject;
    [SerializeField] private GameObject _manager;
    [SerializeField] private GameObject[] _upgrades = new GameObject[3];

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
    [SerializeField] private Image[] _upgradeImages = new Image[3];
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

    public void SetCost(MoneyModel cost)
    {
        _priceText.text = cost.ToFormattedString();
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
        SetImprovementSprites(model.Level);
        SetPosition(model.Position);
        SetCost(model.Cost);
        ChangeState(model);
        SetActiveImprovements(model);
    }

    public void SetActiveImprovements(GameCellModel model)
    {
        SetActivateManager(model.IsActivatedManager);

        for (var i = 0; i < _upgrades.Length; i++)
        {
            _upgrades[i].SetActive(model.IsActivatedUpgrades[i]);
        }
    }

    private void HideImprovements()
    {
        SetActivateManager(false);
        foreach (var upgrade in _upgrades)
        {
            upgrade.SetActive(false);
        }
    }

    public void HideBusiness(Action callBack)
    {
        _businessImage.Fade(0, _fadeTime, null, () => callBack?.Invoke());
    }

    public void SetLockState()
    {
        _progressBar.SetActive(false);
        _lockObject.SetActive(true);
        _lockImage.Fade(1, _fadeTime, null, null);
    }

    public void SetUnlockState()
    {
        var sequence = DOTween.Sequence();

        _businessImage.Fade(0, _fadeTime, sequence, null);
        _lockImage.Fade(0, _fadeTime, sequence, () => _lockObject.SetActive(false));
        _unlockImage.Fade(1, _fadeTime, sequence, null);

        HideImprovements();
        _unlockImage.gameObject.SetActive(true);
        sequence.Play();
    }

    public void SetActiveState(Action callBack)
    {
        var sequence = DOTween.Sequence();

        _unlockImage.Fade(0, _fadeTime, sequence, () => _unlockImage.gameObject.SetActive(false));
        _businessImage.Fade(1, _fadeTime, sequence, () => callBack?.Invoke());

        sequence.Play();
    }

    private void ChangeState(GameCellModel model)
    {
        switch (model.State)
        {
            case CellState.Lock:
                SetLockState();
                break;

            case CellState.Unlock:
                SetUnlockState();
                break;

            case CellState.Active:
                SetActiveState(null);
                break;
        }
    }

    public void SetInProgressState(double totalTime, Action callBack)
    {
        _inProgressAnimation = DOTween.Sequence();
        _progressBar.SetActive(true);
        _progressImage.Fill(_progressTimeText, (float)totalTime, _inProgressAnimation, () =>
        {
            _progressImage.fillAmount = 1;
            _progressBar.SetActive(false);
            callBack?.Invoke();
        });
    }

    public void StopInProgressAnimation()
    {
        _inProgressAnimation.Kill();
        _progressBar.SetActive(false);
    }
}
