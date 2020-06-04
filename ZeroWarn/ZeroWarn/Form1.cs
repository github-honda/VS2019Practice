using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZeroWarn
{
    public partial class Form1 : Form
    {
        Class1 _class1;  // OK for Naming rule.

        // IDE0044 Make field readonly.
        // IDE0051 Private member is unused.
        Class1 _class2; 

        string _somevar1; // OK for Naming rule.
        string some_one_say; // OK for Naming rule.
        string mString1; // OK for Naming rule.

        public Form1()
        {
            InitializeComponent();
        }

        // IDE1006	Naming rule violation: These words must begin with upper case characters: button1_Click.
        private void button1_Click(object sender, EventArgs e) 
        {
            textBox1.Text = DateTime.Now.ToString();
            _somevar1 = "test";
            textBox1.Text = _somevar1;
            some_one_say = "hello";
            textBox1.Text = some_one_say;
            mString1 = _somevar1;
            textBox1.Text = mString1;
        }

        // IDE1006 Naming rule violation: These words must begin with upper case characters: some_function_here.
        // IDE0051 Private member is unused.
        private void some_function_here()
        { }


        // IDE0051 Private member is unused.
        private void Some_function_here_OK()
        { }

        private void Form1_Load(object sender, EventArgs e)
        {
            // IDE0017 Object initialization can be simplified
            _class1 = new Class1();  
            _class1.Field1 = DateTime.Now.ToString();

            // To fix IDE0017 Object initialization can be simplified.
            _class1 = new Class1
            {
                Field1 = DateTime.Now.ToString()
            };

            textBox1.Text = _class1.Field1;
        }
    }
}
