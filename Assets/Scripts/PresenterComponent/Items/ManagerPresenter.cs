using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;
using ShopTown.ViewComponent;

namespace ShopTown.PresenterComponent
{
public class ManagerPresenter : ImprovementPresenter, IManager
{
    public ManagerPresenter(IModel model, IView view) : base(model, view)
    {}

    public override void Activate()
    {
        _view.ActivateAnimation();
        Model.IsActivated = true;
        SetState(ImprovementState.Lock);
    }

    protected override void SetSprite(ImprovementContainer improvementSprites)
    {
        _view.SetImprovementSprite(improvementSprites.ManagerSprites[Model.Level - 1]);
    }

    protected override void SetName(ImprovementData improvementData)
    {
        Model.Name = improvementData.ManagerNames[Model.Level - 1];
    }

    protected override void SetDescription(ImprovementData improvementData)
    {
        var businessName = improvementData.BusinessNames.Names[Model.Level - 1];
        Model.Description = $"Hire manager to run your {businessName}";
    }

    protected override void SetCost(ImprovementData improvementData)
    {
        Model.Cost = improvementData.ManagerBaseCost[Model.Level - 1];
    }
}
}
