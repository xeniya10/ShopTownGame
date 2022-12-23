using NSubstitute;
using NUnit.Framework;
using ShopTown.Data;
using ShopTown.ModelComponent;
using ShopTown.PresenterComponent;
using ShopTown.SpriteContainer;
using ShopTown.ViewComponent;
using UnityEngine;

public class ManagerPresenterTester
{
    private ImprovementModel _model;
    private IImprovementView _view;
    private IImprovementData _improvementData;
    private IImprovementSprites _improvementSprites;
    private ManagerPresenter _presenter;

    [SetUp] public void CreateTestObjects()
    {
        _model = Substitute.For<ImprovementModel>();
        _view = Substitute.For<IImprovementView>();
        _improvementData = Substitute.For<IImprovementData>();
        _improvementSprites = Substitute.For<IImprovementSprites>();
        _presenter = new ManagerPresenter(_model, _view);

        TuneObjects();
    }

    private void TuneObjects()
    {
        _improvementData.GetBusinessName(Arg.Any<int>()).Returns(string.Empty);
        _improvementData.GetManagerCost(Arg.Any<int>()).Returns(new MoneyModel(Random.Range(1, 100000)));
        _improvementData.GetManagerName(Arg.Any<int>()).Returns(string.Empty);

        _improvementSprites.GetManagerSprites(Arg.Any<int>())
            .Returns(Sprite.Create(Texture2D.blackTexture, Rect.zero, Vector2.one));

        _presenter.Model.Level = 1;
    }

    [Test]
    public void Initialize__SetsName()
    {
        _presenter.Initialize(_improvementData, _improvementSprites);
        Assert.AreEqual(_presenter.Model.Name, _improvementData.GetManagerName(_presenter.Model.Level));
    }

    [Test]
    public void Initialize__SetsDescription()
    {
        _presenter.Initialize(_improvementData, _improvementSprites);
        Assert.True(_presenter.Model.Description.Contains(_improvementData.GetBusinessName(_presenter.Model.Level)));
    }

    [Test]
    public void Initialize__SetsCost()
    {
        _presenter.Initialize(_improvementData, _improvementSprites);
        Assert.AreEqual(_presenter.Model.Cost.Number, _improvementData.GetManagerCost(_presenter.Model.Level).Number);
    }

    [Test]
    public void SetState_ActivatedManager_SetsLock()
    {
        _presenter.Model.IsActivated = true;
        _presenter.SetState(ImprovementState.Unlock);
        Assert.AreEqual(_presenter.Model.State, ImprovementState.Lock);
    }

    [Test]
    public void SetState_NotActivatedManager_SetsUnlock()
    {
        _presenter.Model.IsActivated = false;
        _presenter.SetState(ImprovementState.Unlock);
        Assert.AreEqual(_presenter.Model.State, ImprovementState.Unlock);
    }

    [Test]
    public void SetState_NotActivatedManager_InvokesChangeEvent()
    {
        var wasCalled = false;
        _presenter.ChangeEvent += () => wasCalled = true;
        _presenter.SetState(ImprovementState.Unlock);
        Assert.True(wasCalled);
    }

    [Test]
    public void SetState_NotActivatedManager_StartsAnimation()
    {
        _presenter.SetState(ImprovementState.Unlock);
        _view.Received(1).StartAnimation(Arg.Any<ImprovementState>());
    }

    [Test]
    public void Activate_NotActivatedManager_ActivatesAnimation()
    {
        _presenter.Model.IsActivated = false;
        _presenter.Activate();
        _view.Received(1).ActivateAnimation();
    }

    [Test]
    public void Activate_NotActivatedManager_SetsTrue()
    {
        _presenter.Model.IsActivated = false;
        _presenter.Activate();
        Assert.True(_presenter.Model.IsActivated);
    }

    [Test]
    public void Activate_NotActivatedManager_SetsLock()
    {
        _presenter.Model.IsActivated = false;
        _presenter.Model.State = ImprovementState.Unlock;
        _presenter.Activate();
        Assert.AreEqual(_presenter.Model.State, ImprovementState.Lock);
    }
}
