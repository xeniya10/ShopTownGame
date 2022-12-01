using ShopTown.ModelComponent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class GameScreenView : MonoBehaviour, IGameScreenView
{
    [Header("Boards")]
    [SerializeField] private Transform _gameBoard;
    [SerializeField] private Transform _managerBoard;
    [SerializeField] private Transform _upgradeBoard;

    [Header("Buttons")]
    [SerializeField] private Button _dollarAddButton;
    [SerializeField] private Button _goldAddButton;
    [SerializeField] private Button _menuButton;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _dollarText;
    [SerializeField] private TextMeshProUGUI _goldText;

    private void SetDollarNumber(MoneyModel number)
    {
        _dollarText.text = number.ToFormattedString();
    }

    private void SetGoldNumber(MoneyModel number)
    {
        _goldText.text = number.ToFormattedString();
    }

    public void Initialize(GameDataModel data)
    {
        SetDollarNumber(data.CurrentDollarBalance);
        SetGoldNumber(data.CurrentGoldBalance);
    }

    public Transform GetGameBoard()
    {
        return _gameBoard;
    }

    public Transform GetManagerBoard()
    {
        return _managerBoard;
    }

    public Transform GetUpgradeBoard()
    {
        return _upgradeBoard;
    }

    public Button GetDollarAddButton()
    {
        return _dollarAddButton;
    }

    public Button GetGoldAddButton()
    {
        return _goldAddButton;
    }

    public Button GetMenuButton()
    {
        return _menuButton;
    }
}

public interface IGameScreenView : IInitializable<GameDataModel>, IBoard, IPurchaseButton, IMenuButton
{}

public interface IBoard
{
    Transform GetGameBoard();

    Transform GetManagerBoard();

    Transform GetUpgradeBoard();
}

public interface IPurchaseButton
{
    Button GetDollarAddButton();

    Button GetGoldAddButton();
}

public interface IMenuButton
{
    Button GetMenuButton();
}
}
