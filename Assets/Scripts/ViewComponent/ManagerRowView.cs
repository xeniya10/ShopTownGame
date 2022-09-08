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

    public void SetSprite(int level)
    {
        ManagerImage.sprite = ManagerSpriteCollection.ManagerSprites[level - 1];
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
