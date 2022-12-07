using System.Collections.Generic;
using ShopTown.PresenterComponent;

namespace ShopTown.ControllerComponent
{
public class ManagerController : ImprovementController<ManagerPresenter>
{
    protected override string _key { get; set; } = "Managers";

    protected override void CreateBoard()
    {
        _presenters = new List<ImprovementPresenter>();

        foreach (var model in _models)
        {
            var view = _view.Instantiate(_board.GetManagerBoard());
            ImprovementPresenter manager = new ManagerPresenter(model, view);
            InitializeImprovement(manager);
        }
    }
}
}
