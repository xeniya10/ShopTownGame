using ShopTown.ModelComponent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class GameScreenView : MonoBehaviour
{
    [Header("Boards")]
    public Transform GameBoard;
    public Transform ManagerBoard;
    public Transform UpgradeBoard;

    [Header("Buttons")]
    public Button DollarAddButton;
    public Button GoldAddButton;
    public Button MenuButton;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _dollarText;
    [SerializeField] private TextMeshProUGUI _goldText;

    public void SetMoneyNumber(MoneyModel number)
    {
        if (number.Value == Currency.Dollar)
        {
            _dollarText.text = number.ToFormattedString();
            return;
        }

        _goldText.text = number.ToFormattedString();
    }
}
}
