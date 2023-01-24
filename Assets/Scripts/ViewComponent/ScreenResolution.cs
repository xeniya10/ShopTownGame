using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(1920, 1080, false);
    }
}
