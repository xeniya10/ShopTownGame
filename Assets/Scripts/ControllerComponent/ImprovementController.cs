using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using ShopTown.SpriteContainer;
using ShopTown.ViewComponent;
using VContainer;

namespace ShopTown.ControllerComponent
{
public abstract class ImprovementController : StorageManager
{
    private GameDataModel _data;
    [Inject] protected readonly ImprovementView _view;
    [Inject] protected readonly GameScreenView _gameScreen;
    [Inject] protected readonly BusinessData _business;
    [Inject] protected readonly ImprovementData _improvementData;
    [Inject] protected readonly ImprovementCollection _improvementSprites;

    protected abstract string _key { get; set; }

    protected List<ImprovementModel> _models;
    protected List<ImprovementPresenter> _improvements;

    public Action<ImprovementModel> ImprovementActivateEvent;

    public void Initialize(ref GameDataModel data)
    {
        // PlayerPrefs.DeleteKey(_key);
        _data = data;
        _models = JsonConvert.DeserializeObject<List<ImprovementModel>>(Load(_key));

        if (_models == null)
        {
            CreateDefaultModels();
        }

        _improvements = new List<ImprovementPresenter>();
        CreateImprovements();
    }

    protected abstract void CreateImprovements();

    protected void TryBuy(ImprovementPresenter improvement)
    {
        if (_data.CanBuy(improvement.Model.Cost))
        {
            improvement.Activate();
            ImprovementActivateEvent?.Invoke(improvement.Model);
        }
    }

    private void CreateDefaultModels()
    {
        _models = new List<ImprovementModel>();

        for (var i = 0; i < _data.MaxLevel; i++)
        {
            var improvement = new ImprovementModel();
            improvement.Initialize(i + 1);
            _models.Add(improvement);
            if (i == 0)
            {
                improvement.SetState(ImprovementState.Lock);
            }
        }

        SaveData();
    }

    public ImprovementPresenter FindImprovement(int level)
    {
        return _improvements.Find(manager => manager.Model.Level == level);
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

    public void SaveData()
    {
        Save(_key, _models);
    }
}

public class ManagerController : ImprovementController
{
    protected override string _key { get; set; } = "Managers";

    protected override void CreateImprovements()
    {
        foreach (var model in _models)
        {
            var view = _view.Create(_gameScreen.ManagerBoard);
            ImprovementPresenter manager =
                new ManagerPresenter(model, view, _business, _improvementData, _improvementSprites);

            manager.Initialize();
            manager.ModelChangeEvent += () => Save(_key, _models);
            manager.SubscribeToBuyButton(TryBuy);
            _improvements.Add(manager);
        }
    }
}

public class UpgradeController : ImprovementController
{
    protected override string _key { get; set; } = "Upgrades";

    protected override void CreateImprovements()
    {
        foreach (var model in _models)
        {
            var view = _view.Create(_gameScreen.UpgradeBoard);
            ImprovementPresenter upgrade =
                new UpgradePresenter(model, view, _business, _improvementData, _improvementSprites);

            upgrade.Initialize();
            upgrade.ModelChangeEvent += () => Save(_key, _models);
            upgrade.SubscribeToBuyButton(TryBuy);
            _improvements.Add(upgrade);
        }
    }
}
}
