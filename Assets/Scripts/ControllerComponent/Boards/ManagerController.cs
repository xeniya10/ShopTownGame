using System.Collections.Generic;
using ShopTown.PresenterComponent;
using VContainer;

namespace ShopTown.ControllerComponent
{
public class ManagerController : ImprovementController<ManagerPresenter>
{
    [Inject] private readonly IPresenterFactory<IManager> _factory;
    protected override string _key { get; set; } = "Managers";

    protected override void CreateBoard()
    {
        _presenters = new List<ImprovementPresenter>();

        foreach (var model in _models)
        {
            var view = _view.Instantiate(_board.GetManagerBoard());
            var manager = (ImprovementPresenter)_factory.Create(model, view);
            InitializeImprovement(manager);
        }
    }
}
}
