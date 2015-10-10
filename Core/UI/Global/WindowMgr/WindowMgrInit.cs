using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Rendering;
using Common;

public partial class WindowMgr
{
    private GameObject m_objRootCanvas;
    private GameObject m_objUICam;
    private GameObject m_objEventSystem;
    private GameObject m_objWindowRoot;

    private RectTransform m_transRootCanvas;
    private RectTransform m_transWindowRoot;

    private Camera m_camUICam;

    private Canvas m_compRootCanvas;
    private CanvasScaler m_compRootCanvasScaler;
    private GraphicRaycaster m_compRootGraphicRaycaster;

    private EventSystem m_compEventSystem;
    private StandaloneInputModule m_compSIModule;
    private TouchInputModule m_compTIModule;

    private Ticker m_compTicker;

    public void Init()
    {
        CreateRootCanvas();
        CreateUICam();
        CreateEventSystem();
        CreateWindowRoot();
        WindowInfoMgr.Init();
    }

    private void CreateRootCanvas()
    {
        m_objRootCanvas = new GameObject();
        m_objRootCanvas.name = "RootCanvas";
        m_objRootCanvas.layer = GameLayer.UI;
        m_transRootCanvas = m_objRootCanvas.AddComponent<RectTransform>();
        m_compRootCanvas = m_objRootCanvas.AddComponent<Canvas>();
        m_compRootCanvasScaler = m_objRootCanvas.AddComponent<CanvasScaler>();
        m_compRootGraphicRaycaster = m_objRootCanvas.AddComponent<GraphicRaycaster>();
        m_compTicker = m_objRootCanvas.AddComponent<Ticker>();
        m_compTicker.onUpdate += OnTryOpenBufferedWindow;
        GameObject.DontDestroyOnLoad(m_objRootCanvas);

        m_compRootCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        m_compRootCanvasScaler.referenceResolution = new Vector2(1920, 1080);
    }

    private void CreateUICam()
    {
        m_objUICam = new GameObject();
        m_objUICam.name = "UICamera";
        m_objUICam.layer = GameLayer.UI;
        m_objUICam.transform.SetParent(m_transRootCanvas, false);
        m_camUICam = m_objUICam.AddComponent<Camera>();
        m_camUICam.orthographic = true;
        m_compRootCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        m_compRootCanvas.worldCamera = m_camUICam;
        m_objUICam.transform.localPosition = new Vector3(0.0f, 0.0f, -150.0f);
        m_camUICam.farClipPlane = 300.0f;
        m_camUICam.cullingMask = 1 << GameLayer.UI;
        m_camUICam.clearFlags = CameraClearFlags.Depth;
        RenderingUnit unit = new RenderingUnit("UINode", m_camUICam);
        RenderingMgr.Instance.AddCrucialUnitAtLast("UINode", unit);
    }

    private void CreateEventSystem()
    {
        m_objEventSystem = new GameObject();
        m_objEventSystem.name = "EventSystem";
        m_objEventSystem.layer = GameLayer.UI;
        m_objEventSystem.transform.SetParent(m_transRootCanvas, false);
        m_compEventSystem = m_objEventSystem.AddComponent<EventSystem>();
        m_compSIModule = m_objEventSystem.AddComponent<StandaloneInputModule>();
        m_compTIModule = m_objEventSystem.AddComponent<TouchInputModule>();
    }

    private void CreateWindowRoot()
    {
        m_objWindowRoot = new GameObject();
        m_objWindowRoot.name = "WindowRoot";
        m_objWindowRoot.layer = GameLayer.UI;
        m_transWindowRoot = m_objWindowRoot.AddComponent<RectTransform>();
        m_transWindowRoot.SetParent(m_transRootCanvas, false);
        UIUtils.SetRectAsFullScreen(ref m_transWindowRoot);
    }
}
