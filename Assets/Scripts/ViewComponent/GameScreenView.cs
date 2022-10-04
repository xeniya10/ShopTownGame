using System;
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
    [SerializeField] private Button _moneyAddButton;
    [SerializeField] private Button _goldAddButton;
    [SerializeField] private Button _menuButton;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _goldText;

    public void SetMoneyNumber(double number)
    {
        _moneyText.text = MoneyFormatUtility.Default(number);
    }

    public void SetGoldNumber(double number)
    {
        _goldText.text = MoneyFormatUtility.Default(number);
    }

    public void ClickAddButton(Action callBack)
    {
        _moneyAddButton.onClick.AddListener(() => callBack?.Invoke());
        _goldAddButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void ClickMenuButton(Action callBack)
    {
        _menuButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void HideGameBoard()
    {
        GameBoard.gameObject.SetActive(false);
    }

    public void ShowGameBoard()
    {
        GameBoard.gameObject.SetActive(true);
    }
}
}
