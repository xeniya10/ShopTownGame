using System;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;

public interface IImprovementController<T> : IDisposable
{
    event Action<ImprovementModel> ImprovementActivationEvent;

    void Initialize();

    void UnlockImprovement(int level, bool isCellActivated);

    IImprovement FindImprovement(int level);
}
