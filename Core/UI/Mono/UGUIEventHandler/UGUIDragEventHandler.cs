using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public partial class UGUIEventHandler :
    IBeginDragHandler,
    ICancelHandler,
    IDeselectHandler,
    IDragHandler,
    IDropHandler,
    IEndDragHandler,
    IMoveHandler
{
    public EVENT_CALLBACK<PointerEventData> FuncOnBeginDrag = null;
    public EVENT_CALLBACK<BaseEventData> FuncOnCancel = null;
    public EVENT_CALLBACK<BaseEventData> FuncOnDeselect = null;
    public EVENT_CALLBACK<PointerEventData> FuncOnDrag = null;
    public EVENT_CALLBACK<PointerEventData> FuncOnDrop = null;
    public EVENT_CALLBACK<PointerEventData> FuncOnEndDrag = null;
    public EVENT_CALLBACK<AxisEventData> FuncOnMove = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (FuncOnBeginDrag != null)
            FuncOnBeginDrag.Invoke(eventData);
    }

    public void OnCancel(BaseEventData eventData)
    {
        if (FuncOnCancel != null)
            FuncOnCancel.Invoke(eventData);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (FuncOnDeselect != null)
            FuncOnDeselect.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (FuncOnDrag != null)
            FuncOnDrag.Invoke(eventData);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (FuncOnDrop != null)
            FuncOnDrop.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (FuncOnEndDrag != null)
            FuncOnEndDrag.Invoke(eventData);
    }
    public void OnMove(AxisEventData eventData)
    {
        if (FuncOnMove != null)
            FuncOnMove.Invoke(eventData);
    }
}
