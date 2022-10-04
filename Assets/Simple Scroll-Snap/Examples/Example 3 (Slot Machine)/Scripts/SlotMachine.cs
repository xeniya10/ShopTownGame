// Simple Scroll-Snap - https://assetstore.unity.com/packages/tools/gui/simple-scroll-snap-140884
// Copyright (c) Daniel Lochner

using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine;

namespace Simple_Scroll_Snap.Examples.Example_3__Slot_Machine_.Scripts
{
public class SlotMachine : MonoBehaviour
{
    #region Fields

    [SerializeField] private SimpleScrollSnap[] slots;

    #endregion

    #region Methods

    public void Spin()
    {
        foreach (var slot in slots)
            slot.Velocity += Random.Range(2500, 5000) * Vector2.up;
    }

    #endregion
}
}
