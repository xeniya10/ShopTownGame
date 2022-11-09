using System;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using VContainer;

namespace ShopTown.PresenterComponent
{
public class ManagerRowPresenter : ICreatable<ManagerRowPresenter, ManagerRowModel>
{
    [Inject]
    private readonly GameScreenView _gameScreen;
    private readonly ManagerRowView _view;
    public readonly ManagerRowModel Model;

    public ManagerRowPresenter(ManagerRowModel model, ManagerRowView view)
    {
        Model = model;
        _view = view;
    }

    public ManagerRowPresenter Create(ManagerRowModel model)
    {
        var view = _view.Create(_gameScreen.ManagerBoard);
        view.Initialize(model);
        var presenter = new ManagerRowPresenter(model, view);
        presenter.SetState(model.State);
        return presenter;
    }

    public void SetState(ManagerState state)
    {
        Model.SetState(state);

        switch (state)
        {
            case ManagerState.Hide:
                _view.Hide();
                break;

            case ManagerState.Lock:
                _view.Lock();
                break;

            case ManagerState.Unlock:
                _view.Unlock();
                break;
        }
    }

    public void Activate()
    {
        _view.Salute.Play();
        SetState(ManagerState.Lock);
        Model.IsActivated = true;
    }

    public void SubscribeToHireButton(Action<ManagerRowPresenter> callBack)
    {
        _view.HireButton.onClick.AddListener(() => callBack?.Invoke(this));
    }
}
}
