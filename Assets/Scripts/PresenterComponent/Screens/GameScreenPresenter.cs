using ShopTown.ControllerComponent;
using ShopTown.ViewComponent;
using VContainer;
using VContainer.Unity;

namespace ShopTown.PresenterComponent
{
public class GameScreenPresenter : IInitializable
{
    [Inject] private readonly IGameData _data;
    [Inject] private readonly IButtonSubscriber _subscriber;
    [Inject] private readonly IGameScreenView _gameScreen;
    [Inject] private readonly IMenuScreenView _menuScreen;
    [Inject] private readonly IPurchaseScreenView _purchaseScreen;

    public void Initialize()
    {
        _gameScreen.Initialize(_data.GameData);
        _data.GameData.ChangeEvent += () => _gameScreen.Initialize(_data.GameData);
        _subscriber.AddListenerToButton(_gameScreen.GetDollarAddButton(), _purchaseScreen.ChangeActivation);
        _subscriber.AddListenerToButton(_gameScreen.GetGoldAddButton(), _purchaseScreen.ChangeActivation);
        _subscriber.AddListenerToButton(_gameScreen.GetMenuButton(), _menuScreen.ChangeActivation);
    }
}
}
