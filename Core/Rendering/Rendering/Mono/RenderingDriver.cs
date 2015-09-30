using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Rendering
{
    public class RenderingDriver : MonoBehaviour
    {
        public delegate void ON_PRE_RENDER(float dt);
        public delegate void ON_POST_RENDER(float dt);

        public ON_PRE_RENDER onPreRender = null;
        public ON_POST_RENDER onPostRender = null;

        public void OnPreRender()
        {
            if (onPreRender != null)
                onPreRender.Invoke(Time.deltaTime);
        }

        public void OnPostRender()
        {
            if (onPostRender != null)
                onPostRender.Invoke(Time.deltaTime);
        }
    }
}
