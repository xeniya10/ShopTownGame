using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public interface ISplashScreenView : ISplashField, IStartButton, ISplashAnimation
{}

public interface IStartButton
{
    Button GetStartButton();
}

public interface ISplashField
{
    Transform GetSplashField();
}
}
