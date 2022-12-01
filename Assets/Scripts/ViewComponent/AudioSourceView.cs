using UnityEngine;
using VContainer;

namespace ShopTown.ViewComponent
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

public interface IPlayable
{
    void PlayBackgroundMusic();

    void PlayClickSound();

    void PlayNewBusinessSound();

    void PlayNewCoinSound();

    void PlaySwapSound();
}

public class AudioSourceView : MonoBehaviour, IAudioSource
{
    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private AudioSource _clickSound;
    [SerializeField] private AudioSource _newBusinessSound;
    [SerializeField] private AudioSource _newCoinSound;
    [SerializeField] private AudioSource _swapSound;

    public AudioSource GetBackgroundMusic()
    {
        return _backgroundMusic;
    }

    public AudioSource GetClickSound()
    {
        return _clickSound;
    }

    public AudioSource GetNewBusinessSound()
    {
        return _newBusinessSound;
    }

    public AudioSource GetNewCoinSound()
    {
        return _newCoinSound;
    }

    public AudioSource GetSwapSound()
    {
        return _swapSound;
    }
}

public interface IAudioSource
{
    AudioSource GetBackgroundMusic();

    AudioSource GetClickSound();

    AudioSource GetNewBusinessSound();

    AudioSource GetNewCoinSound();

    AudioSource GetSwapSound();
}
}
