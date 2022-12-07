using System.Collections.Generic;
using DG.Tweening;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using VContainer;
using VContainer.Unity;

namespace ShopTown.PresenterComponent
{
public class SplashScreenPresenter : ButtonSubscription, IInitializable
{
    [Inject] private readonly ISplashCellView _cell;
    [Inject] private readonly ISplashScreenView _splashScreen;

    private readonly List<ISplashCellView> _cells = new List<ISplashCellView>();

    private Sequence _appearSequence;
    private Sequence _disappearSequence;

    public void Initialize()
    {
        Show();
        SubscribeToButton(_splashScreen.GetStartButton(), Hide);
    }

    private void Show()
    {
        _appearSequence = DOTween.Sequence();
        CreateBoard();
        _splashScreen.AppearAnimation(_appearSequence);
        _appearSequence.Play();
    }

    private void Hide()
    {
        _disappearSequence = DOTween.Sequence();
        _splashScreen.DisappearAnimation(_disappearSequence);
        _cells.ForEach(cell => cell.DisappearAnimation(_disappearSequence));
        _disappearSequence.Play();
    }

    private void CreateBoard()
    {
        var boardModel = new SplashBoardModel(7, 4);
        for (var i = 0; i < boardModel.Rows; i++)
        {
            for (var j = 0; j < boardModel.Columns; j++)
            {
                var size = boardModel.CalculateCellSize();
                var start = boardModel.CalculateStartPosition(j, i);
                var target = boardModel.CalculateTargetPosition(j, i);

                var view = _cell.Instantiate(_splashScreen.GetSplashField());
                var cellIndex = j + i * boardModel.Columns;
                var model = new SplashCellModel(cellIndex, size, start, target);
                view.Initialize(model);
                view.AppearAnimation(_appearSequence);
                _cells.Add(view);
            }
        }
    }
}
}
