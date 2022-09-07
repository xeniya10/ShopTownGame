using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCellView : MonoBehaviour
{
    [Header("Components")]
    public Button CellButton;

    [Space]
    public Image BackgroundImage;
    public Image BusinessImage;
    public Image ProgressImage;

    [Space]
    public GameObject ProgressBar;
    public GameObject LockState;
    public GameObject UnlockState;

    [Space]
    public TextMeshProUGUI ProgressTimeText;
    public TextMeshProUGUI PriceText;

    [Header("Collections")]
    public CellBackgroundSpriteCollection BackgroundSpriteCollection;
    public BusinessSpriteCollection BusinessSpriteCollection;
}
