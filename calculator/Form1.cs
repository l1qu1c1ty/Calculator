using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace calculator
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
        }

        private void one_Click(object sender, EventArgs e)
        {
            operate1.Text += "1";
        }

        private void two_Click(object sender, EventArgs e)
        {
            operate1.Text += "2";
        }

        private void three_Click(object sender, EventArgs e)
        {
            operate1.Text += "3";
        }

        private void four_Click(object sender, EventArgs e)
        {
            operate1.Text += "4";
        }

        private void five_Click(object sender, EventArgs e)
        {
            operate1.Text += "5";
        }

        private void six_Click(object sender, EventArgs e)
        {
            operate1.Text += "6";
        }

        private void seven_Click(object sender, EventArgs e)
        {
            operate1.Text += "7";
        }

        private void eight_Click(object sender, EventArgs e)
        {
            operate1.Text += "8";
        }

        private void nine_Click(object sender, EventArgs e)
        {
            operate1.Text += "9";
        }

        private void percent_Click(object sender, EventArgs e)
        {
            operate1.Text += "%";
        }

        private void zero_Click(object sender, EventArgs e)
        {
            operate1.Text += "0";
        }

        private void semicolon_Click(object sender, EventArgs e)
        {
            operate1.Text += ".";
        }

        private void clear_Click(object sender, EventArgs e)
        {
            operate1.Clear();
            operate2.Text = "= ";
        }

        private void divide_Click(object sender, EventArgs e)
        {
            operate1.Text += "/";
        }

        private void multiply_Click(object sender, EventArgs e)
        {
            operate1.Text += "*";
        }

        private void subtraction_Click(object sender, EventArgs e)
        {
            operate1.Text += "-";
        }

        private void addition_Click(object sender, EventArgs e)
        {
            operate1.Text += "+";
        }

        public static object Eval(string sCSCode)
        {

            CSharpCodeProvider c = new CSharpCodeProvider();
            ICodeCompiler icc = c.CreateCompiler();
            CompilerParameters cp = new CompilerParameters();

            cp.ReferencedAssemblies.Add("system.dll");
            cp.ReferencedAssemblies.Add("system.xml.dll");
            cp.ReferencedAssemblies.Add("system.data.dll");
            cp.ReferencedAssemblies.Add("system.windows.forms.dll");
            cp.ReferencedAssemblies.Add("system.drawing.dll");

            cp.CompilerOptions = "/t:library";
            cp.GenerateInMemory = true;

            StringBuilder sb = new StringBuilder("");
            sb.Append("using System;\n");
            sb.Append("using System.Xml;\n");
            sb.Append("using System.Data;\n");
            sb.Append("using System.Data.SqlClient;\n");
            sb.Append("using System.Windows.Forms;\n");
            sb.Append("using System.Drawing;\n");

            sb.Append("namespace CSCodeEvaler{ \n");
            sb.Append("public class CSCodeEvaler{ \n");
            sb.Append("public object EvalCode(){\n");
            sb.Append("return " + sCSCode + "; \n");
            sb.Append("} \n");
            sb.Append("} \n");
            sb.Append("}\n");

            CompilerResults cr = icc.CompileAssemblyFromSource(cp, sb.ToString());
            if (cr.Errors.Count > 0)
            {
                MessageBox.Show("ERROR: " + cr.Errors[0].ErrorText,
                   "Error evaluating cs code", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return null;
            }

            System.Reflection.Assembly a = cr.CompiledAssembly;
            object o = a.CreateInstance("CSCodeEvaler.CSCodeEvaler");

            Type t = o.GetType();
            MethodInfo mi = t.GetMethod("EvalCode");

            object s = mi.Invoke(o, null);
            return s;

        }

        private void equal_Click(object sender, EventArgs e)
        {
            var eval = Eval(operate1.Text);
            operate2.Text += eval;
        }

        private void Eval()
        {
            throw new NotImplementedException();
        }
    }
}
