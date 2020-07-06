using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_vniia
{
    public partial class Form_way_system : Form
    {
        public Form_way_system()
        {
            InitializeComponent();
        }

        public static string textbox1_;

        private void Form_way_system_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textbox1_ = textBox1.Text;
            Close();
        }
    }
}
