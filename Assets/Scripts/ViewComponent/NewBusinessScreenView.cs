using System;
using DG.Tweening;
using ShopTown.Data;
using ShopTown.SpriteContainer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class NewBusinessScreenView : MonoBehaviour
{
    [SerializeField] private Button _okButton;

    [Header("Images")]
    [SerializeField] private Image _businessImage;
    [SerializeField] private Image _managerImage;
    [SerializeField] private Image _firstUpgradeImage;
    [SerializeField] private Image _secondUpgradeImage;
    [SerializeField] private Image _thirdUpgradeImage;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _businessName;
    [SerializeField] private TextMeshProUGUI _managerName;
    [SerializeField] private TextMeshProUGUI _firstUpgradeName;
    [SerializeField] private TextMeshProUGUI _secondUpgradeName;
    [SerializeField] private TextMeshProUGUI _thirdUpgradeName;

    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem _leftConfetti;
    [SerializeField] private ParticleSystem _rightConfetti;

    [Header("Containers")]
    [SerializeField] private BusinessCollection _businessCollection;
    [SerializeField] private UpgradeCollection _upgradeCollection;
    [SerializeField] private ManagerCollection _managerCollection;
    [SerializeField] private BusinessData _businessData;
    [SerializeField] private UpgradeRowData _upgradeData;
    [SerializeField] private ManagerRowData _managerData;

    [Header("Animation Durations")]
    [SerializeField] private float _moveTime;

    private Vector2 _startPosition;

    private void SetBusinessSprite(int level)
    {
        _businessImage.sprite = _businessCollection.Sprites[level - 1];
    }

    private void SetManagerSprite(int level)
    {
        _managerImage.sprite = _managerCollection.AvatarSprites[level - 1];
    }

    private void SetUpgradeSprites(int level)
    {
        _firstUpgradeImage.sprite = _upgradeCollection.FirstLevelSprites[level - 1];
        _secondUpgradeImage.sprite = _upgradeCollection.SecondLevelSprites[level - 1];
        _thirdUpgradeImage.sprite = _upgradeCollection.ThirdLevelSprites[level - 1];
    }

    private void SetBusinessName(int level)
    {
        _businessName.text = _businessData.LevelNames[level - 1];
    }

    private void SetManagerName(int level)
    {
        _managerName.text = _managerData.ManagerNames[level - 1];
    }

    private void SetUpgradeNames(int level)
    {
        _firstUpgradeName.text = _upgradeData.FirstLevelNames[level - 1];
        _secondUpgradeName.text = _upgradeData.SecondLevelNames[level - 1];
        _thirdUpgradeName.text = _upgradeData.ThirdLevelNames[level - 1];
    }

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public void ClickOkButton(Action callBack)
    {
        _okButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void Initialize(int level)
    {
        SetBusinessSprite(level);
        SetManagerSprite(level);
        SetUpgradeSprites(level);

        SetBusinessName(level);
        SetManagerName(level);
        SetUpgradeNames(level);
    }

    public void Show()
    {
        var sequence = DOTween.Sequence();
        _startPosition = transform.localPosition;
        gameObject.SetActive(true);
        AnimationUtility.MoveFromScreenBorder(transform, 0f, -1.5f, _moveTime, sequence);
        sequence.OnComplete(() =>
        {
            _leftConfetti.Play();
            _rightConfetti.Play();
        });
    }

    public void Hide()
    {
        var sequence = DOTween.Sequence();
        AnimationUtility.MoveToScreenBorder(transform, 0f, -1.5f, _moveTime, sequence);
        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
            SetPosition(_startPosition);
        });
    }
}
}
