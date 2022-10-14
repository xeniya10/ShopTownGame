using System;
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

    [Header("Images")]
    [SerializeField] private Image _managerImage;
    [SerializeField] private Image _lockImage;

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
        _priceText.text = MoneyFormatUtility.Default(cost);
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
        var row = Instantiate(this, parent);
        return row;
    }

    public void Initialize(ManagerRowModel model)
    {
        SetSprite(model.Level);
        SetName(model.Name);
        SetDescription(model.Description);
        SetCost(model.Cost);
    }

    public void Lock()
    {
        AnimationUtility.Fade(_lockImage, 0, _fadeTime, null, () =>
        {
            _lockImage.gameObject.SetActive(true);
            AnimationUtility.Fade(_lockImage, 0.35f, _fadeTime, null, null);
        });
    }

    public void Unlock()
    {
        AnimationUtility.Fade(_lockImage, 0, _fadeTime, null, () => _lockImage.gameObject.SetActive(false));
    }
}
}
