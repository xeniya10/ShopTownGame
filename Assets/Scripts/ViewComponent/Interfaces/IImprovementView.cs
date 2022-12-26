using ShopTown.ModelComponent;
using UnityEngine;

namespace ShopTown.ViewComponent
{
public interface IImprovementView : ICellView<IImprovementView, ImprovementModel>
{
    void SetImprovementSprite(Sprite sprite);

    void StartAnimation(ImprovementState state);

    void ActivateAnimation();
}
}
