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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            FormClosing += Form2_FormClosing1;
        }

        private void Form2_FormClosing1(object sender, FormClosingEventArgs e)
        {
            if(!knopka)
                close_all = true;
        }

        bool knopka = false;
        public static bool close_all = false;
        public static string textbox1_;
        public static string textbox2_;
        public static string textbox3_;
        public static string textbox4_;
        public static string textbox5_;
        public static string textbox6_;

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textbox1_ = textBox1.Text;
            textbox2_ = textBox2.Text;
            textbox3_ = textBox3.Text;
            textbox4_ = textBox4.Text;
            textbox5_ = textBox5.Text;
            textbox6_ = textBox6.Text;
            knopka = true;
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }

