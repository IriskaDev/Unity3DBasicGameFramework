using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using GameEvents;

public partial class WindowMgr : Singleton<WindowMgr>
{
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
        UnityEngine.Debug.Log("OOOOOOOOOOOOpen");
    }

    private void OnWindowClose(int moduleId)
    {
        UnityEngine.Debug.Log("CCCCCCCCCCClose");
    }
}

