using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public struct WindowInfo
{
    public int ModuleID;
    public string ResName;
    public bool UniqeWindow;
}

public static class WindowInfoMgr
{
    private static Dictionary<int, WindowInfo> m_dictWinInfoMapper = new Dictionary<int, WindowInfo>();

    public static void Init()
    {
        //WindowInfo loginInfo;
        //loginInfo.ModuleID = UIModule.LOGIN;
        //loginInfo.ResName = "UGUILogin";
        //loginInfo.UniqeWindow = true;
        //m_dictWinInfoMapper.Add(UIModule.LOGIN, loginInfo);
    }

    public static WindowInfo GetWindowInfo(int moduleId)
    {
        WindowInfo info;
        if (m_dictWinInfoMapper.TryGetValue(moduleId, out info))
        {
            return info;
        }
        return default(WindowInfo);
    }
}
