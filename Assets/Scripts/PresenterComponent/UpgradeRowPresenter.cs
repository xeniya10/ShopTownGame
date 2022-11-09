using System;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using VContainer;

namespace ShopTown.PresenterComponent
{
public class UpgradeRowPresenter : ICreatable<UpgradeRowPresenter, UpgradeRowModel>
{
    [Inject]
    private readonly GameScreenView _gameScreen;
    private readonly UpgradeRowView _view;
    public readonly UpgradeRowModel Model;

    private UpgradeRowPresenter(UpgradeRowModel model, UpgradeRowView view)
    {
        Model = model;
        _view = view;
    }

    public UpgradeRowPresenter Create(UpgradeRowModel model)
    {
        var view = _view.Create(_gameScreen.UpgradeBoard);
        view.Initialize(model);
        var presenter = new UpgradeRowPresenter(model, view);
        presenter.SetState(model.State);
        return presenter;
    }

    public void SetState(UpgradeState state)
    {
        Model.SetState(state);

        switch (state)
        {
            case UpgradeState.Hide:
                _view.Hide();
                break;

            case UpgradeState.Lock:
                _view.Lock();
                break;

            case UpgradeState.Unlock:
                _view.Unlock();
                break;
        }
    }

    public void Activate()
    {
        _view.Salute.Play();
        LevelUp();
    }

    private void LevelUp()
    {
        if (Model.UpgradeLevel == 3)
        {
            SetState(UpgradeState.Lock);
            Model.AreAllLevelsActivated = true;
            return;
        }

        Model.UpgradeLevel += 1;
        _view.Initialize(Model);
    }

    public void SubscribeToBuyButton(Action<UpgradeRowPresenter> callBack)
    {
        _view.BuyButton.onClick.AddListener(() => callBack?.Invoke(this));
    }
}
}
