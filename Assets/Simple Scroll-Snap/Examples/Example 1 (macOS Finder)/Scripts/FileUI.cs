// Simple Scroll-Snap - https://assetstore.unity.com/packages/tools/gui/simple-scroll-snap-140884
// Copyright (c) Daniel Lochner

using UnityEngine;
using UnityEngine.UI;

namespace Simple_Scroll_Snap.Examples.Example_1__macOS_Finder_.Scripts
{
public class FileUI : MonoBehaviour
{
    #region Methods

    private void Awake()
    {
        toggle.onValueChanged.AddListener(delegate(bool isOn)
        {
            nameText.color = dateModifiedText.color = sizeText.color = kindText.color = isOn ? Color.white : Color.black;
        });
    }

    #endregion
    #region Fields

    [SerializeField] private Toggle toggle;
    [Space]
    [SerializeField] private Text nameText;
    [SerializeField] private Text dateModifiedText;
    [SerializeField] private Text sizeText;
    [SerializeField] private Text kindText;

    #endregion
}
}
