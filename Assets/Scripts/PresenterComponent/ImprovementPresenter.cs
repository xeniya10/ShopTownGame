using System;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;
using ShopTown.ViewComponent;

namespace ShopTown.PresenterComponent
{
public abstract class ImprovementPresenter
{
    public readonly ImprovementModel Model;
    protected readonly IImprovementView _view;
    protected readonly BusinessData _business;
    protected readonly ImprovementData _improvementData;
    protected readonly ImprovementCollection _improvementSprites;

    public Action ModelChangeEvent;

    protected ImprovementPresenter(ImprovementModel model, IImprovementView view, BusinessData business,
        ImprovementData improvementData, ImprovementCollection improvementSprites)
    {
        Model = model;
        _view = view;
        _business = business;
        _improvementData = improvementData;
        _improvementSprites = improvementSprites;
    }

    public void Initialize()
    {
        SetParameters();
        _view.Initialize(Model);
        SetSprite();
        SetState(Model.State);
    }

    public abstract void SubscribeToBuyButton(Action<ImprovementPresenter> callBack);

    public void SetState(ImprovementState state)
    {
        Model.SetState(state);
        if (Model.IsActivated)
        {
            Model.SetState(ImprovementState.Lock);
        }

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

    private void SetParameters()
    {
        SetName();
        SetDescription();
        SetCost();
    }

    protected abstract void SetSprite();

    protected abstract void SetName();

    protected abstract void SetDescription();

    protected abstract void SetCost();

    public abstract void Activate();
}

public class ManagerPresenter : ImprovementPresenter
{
    public ManagerPresenter(ImprovementModel model, IImprovementView view, BusinessData business,
        ImprovementData improvementData, ImprovementCollection improvementSprites) : base(model, view, business,
        improvementData, improvementSprites)
    {}

    public override void Activate()
    {
        _view.Activate();
        Model.IsActivated = true;
        SetState(ImprovementState.Lock);
    }

    public override void SubscribeToBuyButton(Action<ImprovementPresenter> callBack)
    {
        _view.SubscribeToBuyButton(() => callBack.Invoke(this));
    }

    protected override void SetSprite()
    {
        _view.SetImprovementSprite(_improvementSprites.ManagerSprites[Model.Level - 1]);
    }

    protected override void SetName()
    {
        Model.Name = _improvementData.ManagerNames[Model.Level - 1];
    }

    protected override void SetDescription()
    {
        var businessName = _business.LevelNames[Model.Level - 1];
        Model.Description = $"Hire manager to run your {businessName}";
    }

    protected override void SetCost()
    {
        Model.Cost = _improvementData.ManagerBaseCost[Model.Level - 1];
    }
}

public class UpgradePresenter : ImprovementPresenter
{
    public UpgradePresenter(ImprovementModel model, IImprovementView view, BusinessData business,
        ImprovementData improvementData, ImprovementCollection improvementSprites) : base(model, view, business,
        improvementData, improvementSprites)
    {}

    public override void SubscribeToBuyButton(Action<ImprovementPresenter> callBack)
    {
        _view.SubscribeToBuyButton(() => callBack.Invoke(this));
    }

    public override void Activate()
    {
        _view.Activate();
        LevelUp();
    }

    private void LevelUp()
    {
        if (Model.ImprovementLevel == 3)
        {
            SetState(ImprovementState.Lock);
            Model.IsActivated = true;
            return;
        }

        Model.ImprovementLevel += 1;
        _view.Initialize(Model);
    }

    protected override void SetSprite()
    {
        if (Model.ImprovementLevel == 2)
        {
            _view.SetImprovementSprite(_improvementSprites.SecondLevelUpgradeSprites[Model.Level - 1]);
            return;
        }

        if (Model.ImprovementLevel == 3)
        {
            _view.SetImprovementSprite(_improvementSprites.ThirdLevelUpgradeSprites[Model.Level - 1]);
            return;
        }

        _view.SetImprovementSprite(_improvementSprites.FirstLevelUpgradeSprites[Model.Level - 1]);
    }

    protected override void SetName()
    {
        if (Model.ImprovementLevel == 2)
        {
            Model.Name = _improvementData.SecondLevelUpgradeNames[Model.Level - 1];
            return;
        }

        if (Model.ImprovementLevel == 3)
        {
            Model.Name = _improvementData.ThirdLevelUpgradeNames[Model.Level - 1];
            return;
        }

        Model.Name = _improvementData.FirstLevelUpgradeNames[Model.Level - 1];
    }

    protected override void SetDescription()
    {
        var businessName = _business.LevelNames[Model.Level - 1];
        var multiplier = Model.ImprovementLevel + 1;
        Model.Description = $"Increase {businessName} profit x{multiplier.ToString()}";
    }

    protected override void SetCost()
    {
        var baseCost = _improvementData.UpgradeBaseCost[Model.Level - 1];
        Model.Cost = new MoneyModel(baseCost.Number * Model.ImprovementLevel, baseCost.Value);
    }
}
}
