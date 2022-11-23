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
    public Button OkButton;

    [Header("Images")]
    [SerializeField] private Image _businessImage;
    [SerializeField] private Image _managerImage;
    [SerializeField] private Image _firstUpgradeImage;
    [SerializeField] private Image _secondUpgradeImage;
    [SerializeField] private Image _thirdUpgradeImage;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _businessName;
    [SerializeField] private TextMeshProUGUI _time;
    [SerializeField] private TextMeshProUGUI _profit;
    [SerializeField] private TextMeshProUGUI _managerName;
    [SerializeField] private TextMeshProUGUI _firstUpgradeName;
    [SerializeField] private TextMeshProUGUI _secondUpgradeName;
    [SerializeField] private TextMeshProUGUI _thirdUpgradeName;

    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem _leftConfetti;
    [SerializeField] private ParticleSystem _rightConfetti;

    [Header("Containers")]
    [SerializeField] private GameCellCollection _gameCellCollection;
    [SerializeField] private ImprovementCollection _improvementSprites;
    [SerializeField] private BusinessData _businessData;
    [SerializeField] private GameCellData _gameCellData;
    [SerializeField] private ImprovementData _improvementData;

    [Header("Animation Durations")]
    [SerializeField] private float _moveTime;

    private Vector2 _startPosition;

    private void SetBusinessSprite(int level)
    {
        _businessImage.sprite = _gameCellCollection.BusinessSprites[level - 1];
    }

    private void SetManagerSprite(int level)
    {
        _managerImage.sprite = _improvementSprites.ManagerSprites[level - 1];
    }

    private void SetUpgradeSprites(int level)
    {
        _firstUpgradeImage.sprite = _improvementSprites.FirstLevelUpgradeSprites[level - 1];
        _secondUpgradeImage.sprite = _improvementSprites.SecondLevelUpgradeSprites[level - 1];
        _thirdUpgradeImage.sprite = _improvementSprites.ThirdLevelUpgradeSprites[level - 1];
    }

    private void SetBusinessName(int level)
    {
        _businessName.text = _businessData.LevelNames[level - 1];
    }

    private void SetBusinessParams(int level)
    {
        _time.text = _gameCellData.ProcessTime[level - 1].ToNameFormatString();
        _profit.text = _gameCellData.BaseProfit[level - 1].ToFormattedString();
    }

    private void SetManagerName(int level)
    {
        _managerName.text = _improvementData.ManagerNames[level - 1];
    }

    private void SetUpgradeNames(int level)
    {
        _firstUpgradeName.text = _improvementData.FirstLevelUpgradeNames[level - 1];
        _secondUpgradeName.text = _improvementData.SecondLevelUpgradeNames[level - 1];
        _thirdUpgradeName.text = _improvementData.ThirdLevelUpgradeNames[level - 1];
    }

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public void Initialize(int level)
    {
        SetBusinessSprite(level);
        SetManagerSprite(level);
        SetUpgradeSprites(level);

        SetBusinessName(level);
        SetBusinessParams(level);
        SetManagerName(level);
        SetUpgradeNames(level);
    }

    public void Show()
    {
        var sequence = DOTween.Sequence();
        _startPosition = transform.localPosition;
        gameObject.SetActive(true);
        transform.MoveFromScreenBorder(0f, -1.5f, _moveTime, sequence);
        sequence.OnComplete(() =>
        {
            _leftConfetti.Play();
            _rightConfetti.Play();
        });
    }

    public void Hide()
    {
        var sequence = DOTween.Sequence();
        transform.MoveToScreenBorder(0f, -1.5f, _moveTime, sequence);
        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
            SetPosition(_startPosition);
        });
    }
}
}
