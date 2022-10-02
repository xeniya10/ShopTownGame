using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;
using DG.Tweening;
using System.Collections.Generic;

public class GameCellView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private RectTransform _cellRect;

    [Space]
    public Button CellButton;
    public Button BuyButton;

    [Space]
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _businessImage;
    [SerializeField] private Image _progressImage;
    [SerializeField] private Image _unlockImage;
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

    public int RandomBackgroundSpriteNumber()
    {
        var randomNumber = Random.Range(0, _backgroundSpriteCollection.BackgroundSprites.Count);
        return randomNumber;
    }

    public void SetBackgroundSprite(int i)
    {
        _backgroundImage.sprite = _backgroundSpriteCollection.BackgroundSprites[i];
    }

    public void SetBusinessSprite(int level)
    {
        _businessImage.sprite = _businessSpriteCollection.BusinessSprites[level - 1];
    }

    public void SetCost(double cost)
    {
        _priceText.text = MoneyFormatUtility.Default(cost);
    }

    public void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public void SetSize(float size)
    {
        _cellRect.sizeDelta = new Vector2(size, size);
    }

    public GameCellView Create(Transform parent, float size, Vector2 position, int spriteNumber)
    {
        var cell = Instantiate(this, parent);
        cell.SetSize(size);
        cell.SetPosition(position);
        cell.SetLockState();
        return cell;
    }

    public void SetLockState()
    {
        _progressBar.SetActive(false);
        _lockImage.gameObject.SetActive(true);
        _unlockImage.gameObject.SetActive(false);
    }

    public void SetUnlockState()
    {
        var sequence = DOTween.Sequence();

        AnimationUtility.Fade(_lockImage, 0, _fadeTime, sequence,
        () => _lockImage.gameObject.SetActive(false));
        AnimationUtility.Fade(_unlockImage, 1, _fadeTime, sequence, null);

        _unlockImage.gameObject.SetActive(true);
        sequence.Play();
    }

    public void SetActiveState()
    {
        AnimationUtility.Fade(_unlockImage, 0, _fadeTime, null,
        () => _unlockImage.gameObject.SetActive(false));
    }

    public void ChangeState(CellState state)
    {
        switch (state)
        {
            case CellState.Lock:
                this.SetLockState();
                break;

            case CellState.Unlock:
                this.SetUnlockState();
                break;

            case CellState.Active:
                this.SetActiveState();
                break;
        }
    }

    public void Click(Action callBack)
    {
        CellButton.onClick.AddListener(() => callBack?.Invoke());
    }

    public void SetProcessState(double totalTime)
    {
        _progressBar.SetActive(true);
        AnimationUtility.Fill(_progressImage, _progressTimeText, (float)totalTime, null, () =>
        {
            _progressImage.fillAmount = 1;
            _progressBar.SetActive(false);
        });

        // _progressImage.fillAmount = currentTime / totalTime;
        // _progressTimeText.text = currentTime.ToString();

        // if (currentTime < 0.01f)
        // {
        //     _progressBar.SetActive(false);
        // }
    }

    // public List<GameCellView> CheckNeighbors()
    // {
    //     var hitedCells = new List<GameCellView>();

    //     for (int i = 0; i < _hitDirections.Length; i++)
    //     {
    //         RaycastHit2D hit = Physics2D.Raycast(transform.position, _hitDirections[i]);
    //         GameCellView hitedCell = hit.collider?.GetComponent<GameCellView>();

    //         if (hitedCell != null)
    //         {
    //             hitedCells.Add(hitedCell);
    //         }
    //     }

    //     return hitedCells;
    // }
}