using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Rendering.PostProcessUnits
{
    public class PPUBlackAndWhite : PostProcessUnit
    {
        public PPUBlackAndWhite()
        {
            m_postProcessShader = Shader.Find("PostProcess/BlackAndWhite");
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
