using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Common;

public class WindowBase
{
    protected int m_iModuleID;
    protected int m_iInstanceID;
    protected GameObject m_objInstanceRoot;
    protected RectTransform m_transInstanceRoot;
    protected Ticker m_ticker;

    protected WindowBase()
    {

    }

    public void BaseInit(int moduleId, int instanceId, GameObject root)
    {
        m_iModuleID = moduleId;
        m_iInstanceID = instanceId;
        m_objInstanceRoot = root;
        m_transInstanceRoot = root.GetComponent<RectTransform>();
        m_ticker = m_objInstanceRoot.AddComponent<Ticker>();
    }

    public virtual void Init()
    {

    }

    public virtual void StartUp(object param = null)
    {

    }

    public virtual void StartListener()
    {

    }

    public virtual void RemoveListener()
    {

    }

    public virtual void Clear()
    {

    }

    protected void Close()
    {
        Dispatcher.Dispatch<int>(GameEvents.WindowCloseEvent.EVT_NAME, m_iInstanceID);
    }
}
