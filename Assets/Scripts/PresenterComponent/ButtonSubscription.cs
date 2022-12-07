using System;
using UnityEngine.UI;

namespace ShopTown.PresenterComponent
{
public abstract class ButtonSubscription
{
    protected void SubscribeToButton(Button button, Action callBack)
    {
        button.onClick.AddListener(() => callBack?.Invoke());
    }
}
}
