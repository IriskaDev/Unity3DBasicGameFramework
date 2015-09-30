using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Rendering
{
    /// <summary>
    /// 
    /// </summary>
    public class PostProcessUnit : IRenderingNode
    {
        protected Shader m_postProcessShader;
        protected Shader m_defaultShader;
        protected Material m_matScreenMat;
        protected Camera m_camProcessor;

        protected PostProcessUnit()
        {
            m_matScreenMat = RenderingMgr.Instance.ScreenInfo.DefaultMat;
            m_defaultShader = m_matScreenMat.shader;
        }

        /// <summary>
        /// call when added to rendering node list, not able to override
        /// </summary>
        public void BaseInit()
        {
            Reset();
            SetShader();
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
        /// </summary>
        public virtual void Reset()
        {
            m_camProcessor = RenderingMgr.Instance.ScreenInfo.ProcessCam;
        }

        /// <summary>
        /// call when init called
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
        /// call when clear called
        /// </summary>
        private void UnSetShader()
        {
            m_matScreenMat.shader = m_defaultShader;
        }

        /// <summary>
        /// call when init called
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
        /// call when remove from rendering node list, not able to override
        /// </summary>
        public void BaseClear()
        {
            Clear();
            UnSetShader();
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
            if (!renderToFrameBuffer)
            {
                // if not the last rendering node, render with ProcessCam
                // otherwise, render with OutputCam, And Render directly into the real frame buffer
                m_camProcessor.Render();
            }
            RenderingMgr.Instance.BufferSwap();
        }
    }
}
