using UnityEngine;

namespace ShopTown.ViewComponent
{
public interface IAudioSource
{
    AudioSource GetBackgroundMusic();

    AudioSource GetClickSound();

    AudioSource GetNewBusinessSound();

    AudioSource GetNewCoinSound();

    AudioSource GetSwapSound();
}
}
