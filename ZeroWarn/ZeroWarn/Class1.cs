using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroWarn
{
    public class Class1
    {
        public string Field1 { get; set; } // OK for Naming rule.
        public string FieldTest1 { get; set; } // OK for Naming rule.
        public string Fieldtest1 { get; set; } // OK for Naming rule.

        // IDE1006 Naming rule violation: Prefix '_' is not expected.
        public string _Field2 { get; set; } 

        // IDE1006 Naming rule violation: These words must begin with upper case characters.
        public string sName { get; set; }

        public void Method1() // OK for Naming rule.
        { }
        public void MethodTest1() // OK for Naming rule.
        { }
        public void Methodtest1() // OK for Naming rule.
        { }

        // IDE1006 Naming rule violation: These words must begin with upper case characters.
        public void method1()
        { }

        // IDE1006 Naming rule violation: These words must begin with upper case characters.
        public void methodtest1()
        { }

    }
}
