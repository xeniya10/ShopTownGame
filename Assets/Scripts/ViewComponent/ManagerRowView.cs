using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerRowView : MonoBehaviour
{
    [Header("Components")]
    public Button HireButton;
    public Image ManagerImage;
    public GameObject LockState;

    [Space]
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI PriceText;

    [Header("Collections")]
    public ManagerSpriteCollection ManagerSpriteCollection;
}
