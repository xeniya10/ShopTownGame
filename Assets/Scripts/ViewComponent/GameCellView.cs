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
    [SerializeField] private GameObject _firstUpgrade;
    [SerializeField] private GameObject _secondUpgrade;
    [SerializeField] private GameObject _thirdUpgrade;

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
    [SerializeField] private Image _firstUpgradeImage;
    [SerializeField] private Image _secondUpgradeImage;
    [SerializeField] private Image _thirdUpgradeImage;
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
        _firstUpgradeImage.sprite = _upgradeCollection.FirstLevelSprites[level - 1];
        _secondUpgradeImage.sprite = _upgradeCollection.SecondLevelSprites[level - 1];
        _thirdUpgradeImage.sprite = _upgradeCollection.ThirdLevelSprites[level - 1];
    }

    private void SetActivateUpgrades(bool isFirstUpgradeActivated, bool isSecondUpgradeActivated, bool isThirdUpgradeActivated)
    {
        _firstUpgrade.SetActive(isFirstUpgradeActivated);
        _secondUpgrade.SetActive(isSecondUpgradeActivated);
        _thirdUpgrade.SetActive(isThirdUpgradeActivated);
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
        ChangeState(model.State);
        SetActiveImprovements(model);
    }

    public void SetActiveImprovements(GameCellModel model)
    {
        _manager.SetActive(model.IsUnlockedManager);

        switch (model.UpgradeLevel)
        {
            case 0:
                SetActivateUpgrades(false, false, false);
                break;

            case 1:
                SetActivateUpgrades(true, false, false);
                break;

            case 2:
                SetActivateUpgrades(true, true, false);
                break;

            case 3:
                SetActivateUpgrades(true, true, true);
                break;
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
        HideBusiness(null);
        _lockImage.Fade(1, _fadeTime, null, null);
    }

    public void SetUnlockState()
    {
        var sequence = DOTween.Sequence();

        _businessImage.Fade(0, _fadeTime, sequence, null);
        _lockImage.Fade(0, _fadeTime, sequence, () => _lockObject.SetActive(false));
        _unlockImage.Fade(1, _fadeTime, sequence, null);

        _unlockImage.gameObject.SetActive(true);
        sequence.Play();
    }

    public void SetActiveState()
    {
        var sequence = DOTween.Sequence();

        _unlockImage.Fade(0, _fadeTime, sequence, () => _unlockImage.gameObject.SetActive(false));
        _businessImage.Fade(1, _fadeTime, sequence, null);

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
        _progressImage.Fill(_progressTimeText, (float)totalTime, () =>
        {
            _progressImage.fillAmount = 1;
            _progressBar.SetActive(false);
            callBack?.Invoke();
        });
    }
}
