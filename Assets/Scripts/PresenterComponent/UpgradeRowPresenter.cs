using System;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.ViewComponent;
using VContainer;

namespace ShopTown.PresenterComponent
{
public class UpgradeRowPresenter : ICreatable<UpgradeRowPresenter, UpgradeRowModel>
{
    [Inject] private readonly BusinessData _business;
    [Inject] private readonly UpgradeRowData _rowData;
    [Inject] private readonly GameScreenView _gameScreen;
    private readonly UpgradeRowView _view;
    public readonly UpgradeRowModel Model;

    public Action ModelChangeEvent;

    private UpgradeRowPresenter(UpgradeRowModel model, UpgradeRowView view)
    {
        Model = model;
        _view = view;
    }

    public UpgradeRowPresenter Create(UpgradeRowModel model)
    {
        SetParameter(model);
        var view = _view.Create(_gameScreen.UpgradeBoard);
        view.Initialize(model);
        var presenter = new UpgradeRowPresenter(model, view);
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
        LevelUp();
    }

    private void LevelUp()
    {
        if (Model.UpgradeLevel == 3)
        {
            SetState(ImprovementState.Lock);
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

    private void SetParameter(UpgradeRowModel model)
    {
        SetName(model);
        SetDescription(model);
        SetCost(model);
    }

    private void SetName(UpgradeRowModel model)
    {
        if (model.UpgradeLevel == 2)
        {
            model.Name = _rowData.SecondLevelNames[model.Level - 1];
            return;
        }

        if (model.UpgradeLevel == 3)
        {
            model.Name = _rowData.ThirdLevelNames[model.Level - 1];
            return;
        }

        model.Name = _rowData.FirstLevelNames[model.Level - 1];
    }

    private void SetDescription(UpgradeRowModel model)
    {
        var businessName = _business.LevelNames[model.Level - 1];
        model.Description = $"Increase {businessName} profit x{model.UpgradeLevel + 1}";
    }

    private void SetCost(UpgradeRowModel model)
    {
        var baseCost = _rowData.BaseCost[model.Level - 1];
        model.Cost = new MoneyModel(baseCost.Number * model.UpgradeLevel, baseCost.Value);
    }
}
}
