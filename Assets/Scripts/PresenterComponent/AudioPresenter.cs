using ShopTown.ControllerComponent;
using ShopTown.ViewComponent;
using VContainer;
using VContainer.Unity;

namespace ShopTown.PresenterComponent
{
public class AudioPresenter : IInitializable, IPlayable
{
    [Inject] private readonly IGameData _data;
    [Inject] private readonly IAudioSource _audio;

    public void Initialize()
    {
        PlayBackgroundMusic();
        _data.GameData.Settings.ChangeEvent += PlayBackgroundMusic;
    }

    public void PlayBackgroundMusic()
    {
        if (_audio.GetBackgroundMusic().isPlaying && !_data.GameData.Settings.MusicOn)
        {
            _audio.GetBackgroundMusic().Stop();
        }

        if (!_audio.GetBackgroundMusic().isPlaying && _data.GameData.Settings.MusicOn)
        {
            _audio.GetBackgroundMusic().Play();
        }
    }

    public void PlayClickSound()
    {
        if (_data.GameData.Settings.SoundOn)
        {
            _audio.GetClickSound().Play();
        }
    }

    public void PlayNewBusinessSound()
    {
        if (_data.GameData.Settings.SoundOn)
        {
            _audio.GetNewBusinessSound().Play();
        }
    }

    public void PlayNewCoinSound()
    {
        if (_data.GameData.Settings.SoundOn)
        {
            _audio.GetNewCoinSound().Play();
        }
    }

    public void PlaySwapSound()
    {
        if (_data.GameData.Settings.SoundOn)
        {
            _audio.GetSwapSound().Play();
        }
    }
}
}
