using ShopTown.ViewComponent;
using VContainer;

namespace ShopTown.PresenterComponent
{
public class AudioSourcePresenter : IPlayable
{
    [Inject] private readonly IAudioSource _audio;

    public void PlayBackgroundMusic()
    {
        _audio.GetBackgroundMusic().Play();
    }

    public void PlayClickSound()
    {
        _audio.GetClickSound().Play();
    }

    public void PlayNewBusinessSound()
    {
        _audio.GetNewBusinessSound().Play();
    }

    public void PlayNewCoinSound()
    {
        _audio.GetNewCoinSound().Play();
    }

    public void PlaySwapSound()
    {
        _audio.GetSwapSound().Play();
    }
}
}
