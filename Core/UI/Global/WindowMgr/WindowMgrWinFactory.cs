using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class WindowMgr
{
    public static IWindow GetWinInstance(int moduleId)
    {
        switch (moduleId)
        {
            //case UIModule.LOGIN:
            //    return new Login();
            default:
                throw new Exception("ModuleID: " + moduleId + " Class Not Registed");
        }
    }
}
