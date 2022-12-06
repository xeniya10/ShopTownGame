using System;
using System.Collections.Generic;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using ShopTown.SpriteContainer;
using ShopTown.ViewComponent;
using VContainer;

namespace ShopTown.ControllerComponent
{
public abstract class ImprovementController : StorageManager, IDisposable
{
    [Inject] protected readonly IGameData _data;
    [Inject] protected readonly IImprovementView _view;
    [Inject] protected readonly IBoard _board;
    [Inject] protected readonly ImprovementData _improvementData;
    [Inject] protected readonly ImprovementContainer _improvementSprites;

    protected abstract string _key { get; set; }

    protected List<ImprovementModel> _models;
    protected List<ImprovementPresenter> _presenters;

    public Action<ImprovementModel> ImprovementActivateEvent;

    public void Initialize()
    {
        // DeleteKey(_key);
        SetData(ref _models, _key, CreateDefaultModels);
        CreateBoard();
    }

    protected abstract void CreateBoard();

    protected void InitializeImprovement(ImprovementPresenter improvement)
    {
        improvement.Initialize(_improvementData, _improvementSprites);
        improvement.ModelChangeEvent += () => Save(_key, _models);
        improvement.SubscribeToBuyButton(TryBuy);
        _presenters.Add(improvement);
    }

    private void TryBuy(ImprovementPresenter improvement)
    {
        if (_data.GameData.CanBuy(improvement.Model.Cost))
        {
            improvement.Activate();
            ImprovementActivateEvent?.Invoke(improvement.Model);
        }
    }

    private void CreateDefaultModels()
    {
        _models = new List<ImprovementModel>();

        for (var i = 0; i < _data.GameData.MaxLevel; i++)
        {
            var improvement = new ImprovementModel();
            improvement.SetDefaultData(_improvementData.DefaultModel);
            improvement.Level = i + 1;
            if (i == 0)
            {
                improvement.State = ImprovementState.Lock;
            }

            _models.Add(improvement);
        }

        Save(_key, _models);
    }

    public ImprovementPresenter FindImprovement(int level)
    {
        return _presenters.Find(manager => manager.Model.Level == level);
    }

    public void Unlock(int level, bool isCellActivated)
    {
        if (!isCellActivated)
        {
            FindImprovement(level)?.SetState(ImprovementState.Lock);
            return;
        }

        FindImprovement(level)?.SetState(ImprovementState.Unlock);
    }

    public void Dispose()
    {
        Save(_key, _models);
    }
}

public class ManagerController : ImprovementController
{
    protected override string _key { get; set; } = "Managers";

    protected override void CreateBoard()
    {
        _presenters = new List<ImprovementPresenter>();

        foreach (var model in _models)
        {
            var view = _view.Create(_board.GetManagerBoard());
            ImprovementPresenter manager = new ManagerPresenter(model, view);
            InitializeImprovement(manager);
        }
    }
}

public class UpgradeController : ImprovementController
{
    protected override string _key { get; set; } = "Upgrades";

    protected override void CreateBoard()
    {
        _presenters = new List<ImprovementPresenter>();

        foreach (var model in _models)
        {
            var view = _view.Create(_board.GetUpgradeBoard());
            ImprovementPresenter upgrade = new UpgradePresenter(model, view);
            InitializeImprovement(upgrade);
        }
    }
}
}
