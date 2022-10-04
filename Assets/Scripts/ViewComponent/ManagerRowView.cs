using System;
using ShopTown.SpriteContainer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class ManagerRowView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button _hireButton;
    [SerializeField] private Image _managerImage;
    [SerializeField] private Image _lockImage;

    [Space]
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

    public void SetMoneyPrice(double price)
    {
        _priceText.text = MoneyFormatUtility.MoneyDefault(price);
    }

    public void SetGoldPrice(double price)
    {
        _priceText.text = MoneyFormatUtility.GoldDefault(price);
    }

    private void SetName(string managerName)
    {
        _nameText.text = managerName;
    }

    private void SetDescription(string description)
    {
        _descriptionText.text = description;
    }

    public void ClickHireButton(Action callBack)
    {
        _hireButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public ManagerRowView Create(Transform parent, int level, string managerName,
        string description)
    {
        var row = Instantiate(this, parent);
        row.SetSprite(level);
        row.SetName(managerName);
        row.SetDescription(description);
        return row;
    }

    public void Lock()
    {
        AnimationUtility.Fade(_lockImage, 0, _fadeTime, null, () =>
        {
            _lockImage.gameObject.SetActive(true);
            AnimationUtility.Fade(_lockImage, 1, _fadeTime, null, null);
        });
    }

    public void Unlock()
    {
        AnimationUtility.Fade(_lockImage, 0, _fadeTime, null, () => _lockImage.gameObject.SetActive(false));
    }
}
}
