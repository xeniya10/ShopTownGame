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
        if (Model.ImprovementLevel == 3)
        {
            SetState(ImprovementState.Lock);
            Model.IsActivated = true;
            return;
        }

        Model.ImprovementLevel += 1;
        _view.Initialize(Model);
    }

    protected override void SetSprite(ImprovementContainer improvementSprites)
    {
        if (Model.ImprovementLevel == 2)
        {
            _view.SetImprovementSprite(improvementSprites.SecondLevelUpgradeSprites[Model.Level - 1]);
            return;
        }

        if (Model.ImprovementLevel == 3)
        {
            _view.SetImprovementSprite(improvementSprites.ThirdLevelUpgradeSprites[Model.Level - 1]);
            return;
        }

        _view.SetImprovementSprite(improvementSprites.FirstLevelUpgradeSprites[Model.Level - 1]);
    }

    protected override void SetName(ImprovementData improvementData)
    {
        if (Model.ImprovementLevel == 2)
        {
            Model.Name = improvementData.SecondLevelUpgradeNames[Model.Level - 1];
            return;
        }

        if (Model.ImprovementLevel == 3)
        {
            Model.Name = improvementData.ThirdLevelUpgradeNames[Model.Level - 1];
            return;
        }

        Model.Name = improvementData.FirstLevelUpgradeNames[Model.Level - 1];
    }

    protected override void SetDescription(ImprovementData improvementData)
    {
        var businessName = improvementData.BusinessNames.Names[Model.Level - 1];
        var multiplier = Model.ImprovementLevel + 1;
        Model.Description = $"Increase {businessName} profit x{multiplier.ToString()}";
    }

    protected override void SetCost(ImprovementData improvementData)
    {
        var baseCost = improvementData.UpgradeBaseCost[Model.Level - 1];
        Model.Cost = new MoneyModel(baseCost.Number * Model.ImprovementLevel, baseCost.Value);
    }
}
}
