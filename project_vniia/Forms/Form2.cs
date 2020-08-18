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
           
            textBox1.TextChanged += TextBox1_TextChanged;
            textBox3.TextChanged += TextBox3_TextChanged;
            textBox5.TextChanged += TextBox5_TextChanged;
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            textBox6.Text = textBox5.Text + "\\Done";
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            textBox4.Text = textBox3.Text + "\\Done";
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text + "\\Done";
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
        public static string textbox7_;
        public static string textbox8_;

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
            textbox7_ = textBox7.Text;
            textbox8_ = textBox8.Text;
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

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;  
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox3.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox5.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox7.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox8.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
    }

