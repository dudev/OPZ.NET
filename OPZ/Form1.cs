using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPZ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Parser parser = new Parser(textBox1.Text);
                Tree tree = parser.Eval();
                label3.Text = Parser.ToOpz(tree);
                label5.Text = Convert.ToString(Calculator.Eval(tree));
            }
            catch (Exception ex)
            {
                label3.Text = ex.GetType() + ": " + ex.Message;
                label5.Text = "";
            }
        }
    }
}
