using System;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;
using ShopTown.ViewComponent;

namespace ShopTown.PresenterComponent
{
public abstract class ImprovementPresenter : ButtonSubscription
{
    public readonly ImprovementModel Model;
    protected readonly IImprovementView _view;

    public Action ModelChangeEvent;

    protected ImprovementPresenter(ImprovementModel model, IImprovementView view)
    {
        Model = model;
        _view = view;
    }

    public void Initialize(ImprovementData improvementData, ImprovementContainer improvementSprites)
    {
        SetParameters(improvementData);
        _view.Initialize(Model);
        SetSprite(improvementSprites);
        SetState(Model.State);
    }

    public void SubscribeToBuyButton(Action<ImprovementPresenter> callBack)
    {
        SubscribeToButton(_view.GetBuyButton(), () => callBack?.Invoke(this));
    }

    public void SetState(ImprovementState state)
    {
        Model.State = state;
        if (Model.IsActivated)
        {
            Model.State = ImprovementState.Lock;
        }

        _view.StartAnimation(Model.State);
        ModelChangeEvent?.Invoke();
    }

    private void SetParameters(ImprovementData improvementData)
    {
        SetName(improvementData);
        SetDescription(improvementData);
        SetCost(improvementData);
    }

    protected abstract void SetSprite(ImprovementContainer improvementSprites);

    protected abstract void SetName(ImprovementData improvementData);

    protected abstract void SetDescription(ImprovementData improvementData);

    protected abstract void SetCost(ImprovementData improvementData);

    public abstract void Activate();
}

public class ManagerPresenter : ImprovementPresenter
{
    public ManagerPresenter(ImprovementModel model, IImprovementView view) : base(model, view)
    {}

    public override void Activate()
    {
        _view.ActivateAnimation();
        Model.IsActivated = true;
        SetState(ImprovementState.Lock);
    }

    protected override void SetSprite(ImprovementContainer improvementSprites)
    {
        _view.SetImprovementSprite(improvementSprites.ManagerSprites[Model.Level - 1]);
    }

    protected override void SetName(ImprovementData improvementData)
    {
        Model.Name = improvementData.ManagerNames[Model.Level - 1];
    }

    protected override void SetDescription(ImprovementData improvementData)
    {
        var businessName = improvementData.BusinessNames.LevelNames[Model.Level - 1];
        Model.Description = $"Hire manager to run your {businessName}";
    }

    protected override void SetCost(ImprovementData improvementData)
    {
        Model.Cost = improvementData.ManagerBaseCost[Model.Level - 1];
    }
}

public class UpgradePresenter : ImprovementPresenter
{
    public UpgradePresenter(ImprovementModel model, IImprovementView view) : base(model, view)
    {}

    public override void Activate()
    {
        _view.ActivateAnimation();
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

    protected override void SetSprite(ImprovementContainer improvementSprites)
    {
        if (Model.ImprovementLevel == 2)
        {
            _view.SetImprovementSprite(improvementSprites.SecondLevelUpgradeSprites[Model.Level - 1]);
            return;
        }

        if (Model.ImprovementLevel == 3)
        {
            _view.SetImprovementSprite(improvementSprites.ThirdLevelUpgradeSprites[Model.Level - 1]);
            return;
        }

        _view.SetImprovementSprite(improvementSprites.FirstLevelUpgradeSprites[Model.Level - 1]);
    }

    protected override void SetName(ImprovementData improvementData)
    {
        if (Model.ImprovementLevel == 2)
        {
            Model.Name = improvementData.SecondLevelUpgradeNames[Model.Level - 1];
            return;
        }

        if (Model.ImprovementLevel == 3)
        {
            Model.Name = improvementData.ThirdLevelUpgradeNames[Model.Level - 1];
            return;
        }

        Model.Name = improvementData.FirstLevelUpgradeNames[Model.Level - 1];
    }

    protected override void SetDescription(ImprovementData improvementData)
    {
        var businessName = improvementData.BusinessNames.LevelNames[Model.Level - 1];
        var multiplier = Model.ImprovementLevel + 1;
        Model.Description = $"Increase {businessName} profit x{multiplier.ToString()}";
    }

    protected override void SetCost(ImprovementData improvementData)
    {
        var baseCost = improvementData.UpgradeBaseCost[Model.Level - 1];
        Model.Cost = new MoneyModel(baseCost.Number * Model.ImprovementLevel, baseCost.Value);
    }
}
}
