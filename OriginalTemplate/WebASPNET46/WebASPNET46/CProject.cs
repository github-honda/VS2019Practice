using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebASPNET46
{
    public class CProject
    {
        public readonly static CProject Me = new CProject();
        public bool IsTrace { get; set; }

        public void Run()
        {
            CLog.Me.Trace();
        }
    }
}