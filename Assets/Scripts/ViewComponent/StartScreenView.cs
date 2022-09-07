using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartScreenView : MonoBehaviour
{
    [Header("Components")]
    public GameObject StartBoardField;
    public Button StartButton;
    public TextMeshProUGUI GameNameText;

    [Header("Animation Durations")]
    public float FadeTime;
    public float MoveTime;
}
