using System;
using UnityEngine.UI;

namespace ShopTown.PresenterComponent
{
public interface IButtonSubscriber
{
    void AddListenerToButton(Button button, Action callBack);
}
}
