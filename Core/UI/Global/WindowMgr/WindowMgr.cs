using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using GameEvents;
using UnityEngine;

public partial class WindowMgr : Singleton<WindowMgr>
{
    public struct WinTemplate
    {
        public int RefCnt;
        public UnityEngine.Object Template;
    }

    private static readonly int NONUNIQE_WINDOW_TEMPLATE_CACHE_SIZE = 16;
    private static readonly int UNIQE_WINDOW_CACHE_SIZE = 16;

    private HashSet<int> m_hsUsedInsID = new HashSet<int>();
    // <instanceId, IWindowBase>
    private Dictionary<int, IWindow> m_dictInstanceMapper = new Dictionary<int, IWindow>();
    private LinkedList<IWindow> m_llInstances = new LinkedList<IWindow>();
    // <moduleId, WinTemplate>
    private Dictionary<int, WinTemplate> m_dictTemplateMapper = new Dictionary<int, WinTemplate>();
    private LinkedList<WinTemplate> m_llWinTemplate = new LinkedList<WinTemplate>();


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
        IWindow tmpIns;
        // if key moduleId exist in m_dictInstanceMapper,
        // shows that this window is a uniqe window,
        // and it's in the cache;
        if (m_dictInstanceMapper.TryGetValue(instanceId, out tmpIns))
        {
            LinkedListNode<IWindow> node = m_llInstances.Find(tmpIns);
            m_llInstances.Remove(node);
            m_llInstances.AddFirst(node);

            tmpIns.StartUp(param.Params);
            tmpIns.StartListener();
            return;
        }

        // not in the cache, but it's not a uniqe window
        tmpIns = GetWinInstance(moduleId);
        if (!tmpIns.IsUniqeWindow())
        {
            WinTemplate template;
            if (m_dictTemplateMapper.TryGetValue(moduleId, out template))
            {
                GameObject root = GameObject.Instantiate(template.Template) as GameObject;
                template.RefCnt += 1;
                LinkedListNode<WinTemplate> node = m_llWinTemplate.Find(template);
                m_llWinTemplate.Remove(node);
                m_llWinTemplate.AddFirst(node);

                m_dictTemplateMapper[moduleId] = template;
                instanceId = GetInstanceID(moduleId);
                tmpIns.BaseInit(moduleId, instanceId, root);
                tmpIns.Init();
                tmpIns.StartUp(param.Params);
                tmpIns.StartListener();
                return;
            }
        }

        IntermediateParams.WinLoadedParam winParam = new IntermediateParams.WinLoadedParam();
        winParam.ModuleID = moduleId;
        winParam.InstanceID = instanceId;
        winParam.Instance = tmpIns;
        string path = Path.GetUIPath(moduleId);
        ResLoader.Load(path, OnWindowResLoaded, winParam);
    }

    private void OnWindowResLoaded(UnityEngine.Object res, object param)
    {
        IntermediateParams.WinLoadedParam interParam = param as IntermediateParams.WinLoadedParam;
        IWindow instance = interParam.Instance;
    }

    private void OnWindowClose(int instanceId)
    {
        
    }
}

