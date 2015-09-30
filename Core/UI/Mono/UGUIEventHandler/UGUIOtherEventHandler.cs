using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public partial class UGUIEventHandler :
    IEventSystemHandler,
    IInitializePotentialDragHandler,
    IScrollHandler,
    ISelectHandler,
    ISubmitHandler,
    IUpdateSelectedHandler
{
    public EVENT_CALLBACK<PointerEventData> FuncOnInitPotentialDrag = null;
    public EVENT_CALLBACK<PointerEventData> FuncOnScroll = null;
    public EVENT_CALLBACK<BaseEventData> FuncOnSelect = null;
    public EVENT_CALLBACK<BaseEventData> FuncOnSubmit = null;
    public EVENT_CALLBACK<BaseEventData> FuncOnUpdateSelected = null;

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        if (FuncOnInitPotentialDrag != null)
            FuncOnInitPotentialDrag.Invoke(eventData);
    }

    public void OnScroll(PointerEventData eventData)
    {
        if (FuncOnScroll != null)
            FuncOnScroll.Invoke(eventData);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (FuncOnSelect != null)
            FuncOnSelect.Invoke(eventData);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        if (FuncOnSubmit != null)
            FuncOnSubmit.Invoke(eventData);
    }

    public void OnUpdateSelected(BaseEventData eventData)
    {
        if (FuncOnUpdateSelected != null)
            FuncOnUpdateSelected.Invoke(eventData);
    }

}

