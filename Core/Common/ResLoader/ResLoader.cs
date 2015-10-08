﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Common
{
    public static class ResLoader
    {
        public delegate void ON_LOAD_COMPLETE(UnityEngine.Object res);

        public static void Load(string path, ON_LOAD_COMPLETE callback)
        {
            UnityEngine.Object res = Resources.Load<UnityEngine.Object>(path);
            
            if (callback != null)
            {
                callback.Invoke(res);
            }
        }
    }
}
