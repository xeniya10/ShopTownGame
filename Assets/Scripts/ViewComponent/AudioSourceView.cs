using UnityEngine;

public class AudioSourceView : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private AudioSource _clickSound;
    [SerializeField] private AudioSource _newBusinessSound;
    [SerializeField] private AudioSource _newCoinSound;
    [SerializeField] private AudioSource _swapSound;

    public void PlayBackgroundMusic()
    {
        _backgroundMusic.Play();
    }
    public void PlayClickSound()
    {
        _clickSound.Play();
    }
    public void PlayNewBusinessSound()
    {
        _newBusinessSound.Play();
    }
    public void PlayNewCoinSound()
    {
        _newCoinSound.Play();
    }
    public void PlaySwapSound()
    {
        _swapSound.Play();
    }
}