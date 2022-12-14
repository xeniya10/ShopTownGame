using System.Collections.Generic;
using ShopTown.PresenterComponent;
using VContainer;

namespace ShopTown.ControllerComponent
{
public class UpgradeController : ImprovementController<UpgradePresenter>
{
    [Inject] private readonly IPresenterFactory<IUpgrade> _factory;
    protected override string _key { get; set; } = "Upgrades";

    protected override void CreateBoard()
    {
        _presenters = new List<ImprovementPresenter>();

        foreach (var model in _models)
        {
            var view = _view.Instantiate(_board.GetUpgradeBoard());
            ImprovementPresenter upgrade = new UpgradePresenter(model, view);
            InitializeImprovement(upgrade);
        }
    }
}
}
