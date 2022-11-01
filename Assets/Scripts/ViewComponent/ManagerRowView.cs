using DG.Tweening;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class ManagerRowView : MonoBehaviour
{
    public Button HireButton;
    public ParticleSystem Salute;

    [Header("Currency Sprites")]
    [SerializeField] private Sprite _dollarIcon;
    [SerializeField] private Sprite _goldIcon;

    [Header("Images")]
    [SerializeField] private Image _managerImage;
    [SerializeField] private Image _lockImage;
    [SerializeField] private Image _currencyImage;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _priceText;

    [Header("Collections")]
    [SerializeField] private ManagerCollection _managerCollection;

    [Header("Animation Duration")]
    [SerializeField] private float _fadeTime;

    private void SetSprite(int level)
    {
        _managerImage.sprite = _managerCollection.AvatarSprites[level - 1];
    }

    private void SetCost(MoneyModel cost)
    {
        _priceText.text = cost.ToFormattedString();
        if (cost.Value == Currency.Dollar)
        {
            _currencyImage.sprite = _dollarIcon;
            return;
        }

        _currencyImage.sprite = _goldIcon;
    }

    private void SetName(string managerName)
    {
        _nameText.text = managerName;
    }

    private void SetDescription(string description)
    {
        _descriptionText.text = description;
    }

    public ManagerRowView Create(Transform parent)
    {
        return Instantiate(this, parent);
    }

    public void Initialize(ManagerRowModel model)
    {
        SetSprite(model.Level);
        SetName(model.Name);
        SetDescription(model.Description);
        SetCost(model.Cost);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Lock()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        _lockImage.DOFade(0, _fadeTime)
            .OnComplete(() =>
            {
                _lockImage.gameObject.SetActive(true);
                _lockImage.DOFade(0.35f, _fadeTime);
            });
    }

    public void Unlock()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        _lockImage.DOFade(0, _fadeTime).OnComplete(() => _lockImage.gameObject.SetActive(false));
    }
}
}
