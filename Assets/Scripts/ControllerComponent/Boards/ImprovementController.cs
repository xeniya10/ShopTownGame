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
public abstract class ImprovementController<T> : IImprovementController<T>, IDisposable
{
    [Inject] protected readonly IGameData _data;
    [Inject] protected readonly IStorageManager _storage;
    [Inject] protected readonly IImprovementView _view;
    [Inject] protected readonly IBoard _board;
    [Inject] protected readonly ImprovementData _improvementData;
    [Inject] protected readonly ImprovementContainer _improvementSprites;

    protected abstract string _key { get; set; }

    protected List<ImprovementModel> _models;
    protected List<ImprovementPresenter> _presenters;

    public event Action<ImprovementModel> ActivateEvent;

    public void Initialize()
    {
        _storage.DeleteKey(_key);
        _storage.SetData(ref _models, _key, CreateDefaultModels);
        CreateBoard();
    }

    protected abstract void CreateBoard();

    protected void InitializeImprovement(ImprovementPresenter improvement)
    {
        improvement.Initialize(_improvementData, _improvementSprites);
        improvement.ChangeEvent += () => _storage.Save(_key, _models);
        improvement.SubscribeToBuyButton(TryBuy);
        _presenters.Add(improvement);
    }

    private void TryBuy(ImprovementPresenter improvement)
    {
        if (_data.GameData.CanBuy(improvement.Model.Cost))
        {
            improvement.Activate();
            ActivateEvent?.Invoke(improvement.Model);
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

        _storage.Save(_key, _models);
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
        _storage.Save(_key, _models);
    }
}
}
