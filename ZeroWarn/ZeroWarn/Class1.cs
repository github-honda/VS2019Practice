using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroWarn
{
    public class Class1
    {
        public string _s111 { get; set; } // IDE1006	Naming rule violation: Prefix '_' is not expected
        public string sName { get; set; } // IDE1006	Naming rule violation: These words must begin with upper case characters

        public string Name { get; set; } // OK
    }
}
