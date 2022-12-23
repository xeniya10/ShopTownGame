using ShopTown.ControllerComponent;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using VContainer;
using VContainer.Unity;

namespace ShopTown.PresenterComponent
{
public class NewBusinessScreenPresenter : IInitializable, IShowable<GameCellModel>
{
    [Inject] private readonly IGameData _data;
    [Inject] private readonly IPlayable _audio;
    [Inject] private readonly IButtonSubscriber _subscriber;
    [Inject] private readonly IAdsView _ads;
    [Inject] private readonly IScreenView<GameCellModel> _view;

    public void Initialize()
    {
        _subscriber.AddListenerToButton(_view.GetHideButton(), () =>
        {
            _view.SetActive(false);
            _ads.ShowInterstitialAd();
        });
    }

    public void Show(GameCellModel model)
    {
        if (model.Level > _data.GameData.MaxOpenedLevel)
        {
            _view.Initialize(model);
            _audio.PlayNewBusinessSound();
            _view.SetActive(true);
            _data.GameData.SetMaxOpenedLevel(model.Level);
        }
    }
}
}
