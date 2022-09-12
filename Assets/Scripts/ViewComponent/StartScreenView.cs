using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using DG.Tweening;

public class StartScreenView : MonoBehaviour
{
    [Header("Components")]
    public StartImageCellView CellPrefab;
    public Image StartScreenImage;
    public Transform CellField;
    public Button StartButton;
    public TextMeshProUGUI StartButtonText;
    public TextMeshProUGUI GameNameText;

    [Header("Start Board Parameters")]

    [SerializeField]
    private int _numberOfRow;

    [SerializeField]
    private int _numberOfColumn;

    [Header("Animation Durations")]

    [SerializeField]
    private float _scaleTime;

    [SerializeField]
    private float _fadeTime;

    [SerializeField]
    private float _moveTime;
    private float _sizeFactor = 1.3f;

    private Sequence _appearSequence;
    private Sequence _disappearSequence;
    private List<StartImageCellView> _cellList = new List<StartImageCellView>();

    public void AppearAnimation()
    {
        _appearSequence = DOTween.Sequence();

        AppearCells();
        AppearGameName();
        AppearStartButton();
    }

    private void AppearCells()
    {
        var cellSize = BoardCalculatorUtility.CalculateCellSize(_numberOfRow, _numberOfColumn, 0, 0, _sizeFactor);
        CellPrefab.SetSize(cellSize);

        for (int i = 0; i < _numberOfRow; i++)
        {
            for (int j = 0; j < _numberOfColumn; j++)
            {
                var cell = Instantiate(CellPrefab, CellField);
                _cellList.Add(cell);
                cell.SetSprite(_cellList.IndexOf(cell));

                var startX = BoardCalculatorUtility.CalculateXPosition(-0.5f, cellSize, (j + 1));
                var startY = BoardCalculatorUtility.CalculateYPosition(1f, cellSize, (i + 1));
                cell.StartPosition = new Vector2(startX, startY);
                cell.SetPosition(cell.StartPosition);

                var targetX = BoardCalculatorUtility.CalculateXPosition(0.5f, cellSize, j);
                var targetY = BoardCalculatorUtility.CalculateYPosition(0.5f, cellSize, (i - 2.5f));
                cell.TargetPosition = new Vector2(targetX, targetY);

                var cellMoveAnimation = cell.transform.DOLocalMove(cell.TargetPosition, _moveTime * 0.5f);
                _appearSequence.Append(cellMoveAnimation);
            }
        }
    }
    private void AppearGameName()
    {
        var targetPosition = GameNameText.transform.localPosition;

        var startX = targetPosition.x - Screen.width * 1.5f;
        var startY = targetPosition.y;
        var startPosition = new Vector2(startX, startY);

        GameNameText.transform.localPosition = startPosition;
        var moveAnimation = GameNameText.transform.DOLocalMove(targetPosition, _moveTime);
        _appearSequence.Append(moveAnimation);
    }
    private void AppearStartButton()
    {
        var targetPosition = StartButton.transform.localPosition;

        var startX = targetPosition.x - Screen.width * 1.5f;
        var startY = targetPosition.y;
        var startPosition = new Vector2(startX, startY);

        StartButton.transform.localPosition = startPosition;
        var moveAnimation = StartButton.transform.DOLocalMove(targetPosition, _moveTime);
        _appearSequence.Append(moveAnimation);
    }

    public void DisappearAnimation()
    {
        _disappearSequence = DOTween.Sequence();

        DisappearStartButton();
        DisappearGameName();
        DisappearCells();
        DisappearStartScreen();
    }

    private void DisappearCells()
    {
        for (int i = 0; i < _cellList.Count; i++)
        {
            var scale = new Vector2(0, 0);
            var scaleAnimation = _cellList[i].transform.DOScale(scale, _scaleTime);
            _disappearSequence.Append(scaleAnimation);
        }
    }

    private void DisappearGameName()
    {
        var fadeAnimation = GameNameText.DOFade(0, _fadeTime);
        _disappearSequence.Append(fadeAnimation);
    }

    private void DisappearStartButton()
    {
        var fadeAnimation = StartButtonText.DOFade(0, _fadeTime);
        _disappearSequence.Append(fadeAnimation);
    }

    private void DisappearStartScreen()
    {
        var fadeAnimation = StartScreenImage.DOFade(0, _fadeTime);
        _disappearSequence.Append(fadeAnimation)
        .OnComplete(() => StartScreenImage.gameObject.SetActive(false));
    }
}