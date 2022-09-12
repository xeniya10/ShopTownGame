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

    [Space]
    public UpgradeSpriteCollection UpgradeSprites;

    public void SetFirstUpgradeSprite(int level)
    {
        UpgradeImage.sprite = UpgradeSprites.FirstLevelSprites[level - 1];
    }

    public void SetSecondUpgradeSprite(int level)
    {
        UpgradeImage.sprite = UpgradeSprites.SecondLevelSprites[level - 1];
    }

    public void SetThirdUpgradeSprite(int level)
    {
        UpgradeImage.sprite = UpgradeSprites.ThirdLevelSprites[level - 1];
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