using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameScreenView : MonoBehaviour
{
    [Header("Boards")]
    public Transform GameBoardContent;
    public Transform ManagerBoardContent;
    public Transform UpgradeBoardContent;

    [Header("Buttons")]
    [SerializeField] private Button _moneyAddButton;
    [SerializeField] private Button _goldAddButton;
    [SerializeField] private Button _menuButton;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _goldText;

    public void SetMoneyNumber(int number)
    {
        _moneyText.text = number.ToString();
    }
    public void SetGoldNumber(int number)
    {
        _goldText.text = number.ToString();
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
}