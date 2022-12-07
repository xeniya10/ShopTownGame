using System;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;

public interface IImprovementController<T>
{
    event Action<ImprovementModel> ActivateEvent;

    void Initialize();

    void Unlock(int level, bool isCellActivated);

    ImprovementPresenter FindImprovement(int level);
}
