using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEvents
{
    public class WindowStartUpEvent
    {
        static public readonly string EVT_NAME = "WINDOW_STARTUP_EVENT";

        public int ModuleID;
        public bool CreateNewIns = false;
        public object Params;

        public WindowStartUpEvent()
        {
            //Params = new Dictionary<string, string>();
        }
    }
}
