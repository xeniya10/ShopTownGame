using UnityEngine;
using VContainer;
using VContainer.Unity;
using UnityEngine.UI;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameObject _mainMenuBaseObject;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterController(builder);
        RegisterModel(builder);
        RegisterPresenter(builder);
        RegisterView(builder);
    }

    private void RegisterController(IContainerBuilder builder)
    { }

    private void RegisterModel(IContainerBuilder builder)
    { }

    private void RegisterPresenter(IContainerBuilder builder)
    { }

    private void RegisterView(IContainerBuilder builder)
    { }
}
