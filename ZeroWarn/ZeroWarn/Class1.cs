/*

Summary:
1. Fieldtest1 is OK for naming rule. (fieldtest1, _fieldtest1 is NOT OK).
2. Const can begin with lower case characters and Prefix '_'. Why ?

 */
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

        // IDE0049 Name can be simplified. Because of "Boolean".
        public const Boolean bDebug = true;

        // To fix IDE0049 Name can be simplified. Because of "Boolean".
        // Why const can begin with lower case characters and Prefix '_' ?
        public const bool bDebugOk = true;
        public const bool _bDebugOk = true;

        public void Method1() // OK for Naming rule.
        { }
        public void MethodTest1() // OK for Naming rule.
        { }
        public void Methodtest1() // OK for Naming rule.
        { }

        // IDE1006 Naming rule violation: These words must begin with upper case characters.
        public void method1()
        {
            if (bDebug)
                Field1 = "dummy";
        }

        // IDE1006 Naming rule violation: These words must begin with upper case characters.
        public void methodtest1()
        { }

    }
}
