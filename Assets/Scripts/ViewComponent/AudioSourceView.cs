using UnityEngine;

public class AudioSourceView : MonoBehaviour
{
    public AudioSource BackgroundMusic;
    public AudioSource ClickSound;
    public AudioSource NewBusinessSound;
    public AudioSource NewCoinSound;
    public AudioSource SwapSound;

    public void PlayBackgroundMusic()
    {
        BackgroundMusic.Play();
    }
    public void PlayClickSound()
    {
        ClickSound.Play();
    }
    public void PlayNewBusinessSound()
    {
        NewBusinessSound.Play();
    }
    public void PlayNewCoinSound()
    {
        NewCoinSound.Play();
    }
    public void PlaySwapSound()
    {
        SwapSound.Play();
    }
}
