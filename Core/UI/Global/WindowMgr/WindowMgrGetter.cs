using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public partial class WindowMgr
{
    public Camera UICamera
    {
        get
        {
            return m_camUICam;
        }
    }

    public Canvas RootCanvas
    {
        get
        {
            return m_compRootCanvas;
        }
    }
}

