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
        _model.IsActivated = true;
        SetState(ImprovementState.Lock);
    }

    protected override void SetSprite(IImprovementSprites improvementSprites)
    {
        _view.SetImprovementSprite(improvementSprites.GetManagerSprites(_model.Level));
    }

    protected override void SetName(IImprovementData improvementData)
    {
        _model.Name = improvementData.GetManagerName(_model.Level);
    }

    protected override void SetDescription(IImprovementData improvementData)
    {
        var businessName = improvementData.GetBusinessName(_model.Level);
        _model.Description = $"Hire manager to run your {businessName}";
    }

    protected override void SetCost(IImprovementData improvementData)
    {
        _model.Cost = improvementData.GetManagerCost(_model.Level);
    }
}
}
