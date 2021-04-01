using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebASPNET46
{
    public class CLog
    {
        public readonly static CLog Me = new CLog();

        public void Trace()
        {
            bool DoSomething;
            if (CProject.Me.IsTrace)
                DoSomething = true;
            else
                DoSomething = false;
        }

    }
}