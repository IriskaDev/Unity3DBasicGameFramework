using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public partial class UGUIEventHandler : 
    IPointerClickHandler, 
    IPointerDownHandler, 
    IPointerUpHandler, 
    IPointerEnterHandler, 
    IPointerExitHandler
{
    public EVENT_CALLBACK<PointerEventData> FuncOnPointerClick = null;
    public EVENT_CALLBACK<PointerEventData> FuncOnPointerDown = null;
    public EVENT_CALLBACK<PointerEventData> FuncOnPointerUp = null;
    public EVENT_CALLBACK<PointerEventData> FuncOnPointerEnter = null;
    public EVENT_CALLBACK<PointerEventData> FuncOnPointerExit = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (FuncOnPointerClick != null)
            FuncOnPointerClick.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (FuncOnPointerDown != null)
            FuncOnPointerDown.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (FuncOnPointerUp != null)
            FuncOnPointerUp.Invoke(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (FuncOnPointerEnter != null)
            FuncOnPointerEnter.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (FuncOnPointerExit != null)
            FuncOnPointerExit.Invoke(eventData);
    }

}
