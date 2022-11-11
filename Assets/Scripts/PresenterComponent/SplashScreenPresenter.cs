using System.Collections.Generic;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using VContainer.Unity;

namespace ShopTown.PresenterComponent
{
public class SplashScreenPresenter : IInitializable
{
    private readonly SplashCellView _view;
    private readonly SplashBoardModel _model;
    private readonly SplashScreenView _splashScreen;

    private readonly List<SplashCellView> _cells = new List<SplashCellView>();

    public SplashScreenPresenter(SplashCellView view, SplashBoardModel model, SplashScreenView splashScreen)
    {
        _view = view;
        _model = model;
        _splashScreen = splashScreen;
    }

    public void Initialize()
    {
        _splashScreen.InitializeSequences();
        ShowSplash();
        _splashScreen.SubscribeToStartButton(HideSplash);
    }

    private void ShowSplash()
    {
        CreateBoard();
        _splashScreen.AppearTextFields();
        _splashScreen.PlayAppearSequence();
    }

    private void CreateBoard()
    {
        _view.SetSize(_model.CalculateCellSize());

        for (var i = 0; i < _model.Rows; i++)
        {
            for (var j = 0; j < _model.Columns; j++)
            {
                var start = _model.CalculateStartPosition(j, i);
                var target = _model.CalculateTargetPosition(j, i);

                var cell = _view.Create(_splashScreen.CellField);
                _cells.Add(cell);
                var cellIndex = _cells.IndexOf(cell);
                cell.Initialize(cellIndex, start, target);
                _splashScreen.AppearCell(cell);
            }
        }
    }

    private void HideSplash()
    {
        _splashScreen.DisappearTextFields();
        HideBoard();
        _splashScreen.PlayDisappearSequence();
    }

    private void HideBoard()
    {
        foreach (var cell in _cells)
        {
            _splashScreen.DisappearCells(cell);
        }
    }
}
}
