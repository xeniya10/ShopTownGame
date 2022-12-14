using System;
using ShopTown.ModelComponent;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public interface IGameCellView : ICellView<IGameCellView, GameCellModel>
{
    void InitializeImprovements(GameCellModel model);

    void StartAnimation(GameCellModel model, Action onCompleteAnimation = null);

    void StartLevelUpAnimation(GameCellModel model);

    void SetActiveSelector(bool isActivated);

    Button GetCellButton();
}
}
