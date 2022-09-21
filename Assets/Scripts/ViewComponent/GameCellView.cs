using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GameCellView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private RectTransform _cellRect;

    [Space]
    [SerializeField] private Button _earnButton;
    [SerializeField] private Button _buyButton;

    [Space]
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _businessImage;
    [SerializeField] private Image _progressImage;
    [SerializeField] private Image _blockImage;
    [SerializeField] private Image _lockImage;

    [Space]
    [SerializeField] private TextMeshProUGUI _progressTimeText;
    [SerializeField] private TextMeshProUGUI _priceText;

    [Header("Collections")]
    [SerializeField] private BackgroundSpriteCollection _backgroundSpriteCollection;
    [SerializeField] private BusinessSpriteCollection _businessSpriteCollection;

    [Header("Animation Duration")]
    [SerializeField] private float _fadeTime;

    private Vector2[] _hitDirections = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
    [HideInInspector] public Vector2 StartPosition { get; set; }

    public void SetRandomBackgroundSprite()
    {
        var randomNumber = Random.Range(0, _backgroundSpriteCollection.BackgroundSprites.Count);
        _backgroundImage.sprite = _backgroundSpriteCollection.BackgroundSprites[randomNumber];
    }
    public void SetBackgroundSprite(int i)
    {
        _backgroundImage.sprite = _backgroundSpriteCollection.BackgroundSprites[i];
    }

    public void SetBusinessSprite(int level)
    {
        _businessImage.sprite = _businessSpriteCollection.BusinessSprites[level - 1];
    }

    public void SetMoneyPrice(double price)
    {
        _priceText.text = MoneyFormatUtility.MoneyDefault(price);
    }

    public void SetGoldPrice(double price)
    {
        _priceText.text = MoneyFormatUtility.GoldDefault(price);
    }

    public void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public void SetSize(float size)
    {
        _cellRect.sizeDelta = new Vector2(size, size);
    }

    public void ClickBuyButton(Action callBack)
    {
        _buyButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void ClickEarnButton(Action callBack)
    {
        _earnButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public GameCellView Create(Transform parent)
    {
        var cell = Instantiate(this, parent);
        return cell;
    }

    public void Block()
    {
        _progressBar.SetActive(false);
        _blockImage.gameObject.SetActive(true);
        _lockImage.gameObject.SetActive(false);
    }

    public void Lock()
    {
        _lockImage.gameObject.SetActive(true);
        AnimationUtility.FadeImage(_blockImage, 0, _fadeTime, null,
        () => _blockImage.gameObject.SetActive(false));
    }

    public void Unlock()
    {
        AnimationUtility.FadeImage(_lockImage, 0, _fadeTime, null,
        () => _lockImage.gameObject.SetActive(false));
    }

    public void ChangeProgressBar(float currentTime, float totalTime)
    {
        _progressBar.SetActive(true);

        _progressImage.fillAmount = currentTime / totalTime;
        _progressTimeText.text = currentTime.ToString();

        if (currentTime < 0.01f)
        {
            _progressBar.SetActive(false);
        }
    }

    public void Drag() { }

    public List<GameCellView> CheckNeighbors()
    {
        List<GameCellView> hitedCells = new List<GameCellView>();

        for (int i = 0; i < _hitDirections.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, _hitDirections[i]);
            GameCellView hitedCell = hit.collider?.GetComponent<GameCellView>();

            if (hitedCell != null)
            {
                hitedCells.Add(hitedCell);
            }
        }

        return hitedCells;
    }
}