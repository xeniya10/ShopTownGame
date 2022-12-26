using System;
using ShopTown.ControllerComponent;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;
using ShopTown.ViewComponent;

namespace ShopTown.PresenterComponent
{
public abstract class ImprovementPresenter : IImprovement
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

    public void Initialize(IImprovementData improvementData, IImprovementSprites improvementSprites)
    {
        SetParameters(improvementData);
        _view.Initialize(_model);
        SetSprite(improvementSprites);
        SetState(_model.State);
    }

    public void AddListenerToBuyButton(IButtonSubscriber subscriber, Action<IImprovement> callBack)
    {
        subscriber.AddListenerToButton(_view.GetBuyButton(), () => callBack?.Invoke(this));
    }

    public void SetState(ImprovementState state)
    {
        _model.State = state;
        if (_model.IsActivated)
        {
            _model.State = ImprovementState.Lock;
        }

        _view.StartAnimation(_model.State);
        ChangeEvent?.Invoke();
    }

    private void SetParameters(IImprovementData improvementData)
    {
        SetName(improvementData);
        SetDescription(improvementData);
        SetCost(improvementData);
    }

    protected abstract void SetSprite(IImprovementSprites improvementSprites);

    protected abstract void SetName(IImprovementData improvementData);

    protected abstract void SetDescription(IImprovementData improvementData);

    protected abstract void SetCost(IImprovementData improvementData);

    public abstract void Activate(IGameData _data);
}
}
