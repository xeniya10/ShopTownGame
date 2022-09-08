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
    private int _rowNumber;

    [SerializeField]
    private int _columnNumber;

    [Header("Animation Durations")]
    public float ScaleTime;
    public float FadeTime;
    public float MoveTime;

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
        var cellSize = BoardCalculatorUtility.CalculateCellSize(_columnNumber, 0, 0) * 1.3f;
        var cellOffset = BoardCalculatorUtility.CalculateCellOffset(cellSize, 0);
        CellPrefab.SetSize(cellSize);

        for (int i = 0; i < _rowNumber; i++)
        {
            for (int j = 0; j < _columnNumber; j++)
            {
                var cell = Instantiate(CellPrefab, CellField);
                _cellList.Add(cell);
                cell.SetSprite(_cellList.IndexOf(cell));

                float startX = -Screen.width / 2 - cellSize - (cellOffset * j);
                float startY = Screen.height - cellSize - (cellOffset * i);
                cell.StartPosition = new Vector2(startX, startY);
                cell.SetPosition(cell.StartPosition);

                float targetX = Screen.width / 2 - (cellOffset * j);
                float targetY = Screen.height / 2 - cellSize / 2.3f - (cellOffset * i);
                cell.TargetPosition = new Vector2(targetX, targetY);

                var cellMoveAnimation = cell.transform.DOLocalMove(cell.TargetPosition, MoveTime / 2);
                _appearSequence.Append(cellMoveAnimation);
            }
        }
    }
    private void AppearGameName()
    {
        var startX = GameNameText.transform.localPosition.x - Screen.width;
        var startY = GameNameText.transform.localPosition.y;

        var startPosition = new Vector2(startX, startY);
        var targetPosition = GameNameText.transform.localPosition;

        GameNameText.transform.localPosition = startPosition;
        var moveAnimation = GameNameText.transform.DOLocalMove(targetPosition, MoveTime);
        _appearSequence.Append(moveAnimation);
    }
    private void AppearStartButton()
    {
        var startX = StartButton.transform.localPosition.x - Screen.width;
        var startY = StartButton.transform.localPosition.y;

        var startPosition = new Vector2(startX, startY);
        var targetPosition = StartButton.transform.localPosition;

        StartButton.transform.localPosition = startPosition;
        var moveAnimation = StartButton.transform.DOLocalMove(targetPosition, MoveTime);
        _appearSequence.Append(moveAnimation);
    }

    public void DisappearAnimation()
    {
        _disappearSequence = DOTween.Sequence();

        DisappearCells();
        DisappearStartButton();
        DisappearGameName();
        DisappearStartScreen();
    }

    private void DisappearCells()
    {
        for (int i = 0; i < _cellList.Count; i++)
        {
            var scaleAnimation = _cellList[i].transform.DOScale(new Vector2(0, 0), ScaleTime);
            _disappearSequence.Append(scaleAnimation);
        }
    }

    private void DisappearGameName()
    {
        var fadeAnimation = GameNameText.DOFade(0, FadeTime);
        _disappearSequence.Append(fadeAnimation);
    }

    private void DisappearStartButton()
    {
        var fadeAnimation = StartButtonText.DOFade(0, FadeTime);
        _disappearSequence.Append(fadeAnimation);
    }

    private void DisappearStartScreen()
    {
        var fadeAnimation = StartScreenImage.DOFade(0, FadeTime * 3);
        _disappearSequence.Append(fadeAnimation);
    }
}