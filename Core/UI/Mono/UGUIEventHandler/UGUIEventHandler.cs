using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public enum UGUIEventType
{
    BEGIN_DRAG,
    CANCEL,
    DESELECT,
    DRAG,
    DROP,
    END_DRAG,
    INIT_POTENTIAL_DRAG,
    MOVE,
    POINTER_CLICK,
    POINTER_DOWN,
    POINTER_UP,
    POINTER_ENTER,
    POINTER_EXIT,
    SCROLL,
    SELECT,
    SUBMIT,
    UPDATE_SELECTED
}

public partial class UGUIEventHandler : MonoBehaviour
{
    public delegate void EVENT_CALLBACK<T>(T eventData);

    static public void AddListener(GameObject obj, UGUIEventType evtType, EVENT_CALLBACK<BaseEventData> func)
    {
        AddListenerAsDelegate(obj, evtType, func);
    }

    static public void AddListener(GameObject obj, UGUIEventType evtType, EVENT_CALLBACK<PointerEventData> func)
    {
        AddListenerAsDelegate(obj, evtType, func);
    }

    static public void AddListener(GameObject obj, UGUIEventType evtType, EVENT_CALLBACK<AxisEventData> func)
    {
        AddListenerAsDelegate(obj, evtType, func);
    }

    static public void AddListenerAsDelegate(GameObject obj, UGUIEventType evtType, Delegate func)
    {
        UGUIEventHandler handler = obj.GetComponent<UGUIEventHandler>();
        if (handler == null)
        {
            handler = obj.AddComponent<UGUIEventHandler>();
        }
        EVENT_CALLBACK<BaseEventData> bHandleFunc;
        EVENT_CALLBACK<PointerEventData> pHandleFunc;
        EVENT_CALLBACK<AxisEventData> aHandleFunc;
        switch(evtType)
        {
            case UGUIEventType.BEGIN_DRAG:
                pHandleFunc = func as EVENT_CALLBACK<PointerEventData>;
                if (pHandleFunc == null)
                {
                    throw new Exception("Event BEGIN_DRAG Need a EVENT_CALLBACK<PointerEventData> instance as callback function");
                }
                handler.FuncOnBeginDrag += pHandleFunc;
                break;
            case UGUIEventType.CANCEL:
                bHandleFunc = func as EVENT_CALLBACK<BaseEventData>;
                if (bHandleFunc == null)
                {
                    throw new Exception("Event CANCEL Need a EVENT_CALLBACK<BaseEventData> instance as callback function");
                }
                handler.FuncOnCancel += bHandleFunc;
                break;
            case UGUIEventType.DESELECT:
                bHandleFunc = func as EVENT_CALLBACK<BaseEventData>;
                if (bHandleFunc == null)
                {
                    throw new Exception("Event DESELECT Need a EVENT_CALLBACK<BaseEventData> instance as callback function");
                }
                handler.FuncOnDeselect += bHandleFunc;
                break;
            case UGUIEventType.DRAG:
                pHandleFunc = func as EVENT_CALLBACK<PointerEventData>;
                if (pHandleFunc == null)
                {
                    throw new Exception("Event DRAG Need a EVENT_CALLBACK<PointerEventData> instance as callback function");
                }
                handler.FuncOnDrag += pHandleFunc;
                break;
            case UGUIEventType.DROP:
                pHandleFunc = func as EVENT_CALLBACK<PointerEventData>;
                if (pHandleFunc == null)
                {
                    throw new Exception("Event DROP Need a EVENT_CALLBACK<PointerEventData> instance as callback function");
                }
                handler.FuncOnDrop += pHandleFunc;
                break;
            case UGUIEventType.END_DRAG:
                pHandleFunc = func as EVENT_CALLBACK<PointerEventData>;
                if (pHandleFunc == null)
                {
                    throw new Exception("Event END_DRAG Need a EVENT_CALLBACK<PointerEventData> instance as callback function");
                }
                handler.FuncOnEndDrag += pHandleFunc;
                break;
            case UGUIEventType.INIT_POTENTIAL_DRAG:
                pHandleFunc = func as EVENT_CALLBACK<PointerEventData>;
                if (pHandleFunc == null)
                {
                    throw new Exception("Event INIT_POTENTIAL_DRAG Need a EVENT_CALLBACK<PointerEventData> instance as callback function");
                }
                handler.FuncOnInitPotentialDrag += pHandleFunc;
                break;
            case UGUIEventType.MOVE:
                aHandleFunc = func as EVENT_CALLBACK<AxisEventData>;
                if (aHandleFunc == null)
                {
                    throw new Exception("Event MOVE Need a EVENT_CALLBACK<AxisEventData> instance as callback function");
                }
                handler.FuncOnMove += aHandleFunc;
                break;
            case UGUIEventType.POINTER_CLICK:
                pHandleFunc = func as EVENT_CALLBACK<PointerEventData>;
                if (pHandleFunc == null)
                {
                    throw new Exception("Event POINTER_CLICK Need a EVENT_CALLBACK<PointerEventData> instance as callback function");
                }
                handler.FuncOnPointerClick += pHandleFunc;
                break;
            case UGUIEventType.POINTER_DOWN:
                pHandleFunc = func as EVENT_CALLBACK<PointerEventData>;
                if (pHandleFunc == null)
                {
                    throw new Exception("Event POINTER_DOWN Need a EVENT_CALLBACK<PointerEventData> instance as callback function");
                }
                handler.FuncOnPointerDown += pHandleFunc;
                break;
            case UGUIEventType.POINTER_ENTER:
                pHandleFunc = func as EVENT_CALLBACK<PointerEventData>;
                if (pHandleFunc == null)
                {
                    throw new Exception("Event POINTER_ENTER Need a EVENT_CALLBACK<PointerEventData> instance as callback function");
                }
                handler.FuncOnPointerEnter += pHandleFunc;
                break;
            case UGUIEventType.POINTER_EXIT:
                pHandleFunc = func as EVENT_CALLBACK<PointerEventData>;
                if (pHandleFunc == null)
                {
                    throw new Exception("Event POINTER_EXIT Need a EVENT_CALLBACK<PointerEventData> instance as callback function");
                }
                handler.FuncOnPointerExit += pHandleFunc;
                break;
            case UGUIEventType.POINTER_UP:
                pHandleFunc = func as EVENT_CALLBACK<PointerEventData>;
                if (pHandleFunc == null)
                {
                    throw new Exception("Event POINTER_UP Need a EVENT_CALLBACK<PointerEventData> instance as callback function");
                }
                handler.FuncOnPointerUp += pHandleFunc;
                break;
            case UGUIEventType.SCROLL:
                pHandleFunc = func as EVENT_CALLBACK<PointerEventData>;
                if (pHandleFunc == null)
                {
                    throw new Exception("Event SCROLL Need a EVENT_CALLBACK<PointerEventData> instance as callback function");
                }
                handler.FuncOnScroll += pHandleFunc;
                break;
            case UGUIEventType.SELECT:
                bHandleFunc = func as EVENT_CALLBACK<BaseEventData>;
                if (bHandleFunc == null)
                {
                    throw new Exception("Event SELECT Need a EVENT_CALLBACK<BaseEventData> instance as callback function");
                }
                handler.FuncOnSelect += bHandleFunc;
                break;
            case UGUIEventType.SUBMIT:
                bHandleFunc = func as EVENT_CALLBACK<BaseEventData>;
                if (bHandleFunc == null)
                {
                    throw new Exception("Event SUBMIT Need a EVENT_CALLBACK<BaseEventData> instance as callback function");
                }
                handler.FuncOnSubmit += bHandleFunc;
                break;
            case UGUIEventType.UPDATE_SELECTED:
                bHandleFunc = func as EVENT_CALLBACK<BaseEventData>;
                if (bHandleFunc == null)
                {
                    throw new Exception("Event UPDATE_SELECTED Need a EVENT_CALLBACK<BaseEventData> instance as callback function");
                }
                handler.FuncOnUpdateSelected += bHandleFunc;
                break;
            default:
                throw new Exception("Wrong Event Type!");
        }
    }
}
