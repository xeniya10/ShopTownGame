using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameScreenView : MonoBehaviour
{
    [Header("Screens")]
    public GameObject MenuScreen;
    public GameObject PurchaseScreen;
    public GameObject NewLevelScreen;
    public GameObject WelcomeScreen;

    [Header("Boards")]
    public GameObject GameBoard;
    public GameObject ManagerBoard;
    public GameObject UpgradeBoard;

    [Header("Buttons")]
    public Button MoneyAddButton;
    public Button GoldAddButton;
    public Button MenuButton;

    [Header("Text Fields")]

    [SerializeField]
    private TextMeshProUGUI MoneyText;

    [SerializeField]
    private TextMeshProUGUI GoldText;

    public void SetMoneyNumber(int number)
    {
        MoneyText.text = number.ToString();
    }
    public void SetGoldNumber(int number)
    {
        GoldText.text = number.ToString();
    }
}
