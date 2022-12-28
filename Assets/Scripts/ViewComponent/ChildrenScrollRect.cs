using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ShopTown.ViewComponent
{
public class ChildrenScrollRect : ScrollRect
{
    private List<IEventSystemHandler> _parentEventHandlers = new List<IEventSystemHandler>();
    private bool _shouldInvokeParentEvent = false;

    private void FindAllEventSystemHandlerOfParents()
    {
        if (_parentEventHandlers.Count == 0)
        {
            _parentEventHandlers = GetComponentsInParent<IEventSystemHandler>().ToList();
            _parentEventHandlers.Remove(transform.GetComponent<IEventSystemHandler>());
        }
    }

    private void InitializeEventForParents<T>(Action<T> action) where T : IEventSystemHandler
    {
        FindAllEventSystemHandlerOfParents();

        foreach (var eventHandler in _parentEventHandlers)
        {
            if (eventHandler is T)
            {
                action((T)eventHandler);
            }
        }
    }

    public override void OnInitializePotentialDrag(PointerEventData eventData)
    {
        InitializeEventForParents<IInitializePotentialDragHandler>((parent) =>
            parent.OnInitializePotentialDrag(eventData));

        base.OnInitializePotentialDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (_shouldInvokeParentEvent)
        {
            InitializeEventForParents<IDragHandler>((parent) => parent.OnDrag(eventData));
            return;
        }

        base.OnDrag(eventData);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        _shouldInvokeParentEvent = false;

        if (!horizontal && Math.Abs(eventData.delta.x) > Math.Abs(eventData.delta.y) ||
            !vertical && Math.Abs(eventData.delta.x) < Math.Abs(eventData.delta.y))
        {
            _shouldInvokeParentEvent = true;
        }

        if (_shouldInvokeParentEvent)
        {
            InitializeEventForParents<IBeginDragHandler>((parent) => parent.OnBeginDrag(eventData));
            return;
        }

        base.OnBeginDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (_shouldInvokeParentEvent)
        {
            InitializeEventForParents<IEndDragHandler>((parent) => parent.OnEndDrag(eventData));
            _shouldInvokeParentEvent = false;
            return;
        }

        base.OnEndDrag(eventData);
    }
}
}
