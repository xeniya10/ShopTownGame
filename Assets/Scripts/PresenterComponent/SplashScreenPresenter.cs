using System.Collections.Generic;
using ShopTown.ModelComponent;
using VContainer.Unity;
using ShopTown.ViewComponent;

namespace ShopTown.PresenterComponent
{
public class SplashScreenPresenter : IInitializable
{
    private readonly SplashCellView _viewPrefab;
    private readonly SplashScreenModel _model;
    private readonly SplashScreenView _splashScreenView;

    private readonly List<SplashCellView> _cells = new List<SplashCellView>();

    public SplashScreenPresenter(SplashCellView viewPrefab, SplashScreenModel model, SplashScreenView splashScreenView)
    {
        _viewPrefab = viewPrefab;
        _model = model;
        _splashScreenView = splashScreenView;
    }

    public void Initialize()
    {
        _splashScreenView.InitializeSequences();
        ShowSplash();
        _splashScreenView.ClickStartButton(HideSplash);
    }

    private void ShowSplash()
    {
        CreateBoard();
        _splashScreenView.AppearTextFields();
        _splashScreenView.PlayAppearSequence();
    }

    private void CreateBoard()
    {
        _viewPrefab.SetSize(_model.CellSize);

        for (var i = 0; i < _model.Rows; i++)
        {
            for (var j = 0; j < _model.Columns; j++)
            {
                var start = _model.CalculateStartPosition(j, i);
                var target = _model.CalculateTargetPosition(j, i);

                var cell = _viewPrefab.Create(_splashScreenView.CellField);
                _cells.Add(cell);
                var cellIndex = _cells.IndexOf(cell);
                cell.Initialize(cellIndex, start, target);
                _splashScreenView.AppearCell(cell);
            }
        }
    }

    private void HideSplash()
    {
        _splashScreenView.DisappearTextFields();
        HideBoard();
        _splashScreenView.PlayDisappearSequence();
    }

    private void HideBoard()
    {
        foreach (var cell in _cells)
        {
            _splashScreenView.DisappearCells(cell);
        }
    }
}
}
