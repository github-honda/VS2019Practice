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
        Class1 _class1;  // OK.
        Class1 _class2; // CS0169	The field 'Form1._class2' is never used
        string _somevar1; // OK.
        string some_one_say; // OK.
        string mString1; // OK.
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // IDE1006	Naming rule violation: These words must begin with upper case characters
        {
            textBox1.Text = DateTime.Now.ToString();
            _somevar1 = "test";
            textBox1.Text = _somevar1;
            some_one_say = "hello";
            textBox1.Text = some_one_say;
            mString1 = _somevar1;
            textBox1.Text = mString1;
        }

        private void some_function_here() // Message IDE1006	Naming rule violation: These words must begin with upper case characters: some_function_here
        { }

        private void Some_function_here_OK() // OK
        { }

        private void Form1_Load(object sender, EventArgs e)
        {
            _class1 = new Class1();  // IDE0017	Object initialization can be simplified
            _class1._s111 = DateTime.Now.ToString();

            textBox1.Text = _class1._s111;
        }
    }
}
