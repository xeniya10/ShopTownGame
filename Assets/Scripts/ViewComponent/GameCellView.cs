using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCellView : MonoBehaviour
{
    [Header("Components")]
    public Button CellButton;
    public Button BuyButton;

    [Space]
    public Image BackgroundImage;
    public Image BusinessImage;
    public Image ProgressImage;

    [Space]
    public GameObject ProgressBar;
    public GameObject BlockState;
    public GameObject LockState;

    [Space]
    public TextMeshProUGUI ProgressTimeText;
    public TextMeshProUGUI PriceText;

    [Header("Collections")]
    public BackgroundSpriteCollection BackgroundSpriteCollection;
    public BusinessSpriteCollection BusinessSpriteCollection;

    public void SetBackgroundSprite(int i)
    {
        BackgroundImage.sprite = BackgroundSpriteCollection.BackgroundSprites[i];
    }

    public void SetBusinessSprite(int level)
    {
        BusinessImage.sprite = BusinessSpriteCollection.BusinessSprites[level - 1];
    }

    public void SetPrice(float price)
    {
        PriceText.text = price.ToString();
    }

    public void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public void Block()
    {
        ProgressBar.SetActive(false);
        BlockState.SetActive(true);
        LockState.SetActive(false);
    }

    public void Lock()
    {
        BlockState.SetActive(false);
        LockState.SetActive(true);
    }

    public void Unlock()
    {
        LockState.SetActive(false);
    }

    public void ChangeProgressBar(float currentTime, float totalTime)
    {
        ProgressBar.SetActive(true);

        ProgressImage.fillAmount = currentTime / totalTime;
        ProgressTimeText.text = currentTime.ToString();

        if (currentTime < 0.01f)
        {
            ProgressBar.SetActive(false);
        }
    }
}
