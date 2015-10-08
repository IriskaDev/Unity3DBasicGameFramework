using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using GameEvents;
using UnityEngine;

public partial class WindowMgr : Singleton<WindowMgr>
{
    private HashSet<int> m_hsUsedInsID = new HashSet<int>();
    private Dictionary<int, WindowBase> m_dictInstanceMapper = new Dictionary<int, WindowBase>();
    private LinkedList<WindowBase> m_llInstances = new LinkedList<WindowBase>();

    public WindowMgr()
    {
        Dispatcher.AddListener<WindowStartUpEvent>(WindowStartUpEvent.EVT_NAME, OnWindowOpen);
        Dispatcher.AddListener<int>(WindowStartUpEvent.EVT_NAME, OnWindowOpen);
        Dispatcher.AddListener<int>(WindowCloseEvent.EVT_NAME, OnWindowClose);
    }

    private void OnWindowOpen(int moduleId)
    {
        WindowStartUpEvent evt = new WindowStartUpEvent();
        evt.ModuleID = moduleId;
        OnWindowOpen(evt);
    }

    private void OnWindowOpen(WindowStartUpEvent param)
    {
        int moduleId = param.ModuleID;
        int instanceId = moduleId;
        if (param.CreateNewIns)
        {
            instanceId = GetInstanceID(moduleId);
        }
    }

    private void OnWindowClose(int instanceId)
    {
        
    }
}

