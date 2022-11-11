using System;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using VContainer;

namespace ShopTown.PresenterComponent
{
public class ManagerRowPresenter : ICreatable<ManagerRowPresenter, ManagerRowModel>
{
    [Inject] private readonly GameScreenView _gameScreen;
    [Inject] private readonly BusinessData _business;
    [Inject] private readonly ManagerRowData _rowData;
    private readonly ManagerRowView _view;
    public readonly ManagerRowModel Model;

    public Action ModelChangeEvent;

    public ManagerRowPresenter(ManagerRowModel model, ManagerRowView view)
    {
        Model = model;
        _view = view;
    }

    public ManagerRowPresenter Create(ManagerRowModel model)
    {
        SetParameter(model);
        var view = _view.Create(_gameScreen.ManagerBoard);
        view.Initialize(model);
        var presenter = new ManagerRowPresenter(model, view);
        presenter.SetState(model.State);
        return presenter;
    }

    public void SetState(ImprovementState state)
    {
        Model.SetState(state);

        switch (state)
        {
            case ImprovementState.Hide:
                _view.Hide();
                break;

            case ImprovementState.Lock:
                _view.Lock();
                break;

            case ImprovementState.Unlock:
                _view.Unlock();
                break;
        }

        ModelChangeEvent?.Invoke();
    }

    public void Activate()
    {
        _view.Salute.Play();
        Model.IsActivated = true;
        SetState(ImprovementState.Lock);
    }

    public void SubscribeToHireButton(Action<ManagerRowPresenter> callBack)
    {
        _view.HireButton.onClick.AddListener(() => callBack?.Invoke(this));
    }

    private void SetParameter(ManagerRowModel model)
    {
        SetName(model);
        SetDescription(model);
        SetCost(model);
    }

    private void SetName(ManagerRowModel model)
    {
        model.Name = _rowData.ManagerNames[model.Level - 1];
    }

    private void SetDescription(ManagerRowModel model)
    {
        var businessName = _business.LevelNames[model.Level - 1];
        model.Description = $"Hire manager to run your {businessName}";
    }

    private void SetCost(ManagerRowModel model)
    {
        model.Cost = _rowData.BaseCost[model.Level - 1];
    }
}
}
