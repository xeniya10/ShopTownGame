using ShopTown.ViewComponent;
using VContainer;

namespace ShopTown.PresenterComponent
{
public interface ICreatable<P, M>
{
    P Create(M m);
}

public class Presenter<V, M>
{
    [Inject]
    private readonly GameScreenView _gameScreen;
    private readonly V _view;
    public readonly M Model;

    public Presenter(M model, V view)
    {
        Model = model;
        _view = view;
    }

    // public void SetState(UpgradeState state)
    // {
    //     Model.SetState(state);
    //
    //     switch (state)
    //     {
    //         case UpgradeState.Hide:
    //             _view.Hide();
    //             break;
    //
    //         case UpgradeState.Lock:
    //             _view.Lock();
    //             break;
    //
    //         case UpgradeState.Unlock:
    //             _view.Unlock();
    //             break;
    //     }
    // }
    //
    // public void Activate()
    // {
    //     _view.Salute.Play();
    //     LevelUp();
    // }
    //
    // public void SubscribeToBuyButton(Action<UpgradeRowPresenter> callBack)
    // {
    //     _view.BuyButton.onClick.AddListener(() => callBack?.Invoke(this));
    // }
}
}
