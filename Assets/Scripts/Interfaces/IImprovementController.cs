using System;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;

public interface IImprovementController<T> : IDisposable
{
    event Action<ImprovementModel> ActivateEvent;

    void Initialize();

    void Unlock(int level, bool isCellActivated);

    IImprovement FindImprovement(int level);
}
