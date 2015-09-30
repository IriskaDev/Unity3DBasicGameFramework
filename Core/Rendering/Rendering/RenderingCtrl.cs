using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Rendering
{
    public partial class RenderingMgr
    {
        private void SetFrameBuffer()
        {
            m_csScreen.DefaultMat.SetTexture("_MainTex", m_csScreen.FFrameBuffer);
        }

        public void BufferSwap()
        {
            RenderTexture rt = m_csScreen.FFrameBuffer;
            m_csScreen.FFrameBuffer = m_csScreen.BFrameBuffer;
            m_csScreen.BFrameBuffer = rt;
            m_camProcessor.targetTexture = m_csScreen.BFrameBuffer;
            SetFrameBuffer();
        }

        private void ClearBuffer()
        {

            int culling = m_camProcessor.cullingMask;
            CameraClearFlags flag = m_camProcessor.clearFlags;
            m_camProcessor.clearFlags = CameraClearFlags.Skybox;
            m_camProcessor.cullingMask = 0;
            m_camProcessor.targetTexture = m_csScreen.FFrameBuffer;
            m_camProcessor.Render();
            m_camProcessor.targetTexture = m_csScreen.BFrameBuffer;
            m_camProcessor.Render();
            m_camProcessor.cullingMask = culling;
            m_camProcessor.clearFlags = flag;
        }

        private void ExecuteNodeList(float dt)
        {
            if (m_llRenderingNodeList.Count > 0)
            {
                ClearBuffer();
                LinkedListNode<IRenderingNode> iter = m_llRenderingNodeList.First;
                for (; iter != null; iter = iter.Next)
                {
                    // if no next node, render to frame buffer immediately
                    iter.Value.Execute(dt, iter.Next == null);
                }

                //finish processing, copy the CFrameBuffer to the real frame buffer
                //m_camProcessor.targetTexture = null;
            }
        }

        private void ForceTerminateRendering()
        {
            m_camProcessor.targetTexture = null;
            //m_csScreen.RawImgComp.texture = null;
            if (m_csScreen.FFrameBuffer != null)
            {
                m_csScreen.FFrameBuffer.Release();
                m_csScreen.FFrameBuffer = null;
            }
            if (m_csScreen.BFrameBuffer != null)
            {
                m_csScreen.BFrameBuffer.Release();
                m_csScreen.BFrameBuffer = null;
            }

            //remove ExecuteNodeList from ticker
            m_driver.onPreRender = null;
        }

        private void ResumeRendering()
        {
            //Add ExecuteNodeList to ticker
            LinkedListNode<IRenderingNode> iter = m_llRenderingNodeList.First;
            for (; iter != null; iter = iter.Next)
            {
                iter.Value.Reset();
            }
            m_driver.onPreRender = ExecuteNodeList;
        }

    }
}
