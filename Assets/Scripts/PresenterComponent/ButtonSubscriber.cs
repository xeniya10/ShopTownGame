using System;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace ShopTown.PresenterComponent
{
public class ButtonSubscriber : IButtonSubscriber
{
    [Inject] private readonly IPlayable _audio;

    public void AddListenerToButton(Button button, Action callBack)
    {
        if (button == null)
        {
            Debug.Log("Subscriber didn't receive button");
            return;
        }

        button.onClick.AddListener(() =>
        {
            _audio.PlayClickSound();
            callBack?.Invoke();
        });
    }
}
}
