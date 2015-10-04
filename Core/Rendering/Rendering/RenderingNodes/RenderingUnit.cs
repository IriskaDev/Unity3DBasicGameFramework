using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Rendering
{
    public class RenderingUnit : IRenderingNode
    {
        private string m_strRUName;
        private Camera m_camTarget;
        private Camera m_camProcessor;

        public RenderingUnit(string RUName, Camera targetCam)
        {
            m_strRUName = RUName;
            m_camTarget = targetCam;
            m_camTarget.enabled = false;
            Reset();
        }

        /// <summary>
        /// call when added to rendering node list, not able to override
        /// </summary>
        public void BaseInit()
        {
            Init();
            Reset();
        }

        /// <summary>
        /// call when Initialize BaesInit called
        /// for derived ru class to init their own stuff
        /// </summary>
        public virtual void Init()
        {
            m_camProcessor = RenderingMgr.Instance.ScreenInfo.ProcessCam;
        }

        /// <summary>
        /// call when rendering mgr is calling resume function
        /// and before the node execute
        /// </summary>
        public virtual void Reset()
        {
            m_camTarget.targetTexture = RenderingMgr.Instance.CFrameBuffer;
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
            Clear();
        }

        /// <summary>
        /// call when BaseClear called
        /// for derived ru class to clear their own stuff
        /// </summary>
        public virtual void Clear()
        {

        }

        /// <summary>
        /// call every frame, before render stuff into real frame buffer
        /// </summary>
        public void Execute(float dt, bool renderToFrameBuffer)
        {
            if (m_camTarget.targetTexture != RenderingMgr.Instance.CFrameBuffer)
            {
                m_camTarget.targetTexture = RenderingMgr.Instance.CFrameBuffer;
            }
            Update(dt);
            m_camTarget.Render();
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
