using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public partial class WindowMgr
{
    private Dictionary<int, int> m_dictCurrentInsIdTail = new Dictionary<int, int>();

    public int GetInstanceID(int moduleId)
    {
        int instanceIdTail;
        if (!m_dictCurrentInsIdTail.TryGetValue(moduleId, out instanceIdTail))
        {
            m_dictCurrentInsIdTail[moduleId] = 0;
            instanceIdTail = 0;
        }
        int instanceId = moduleId * 100 + (instanceIdTail % 100);
        for (int i = 0; i < 100 && m_hsUsedInsID.Contains(instanceId); ++i)
        {
            instanceIdTail += 1;
            instanceId = moduleId * 100 + (instanceIdTail % 100);
            if (i == 99)
            {
                if (m_hsUsedInsID.Contains(instanceId))
                {
                    throw new Exception("Too Much Window Instance of Module-" + moduleId);
                }
            }
        }
        m_dictCurrentInsIdTail[moduleId] = instanceIdTail + 1;
        m_hsUsedInsID.Add(instanceId);
        return instanceId;
    }

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

