using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Rendering
{
    /// <summary>
    /// if your PostProcessUnit have muilti passes, do not render the last pass by yourself
    /// </summary>
    public class PostProcessUnit : IRenderingNode
    {
        protected string m_strPPUName;
        protected Shader m_postProcessShader;
        protected Shader m_defaultShader;
        protected Material m_matScreenMat;
        protected Camera m_camProcessor;
        protected bool m_bNeedToUnsetShader = true;

        protected PostProcessUnit()
        {

        }

        protected PostProcessUnit(string PPUName)
        {
            m_strPPUName = PPUName;
        }

        /// <summary>
        /// call when added to rendering node list, not able to override
        /// </summary>
        public void BaseInit()
        {
            m_matScreenMat = RenderingMgr.Instance.ScreenInfo.DefaultMat;
            m_defaultShader = RenderingMgr.Instance.ScreenInfo.DefaultShader;
            m_camProcessor = RenderingMgr.Instance.ScreenInfo.ProcessCam;
            Reset();
            SetShaderParam();
            Init();
        }

        /// <summary>
        /// call when Initialize called
        /// for derived ppu class to init their own stuff
        /// </summary>
        public virtual void Init()
        {

        }

        /// <summary>
        /// call when rendering mgr is calling resume function
        /// and before the node execute
        /// </summary>
        public virtual void Reset()
        {
            SetShader();
        }

        /// <summary>
        /// call when reset called
        /// </summary>
        private void SetShader()
        {
            if (m_postProcessShader == null)
            {
                throw new Exception("PLEASE DO SET m_postProcessShader IN YOUR CLASS' CONSTRUCT FUNCION!!!");
            }
            m_matScreenMat.shader = m_postProcessShader;
        }

        /// <summary>
        /// call when BaseClear called
        /// </summary>
        private void UnSetShader()
        {
            m_matScreenMat.shader = m_defaultShader;
        }

        /// <summary>
        /// call when reset called
        /// for shader param initialize
        /// </summary>
        public virtual void SetShaderParam()
        {

        }

        /// <summary>
        /// call before execute called
        /// </summary> 
        /// <param name="dt"></param>
        protected virtual void Update(float dt)
        {

        }

        /// <summary>
        /// call after node is executed
        /// </summary>
        public void BaseClear()
        {
            if (m_bNeedToUnsetShader)
                UnSetShader();
            Clear();
            //keep this below UnSetShader();
            RenderingMgr.Instance.BufferSwap();
        }

        /// <summary>
        /// call when Clear called
        /// for derived ppu class to clear their own stuff
        /// </summary>
        public virtual void Clear()
        {

        }

        /// <summary>
        /// call every frame, before render stuff into real frame buffer
        /// </summary>
        public void Execute(float dt, bool renderToFrameBuffer)
        {
            Update(dt);
            m_camProcessor.Render();
            //if not the last rendering node, render with ProcessCam
            //otherwise, render with OutputCam, And Render directly into the real frame buffer
            m_bNeedToUnsetShader = !renderToFrameBuffer;
        }

        /// <summary>
        /// call when it's deleted from rendering node list
        /// </summary>
        public virtual void Dispose()
        {
            m_camProcessor = null;
        }
    }
}
