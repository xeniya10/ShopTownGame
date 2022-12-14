using ShopTown.ControllerComponent;
using ShopTown.ViewComponent;
using VContainer;
using VContainer.Unity;

namespace ShopTown.PresenterComponent
{
public class GameScreenPresenter : ButtonSubscription, IInitializable
{
    [Inject] private readonly IGameData _data;
    [Inject] private readonly IGameScreenView _gameScreen;
    [Inject] public readonly IMenuScreenView _menuScreen;
    [Inject] public readonly IPurchaseScreenView _purchaseScreen;

    public void Initialize()
    {
        _gameScreen.Initialize(_data.GameData);
        _data.GameData.ChangeEvent += () => _gameScreen.Initialize(_data.GameData);
        SubscribeToButton(_gameScreen.GetDollarAddButton(), _purchaseScreen.ChangeActivation);
        SubscribeToButton(_gameScreen.GetGoldAddButton(), _purchaseScreen.ChangeActivation);
        SubscribeToButton(_gameScreen.GetMenuButton(), _menuScreen.ChangeActivation);
    }
}
}
