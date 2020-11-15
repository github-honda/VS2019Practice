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

        // 方法必須大寫字母開頭.
        // Property 別用 "_" 當字頭.
        // Field, const 則不影響
        public string _Field2 { get; set; } // IDE1006 Naming rule violation: Prefix '_' is not expected.
        public string _Field3;
        public const string _Const4 = "";

        public string sName { get; set; } // IDE1006 Naming rule violation: These words must begin with upper case characters.


        // 以 bool 替代 Boolean.
        public const Boolean bDebug = true; // IDE0049 Name can be simplified. Change Boolean to Bool.
        public const bool bDebugOk = true;
        public const bool _bDebugOk = true;

        public void Method1() // OK for Naming rule.
        { }
        public void MethodTest1() // OK for Naming rule.
        { }
        public void Methodtest1() // OK for Naming rule.
        { }

        // 方法必須大寫字母開頭.
        // IDE1006 Naming rule violation: These words must begin with upper case characters.
        public void method1()
        {
            string _LocalVarUnderLineOK;
            string localvarbeginwithlowercharOK;
            Boolean bLocalBooleanOK = true;


            // Message	IDE0059	Unnecessary assignment of a value to 'UnnecessaryAssignmentOfAValue'.
            string UnnecessaryAssignmentOfAValue = string.Empty;

            if (bDebug)
                Field1 = "dummy";

            _LocalVarUnderLineOK = Field1;
            if (_LocalVarUnderLineOK == Field1)
                _LocalVarUnderLineOK = Field1;

            localvarbeginwithlowercharOK = Field1;
            if (localvarbeginwithlowercharOK == Field1)
                localvarbeginwithlowercharOK = Field1;

            if (bLocalBooleanOK)
                _LocalVarUnderLineOK = "Dummy";

        }

        // IDE1006 Naming rule violation: These words must begin with upper case characters.
        public void methodtest1()
        { }

    }
}
