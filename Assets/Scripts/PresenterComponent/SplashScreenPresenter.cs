using System;
using System.Collections.Generic;
using DG.Tweening;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using UnityEngine.UI;
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
        SubscribeToButton(_splashScreen.StartButton, HideSplash);
    }

    private void SubscribeToButton(Button button, Action callBack)
    {
        button.onClick.AddListener(() => callBack?.Invoke());
    }

    private void ShowSplash()
    {
        CreateBoard();
        _splashScreen.AppearTextFields();
        _splashScreen.AppearSequence.Play();
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
        _splashScreen.DisappearSequence.Play();
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
