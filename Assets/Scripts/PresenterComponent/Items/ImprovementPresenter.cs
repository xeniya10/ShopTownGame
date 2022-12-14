using System;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;
using ShopTown.ViewComponent;

namespace ShopTown.PresenterComponent
{
public abstract class ImprovementPresenter : ButtonSubscription, IImprovement
{
    public ImprovementModel Model { get { return _model; } }

    protected readonly ImprovementModel _model;
    protected readonly IImprovementView _view;

    public event Action ChangeEvent;

    protected ImprovementPresenter(IModel model, IView view)
    {
        _model = (ImprovementModel)model;
        _view = (IImprovementView)view;
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
        ChangeEvent?.Invoke();
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
