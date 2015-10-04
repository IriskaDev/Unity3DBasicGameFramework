using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Rendering
{
    public partial class RenderingMgr
    {
        public void Init()
        {
            CreateRenderScreen();
            ResetResolution();
        }

        private void CreateRenderScreen()
        {
            CreateScreenRoot();
        }

        private void CreateScreenRoot()
        {
            m_csScreen.ScreenRoot = new GameObject();
            m_csScreen.ScreenRoot.name = "ScreenRoot";
            m_csScreen.ScreenRoot.layer = GameLayer.Screen;

            GameObject.DontDestroyOnLoad(m_csScreen.ScreenRoot);

            CreateScreenQuad();
            CreateProcessCam();
            CreateOutputCam();
        }

        private void CreateScreenQuad()
        {
            m_csScreen.ScreenQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            m_csScreen.ScreenQuad.layer = GameLayer.Screen;
            MeshCollider collider = m_csScreen.ScreenQuad.GetComponent<MeshCollider>();
            GameObject.Destroy(collider);
            MeshRenderer renderer = m_csScreen.ScreenQuad.GetComponent<MeshRenderer>();
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            renderer.receiveShadows = false;
            renderer.useLightProbes = false;
            m_csScreen.DefaultShader = Shader.Find("UI/Default");
            m_csScreen.DefaultMat = new Material(m_csScreen.DefaultShader);
            renderer.material = m_csScreen.DefaultMat;
            m_csScreen.ScreenQuad.transform.SetParent(m_csScreen.ScreenRoot.transform, false);
        }

        private void CreateProcessCam()
        {
            m_csScreen.ProcessCamObj = new GameObject();
            m_csScreen.ProcessCamObj.name = "ProcessCam";
            m_csScreen.ProcessCamObj.layer = GameLayer.Screen;
            m_csScreen.ProcessCamObj.transform.SetParent(m_csScreen.ScreenRoot.transform, false);
            m_csScreen.ProcessCamObj.transform.localPosition = new Vector3(0.0f, 0.0f, -10.0f);
            m_csScreen.ProcessCam = m_csScreen.ProcessCamObj.AddComponent<Camera>();
            m_csScreen.ProcessCam.enabled = false;
            m_csScreen.ProcessCam.orthographic = true;
            m_csScreen.ProcessCam.farClipPlane = 20.0f;
            m_csScreen.ProcessCam.cullingMask = 1 << GameLayer.Screen;

            m_camProcessor = m_csScreen.ProcessCam;
        }

        private void CreateOutputCam()
        {
            m_csScreen.OutputCamObj = new GameObject();
            m_csScreen.OutputCamObj.name = "OutputCam";
            m_csScreen.OutputCamObj.layer = GameLayer.Screen;
            m_csScreen.OutputCamObj.transform.SetParent(m_csScreen.ScreenRoot.transform, false);
            m_csScreen.OutputCamObj.transform.localPosition = m_csScreen.ProcessCamObj.transform.localPosition;
            m_csScreen.OutputCam = m_csScreen.OutputCamObj.AddComponent<Camera>();
            m_csScreen.OutputCam.CopyFrom(m_csScreen.ProcessCam);
            m_csScreen.OutputCam.enabled = true;

            m_driver = m_csScreen.OutputCamObj.AddComponent<RenderingDriver>();
        }

    }
}
