using ShopTown.ModelComponent;
using UnityEngine;

namespace ShopTown.ViewComponent
{
public interface IImprovementView : IInstantiatable<IImprovementView>, IInitializable<ImprovementModel>, IBuyButton
{
    void SetImprovementSprite(Sprite sprite);

    void StartAnimation(ImprovementState state);

    void ActivateAnimation();
}
}
