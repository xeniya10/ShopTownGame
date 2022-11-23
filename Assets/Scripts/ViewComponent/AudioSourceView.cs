using UnityEngine;

namespace ShopTown.ViewComponent
{
public class AudioSourceView : MonoBehaviour
{
    public AudioSource BackgroundMusic;
    public AudioSource ClickSound;
    public AudioSource NewBusinessSound;
    public AudioSource NewCoinSound;
    public AudioSource SwapSound;

    public void Play(AudioSource audioSource)
    {
        audioSource.Play();
    }
}
}
