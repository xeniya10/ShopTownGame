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
}
