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
            FormClosing += Form_way_system_FormClosing;
        }

        private void Form_way_system_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!knopka1)
                close_all1 = true;
        }
        bool knopka1 = false;
        public static bool close_all1 = false;
        public static string textbox1_;

        private void Form_way_system_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textbox1_ = textBox1.Text;
            knopka1 = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
