using DG.Tweening;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class NewBusinessScreenView : MonoBehaviour, IScreenView<GameCellModel>
{
    [SerializeField] private Button _hideButton;

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
    [SerializeField] private GameCellContainer _gameCellContainer;
    [SerializeField] private ImprovementContainer _improvementSprites;
    [SerializeField] private BusinessData _businessData;
    [SerializeField] private BoardData _boardData;
    [SerializeField] private ImprovementData _improvementData;

    [Header("Animation Durations")]
    [SerializeField] private float _moveTime;

    private Vector2 _startPosition;

    public void Initialize(GameCellModel model)
    {
        SetBusinessSprite(model.Level);
        SetManagerSprite(model.Level);
        SetUpgradeSprites(model.Level);

        SetBusinessName(model.Level);
        SetBusinessParams(model.Level);
        SetManagerName(model.Level);
        SetUpgradeNames(model.Level);
    }

    public void SetActive(bool isActivated)
    {
        if (isActivated)
        {
            Show();
            return;
        }

        Hide();
    }

    public Button GetHideButton()
    {
        return _hideButton;
    }

    private void Show()
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

    private void Hide()
    {
        var sequence = DOTween.Sequence();
        transform.MoveToScreenBorder(0f, -1.5f, _moveTime, sequence);
        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
            SetPosition(_startPosition);
        });
    }

    private void SetBusinessSprite(int level)
    {
        _businessImage.sprite = _gameCellContainer.GetBusinessSprites(level);
    }

    private void SetManagerSprite(int level)
    {
        _managerImage.sprite = _improvementSprites.GetManagerSprites(level);
    }

    private void SetUpgradeSprites(int level)
    {
        _firstUpgradeImage.sprite = _improvementSprites.GetUpgradeSprites(level, 1);
        _secondUpgradeImage.sprite = _improvementSprites.GetUpgradeSprites(level, 2);
        _thirdUpgradeImage.sprite = _improvementSprites.GetUpgradeSprites(level, 3);
    }

    private void SetBusinessName(int level)
    {
        _businessName.text = _businessData.GetName(level);
    }

    private void SetBusinessParams(int level)
    {
        _time.text = _boardData.GetTime(level).ToTimeSpan().ToSymbolFormatString();
        _profit.text = _boardData.GetProfit(level).ToFormattedString();
    }

    private void SetManagerName(int level)
    {
        _managerName.text = _improvementData.GetManagerName(level);
    }

    private void SetUpgradeNames(int level)
    {
        _firstUpgradeName.text = _improvementData.GetUpgradeNames(level, 1);
        _secondUpgradeName.text = _improvementData.GetUpgradeNames(level, 2);
        _thirdUpgradeName.text = _improvementData.GetUpgradeNames(level, 3);
    }

    private void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }
}
}
