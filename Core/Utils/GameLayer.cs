using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

static public class GameLayer
{
    static public int UI
    {
        get
        {
            return LayerMask.NameToLayer("UI");
        }
    }

    static public int Screen
    {
        get
        {
            return LayerMask.NameToLayer("Screen");
        }
    }
}
