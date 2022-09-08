using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeRowView : MonoBehaviour
{
    [Header("Components")]
    public Button BuyButton;
    public Image UpgradeImage;
    public GameObject LockState;

    [Space]
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI PriceText;

    [Header("Collections")]
    public FirstUpgradeSpriteCollection FirstLevelSpriteCollection;
    public SecondUpgradeSpriteCollection SecondLevelSpriteCollection;
    public ThirdUpgradeSpriteCollection ThirdLevelSpriteCollection;

    public void SetFirstUpgradeSprite(int level)
    {
        UpgradeImage.sprite = FirstLevelSpriteCollection.UpgradeSprites[level - 1];
    }

    public void SetSecondUpgradeSprite(int level)
    {
        UpgradeImage.sprite = SecondLevelSpriteCollection.UpgradeSprites[level - 1];
    }

    public void SetThirdUpgradeSprite(int level)
    {
        UpgradeImage.sprite = ThirdLevelSpriteCollection.UpgradeSprites[level - 1];
    }

    public void SetPrice(float price)
    {
        PriceText.text = price.ToString();
    }

    public void SetName(string name)
    {
        PriceText.text = name;
    }

    public void SetDescription(string description)
    {
        PriceText.text = description;
    }

    public void Lock()
    {
        LockState.SetActive(true);
    }

    public void Unlock()
    {
        LockState.SetActive(false);
    }
}