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
}
