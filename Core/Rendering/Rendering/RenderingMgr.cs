using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Rendering
{
    public struct ScreenStruct
    {
        public GameObject ScreenRoot;
        public GameObject ScreenQuad;
        public GameObject ProcessCamObj;
        public GameObject OutputCamObj;
        public Camera ProcessCam;
        public Camera OutputCam;
        public Material DefaultMat;
        public RenderTexture FFrameBuffer;
        public RenderTexture BFrameBuffer;
    };

    public enum RESOLUTION
    {
        R1280x720 = 0,
        R1920x1080 = 1,
        R1680x1050 = 2
    }

    public partial class RenderingMgr : Singleton<RenderingMgr>
    {
        static private Vector2 m_vec2Resolution;

        private LinkedList<IRenderingNode> m_llRenderingNodeList;
        private Camera m_camProcessor;
        private RenderingDriver m_driver;
        private ScreenStruct m_csScreen;
        private Ticker m_ticker;
        private Dictionary<string, LinkedListNode<IRenderingNode>> m_dicCrucialNodes;


        public RenderingMgr()
        {
            m_dicCrucialNodes = new Dictionary<string, LinkedListNode<IRenderingNode>>();
            m_llRenderingNodeList = new LinkedList<IRenderingNode>();
        }

        public RenderTexture CFrameBuffer
        {
            get
            {
                return m_csScreen.FFrameBuffer;
            }
        }

        public ScreenStruct ScreenInfo
        {
            get
            {
                return m_csScreen;
            }
        }

        public void SetResolution(RESOLUTION r)
        {
            ForceTerminateRendering();
            switch(r)
            {
                case RESOLUTION.R1280x720:
                    m_csScreen.FFrameBuffer = new RenderTexture(1280, 720, 24);
                    m_csScreen.BFrameBuffer = new RenderTexture(1280, 720, 24);
                    m_vec2Resolution = new Vector2(1280, 720);
                    break;
                case RESOLUTION.R1680x1050:
                    m_csScreen.FFrameBuffer = new RenderTexture(1680, 1050, 24);
                    m_csScreen.BFrameBuffer = new RenderTexture(1680, 1050, 24);
                    m_vec2Resolution = new Vector2(1680, 1050);
                    break;
                case RESOLUTION.R1920x1080:
                    m_csScreen.FFrameBuffer = new RenderTexture(1920, 1080, 24);
                    m_csScreen.BFrameBuffer = new RenderTexture(1920, 1080, 24);
                    m_vec2Resolution = new Vector2(1920, 1080);
                    break;
                default:
                    m_csScreen.FFrameBuffer = new RenderTexture(1280, 720, 24);
                    m_csScreen.BFrameBuffer = new RenderTexture(1280, 720, 24);
                    m_vec2Resolution = new Vector2(1280, 720);
                    break;
            }
            SetFrameBuffer();
            float y = m_csScreen.ProcessCam.orthographicSize * 2.0f;
            float x = m_vec2Resolution.x / m_vec2Resolution.y * y;
            m_csScreen.ScreenQuad.transform.localScale = new Vector3(x, y, 1.0f);
            //m_csScreen.RawImgComp.texture = m_csScreen.CFrameBuffer;
            //m_csScreen.RawImgComp.material = m_csScreen.DefaultMat;
        }

    }
}
