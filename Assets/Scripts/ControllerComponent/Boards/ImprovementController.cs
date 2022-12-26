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
public abstract class ImprovementController<T> : IImprovementController<T>
{
    [Inject] protected readonly IGameData _data;
    [Inject] protected readonly IStorageManager _storage;
    [Inject] protected readonly IImprovementView _view;
    [Inject] protected readonly IButtonSubscriber _subscriber;
    [Inject] protected readonly IBoard _board;
    [Inject] protected readonly IImprovementData _improvementData;
    [Inject] protected readonly IImprovementSprites _improvementSprites;

    protected abstract string _key { get; set; }

    protected List<ImprovementModel> _models;
    protected List<IImprovement> _presenters;

    public event Action<ImprovementModel> ImprovementActivationEvent;

    public void Initialize()
    {
        _models = _storage.Load<List<ImprovementModel>>(_key);
        CreateBoard();
    }

    protected abstract void CreateBoard();

    protected void InitializeImprovement(IImprovement improvement)
    {
        improvement.Initialize(_improvementData, _improvementSprites);
        improvement.ChangeEvent += () => _storage.Save(_key, _models);
        improvement.AddListenerToBuyButton(_subscriber, TryBuy);
        _presenters.Add(improvement);
    }

    private void TryBuy(IImprovement improvement)
    {
        if (_data.GameData.CanBuy(improvement.Model.Cost))
        {
            improvement.Activate(_data);
            ImprovementActivationEvent?.Invoke(improvement.Model);
        }
    }

    protected void CreateDefaultModels()
    {
        _models = new List<ImprovementModel>();

        for (var i = 0; i < _data.GameData.MaxLevel; i++)
        {
            var improvement = new ImprovementModel();
            improvement.SetDefaultData(_improvementData.GetDefaultModel());
            improvement.Level = i + 1;
            if (i == 0)
            {
                improvement.State = ImprovementState.Lock;
            }

            _models.Add(improvement);
        }

        _storage.Save(_key, _models);
    }

    public IImprovement FindImprovement(int level)
    {
        return _presenters.Find(manager => manager.Model.Level == level);
    }

    public void UnlockImprovement(int level, bool isCellActivated)
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
