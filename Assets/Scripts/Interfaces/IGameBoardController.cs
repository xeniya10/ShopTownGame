using System;
using ShopTown.ModelComponent;

public interface IGameBoardController
{
    event Action<GameCellModel> ActivateEvent;
    event Action<int, bool> UnlockEvent;

    void Initialize();

    void InitializeManager(ImprovementModel improvement);

    void InitializeUpgrade(ImprovementModel improvement);
}
