using System;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;

namespace ShopTown.PresenterComponent
{
public interface IImprovement
{
    public ImprovementModel Model { get; }
    public event Action ChangeEvent;

    void Initialize(ImprovementData improvementData, ImprovementContainer improvementSprites);

    void SubscribeToBuyButton(Action<ImprovementPresenter> callBack);

    void SetState(ImprovementState state);

    void Activate();
}

public interface IManager
{}

public interface IUpgrade
{}
}
