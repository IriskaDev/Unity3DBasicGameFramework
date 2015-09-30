using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Rendering.PostProcessUnits
{
    public class PPUBloom : PostProcessUnit
    {
        public PPUBloom()
        {
            m_postProcessShader = Shader.Find("PostProcess/Bloom");
        }
    }
}
