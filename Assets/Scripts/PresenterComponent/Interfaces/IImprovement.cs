using System;
using ShopTown.ControllerComponent;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;

namespace ShopTown.PresenterComponent
{
public interface IImprovement
{
    public ImprovementModel Model { get; }
    public event Action ChangeEvent;

    void Initialize(IImprovementData improvementData, IImprovementSprites improvementSprites);

    void AddListenerToBuyButton(IButtonSubscriber subscriber, Action<IImprovement> callBack);

    void SetState(ImprovementState state);

    void Activate(IGameData _data);
}

public interface IManager : IImprovement
{}

public interface IUpgrade : IImprovement
{}
}
