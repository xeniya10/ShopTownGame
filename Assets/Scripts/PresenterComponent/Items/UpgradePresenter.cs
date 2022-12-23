using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.SpriteContainer;
using ShopTown.ViewComponent;

namespace ShopTown.PresenterComponent
{
public class UpgradePresenter : ImprovementPresenter, IUpgrade
{
    public UpgradePresenter(IModel model, IView view) : base(model, view)
    {}

    public override void Activate()
    {
        _view.ActivateAnimation();
        LevelUp();
    }

    private void LevelUp()
    {
        if (_model.ImprovementLevel == 3)
        {
            SetState(ImprovementState.Lock);
            _model.IsActivated = true;
            return;
        }

        _model.ImprovementLevel += 1;
        _view.Initialize(_model);
    }

    protected override void SetSprite(IImprovementSprites improvementSprites)
    {
        _view.SetImprovementSprite(improvementSprites.GetUpgradeSprites(_model.Level, _model.ImprovementLevel));
    }

    protected override void SetName(IImprovementData improvementData)
    {
        _model.Name = improvementData.GetUpgradeNames(_model.Level, _model.ImprovementLevel);
    }

    protected override void SetDescription(IImprovementData improvementData)
    {
        var businessName = improvementData.GetBusinessName(_model.Level);
        var multiplier = _model.ImprovementLevel + 1;
        _model.Description = $"Increase {businessName} profit x{multiplier.ToString()}";
    }

    protected override void SetCost(IImprovementData improvementData)
    {
        var baseCost = improvementData.GetUpgradeCost(_model.Level);
        _model.Cost = new MoneyModel(baseCost.Number * _model.ImprovementLevel, baseCost.Value);
    }
}
}
