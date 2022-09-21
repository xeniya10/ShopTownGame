using System.Collections.Generic;
using VContainer.Unity;

public class StartScreenPresenter : IInitializable
{
    private readonly StartImageCellView _cellPrefab;
    private readonly StartScreenModel _startScreenModel;
    private readonly StartScreenView _startScreenView;

    private readonly List<StartImageCellView> _cellList = new List<StartImageCellView>();

    public StartScreenPresenter(StartImageCellView cellPrefab,
    StartScreenModel startScreenModel,
    StartScreenView startScreenView)
    {
        _cellPrefab = cellPrefab;
        _startScreenModel = startScreenModel;
        _startScreenView = startScreenView;
    }

    public void Initialize()
    {
        CreateBoard();

        _startScreenView.AppearAnimation(_cellList);
        _startScreenView.ClickStartButton(
            () => _startScreenView.DisappearAnimation(_cellList));
    }

    public void CreateBoard()
    {
        var cellSize = _startScreenModel.CalculateCellSize();
        _cellPrefab.SetSize(cellSize);

        for (int i = 0; i < _startScreenModel.Rows; i++)
        {
            for (int j = 0; j < _startScreenModel.Columns; j++)
            {
                var cell = _cellPrefab.Create(_startScreenView.CellField);
                _cellList.Add(cell);
                cell.SetSprite(_cellList.IndexOf(cell));

                cell.StartPosition = _startScreenModel.CalculateStartPosition(j, i, cellSize);
                cell.TargetPosition = _startScreenModel.CalculateTargetPosition(j, i, cellSize);
                cell.SetPosition(cell.StartPosition);
            }
        }
    }
}