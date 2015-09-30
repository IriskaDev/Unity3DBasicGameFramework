using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Common
{
    public class Ticker : MonoBehaviour
    {
        public delegate void ON_UPDATE(float dt);
        public delegate void ON_LATE_UPDATE(float dt);
        public delegate void ON_FIXED_UPDATE(float dt);

        public ON_UPDATE onUpdate = null;
        public ON_LATE_UPDATE onLateUpdate = null;
        public ON_FIXED_UPDATE onFixedUpdate = null;


        public void Update()
        {
            if (onUpdate != null)
                onUpdate.Invoke(Time.deltaTime);
        }

        public void LateUpdate()
        {
            if (onLateUpdate != null)
                onLateUpdate.Invoke(Time.deltaTime);
        }

        public void FixedUpdate()
        {
            if (onFixedUpdate != null)
                onFixedUpdate.Invoke(Time.deltaTime);
        }
    }
}
