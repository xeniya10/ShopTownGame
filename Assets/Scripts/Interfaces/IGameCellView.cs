using System;
using ShopTown.ModelComponent;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public interface IGameCellView : IInstantiatable<IGameCellView>, IInitializable<GameCellModel>, IBuyButton
{
    void InitializeImprovements(GameCellModel model);

    void StartAnimation(GameCellModel model, Action onCompleteAnimation = null);

    void StartLevelUpAnimation(GameCellModel model);

    void SetActiveSelector(bool isActivated);

    Button GetCellButton();
}
}
