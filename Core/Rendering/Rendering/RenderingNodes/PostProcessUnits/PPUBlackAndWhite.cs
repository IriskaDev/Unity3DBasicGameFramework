using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Rendering.PostProcessUnits
{
    public class PPUBlackAndWhite : PostProcessUnit
    {
        public PPUBlackAndWhite() : base("BlacnAndWhite")
        {
            m_postProcessShader = Shader.Find("PostProcess/BlackAndWhite");
            Debug.Log(m_strPPUName);
        }

        public override void SetShaderParam()
        {
            
        }

        public override void Init()
        {

        }

        protected override void Update(float dt)
        {

        }

        public override void Clear()
        {

        }
    }
}
