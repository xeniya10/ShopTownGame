using UnityEngine;

namespace ShopTown.ViewComponent
{
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
}
