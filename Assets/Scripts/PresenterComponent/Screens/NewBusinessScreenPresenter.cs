using ShopTown.ControllerComponent;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using VContainer;
using VContainer.Unity;

namespace ShopTown.PresenterComponent
{
public class NewBusinessScreenPresenter : ButtonSubscription, IInitializable
{
    [Inject] private readonly IGameData _data;
    [Inject] private readonly IGameBoardController _gameBoard;
    [Inject] private readonly IScreenView<GameCellModel> _view;

    public void Initialize()
    {
        _gameBoard.ActivateEvent += Show;
        SubscribeToButton(_view.GetHideButton(), () => _view.SetActive(false));
    }

    private void Show(GameCellModel model)
    {
        if (model.Level > _data.GameData.MaxOpenedLevel)
        {
            _view.Initialize(model);
            _view.SetActive(true);
            _data.GameData.SetMaxOpenedLevel(model.Level);
        }
    }
}
}
