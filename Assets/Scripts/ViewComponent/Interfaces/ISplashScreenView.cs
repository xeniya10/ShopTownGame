using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public interface ISplashScreenView : ISplashField, IStartButton, ISplashAnimation
{
    void DisappearAnimationImage(Sequence sequence);
}

public interface IStartButton
{
    Button GetStartButton();
}

public interface ISplashField
{
    Transform GetSplashField();
}
}
