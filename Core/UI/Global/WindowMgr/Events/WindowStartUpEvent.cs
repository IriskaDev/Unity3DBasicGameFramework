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
        public Dictionary<string, string> Params;

        public WindowStartUpEvent()
        {
            //Params = new Dictionary<string, string>();
        }
    }
}
