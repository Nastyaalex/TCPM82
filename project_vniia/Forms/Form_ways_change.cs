using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_vniia
{
    public partial class Form_ways_change : Form
    {
        public Form_ways_change()
        {
            InitializeComponent();
            Form_System _System = new Form_System();
            

            textBox1.TextChanged += TextBox1_TextChanged;
            textBox3.TextChanged += TextBox3_TextChanged;
            textBox5.TextChanged += TextBox5_TextChanged;

            textBox1.Text=Form1.Log_ways;
            textBox2.Text = Form1.Log_ways_peremesti;
            textBox3.Text = Form1.Zamech_ways;
            textBox4.Text = Form1.Zamech_ways_peremesti;
            textBox5.Text = Form1.Proverka_ways;
            textBox6.Text = Form1.Proverka_ways_perem;
            textBox7.Text = Form_System.System_ways;
            textBox8.Text = Form1.conString;
            textBox9.Text = Form1.Protocol_ways;
            textBox10.Text = Form1.Protocol_saved;
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox7.Text = folderBrowserDialog1.SelectedPath;
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            string[] textb = new string[10];
            textb[0] = textBox1.Text;
            textb[1] = textBox2.Text;
            textb[2] = textBox3.Text;
            textb[3] = textBox4.Text;
            textb[4] = textBox5.Text;
            textb[5] = textBox6.Text;
            textb[8] = textBox7.Text;
            textb[9] = textBox8.Text;
            textb[6] = textBox9.Text;
            textb[7] = textBox10.Text;
            foreach (var text in textb)
            {
                if(text=="")
                {
                    MessageBox.Show("Проверьте, чтобы все поля были заполнены!");
                    return;
                }
            }
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            foreach (var www in Form1._ways_)
            {
                if(File.Exists(path + "\\TestWay" + www))
                File.Delete(path + "\\TestWay" + www);
            }
            for (int i = 0; i < 8; i++)
            {
                if (textb[i].Contains("\\Done"))
                {
                    if (!Directory.Exists(textb[i]))
                        Directory.CreateDirectory(textb[i]);
                }
                if (!File.Exists(path + "\\TestWay" + Form1._ways_[i]))
                {
                    File.Create(path + "\\TestWay" + Form1._ways_[i]).Close();
                }
                using (StreamWriter sw = new StreamWriter(path + "\\TestWay" + Form1._ways_[i]))
               {
                    sw.WriteLine("{0}", textb[i]);
               }
            }
            if (File.Exists(path + "\\TestWay" + "\\build_systems.txt"))
                File.Delete(path + "\\TestWay" + "\\build_systems.txt");
            if (!File.Exists(path + "\\TestWay" + "\\build_systems.txt"))
            {
                File.Create(path + "\\TestWay" + "\\build_systems.txt").Close();
            }
            using (StreamWriter sw = new StreamWriter(path + "\\TestWay" + "\\build_systems.txt"))
            {
                sw.WriteLine("{0}", textb[8]);
            }
            /////
            if (File.Exists(path + "\\TestWay\\savings.txt"))
            {
                File.Delete(path + "\\TestWay\\savings.txt");
            }
            if (!File.Exists(path + "\\TestWay\\savings.txt"))
            {
                File.Create(path + "\\TestWay\\savings.txt").Close();
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(path + "\\TestWay\\savings.txt"))
                {
                        sw.WriteLine("{0}", textb[9]);
                }
            }
            catch (Exception k)
            {
            }

            ////
            Form1.Log_ways = textb[0];
            Form1.Log_ways_peremesti = textb[1];
            Form1.Zamech_ways = textb[2];
            Form1.Zamech_ways_peremesti = textb[3];
            Form1.Proverka_ways = textb[4];
            Form1.Proverka_ways_perem = textb[5];
            Form_System.System_ways= textb[8];
            Form1.conString = textb[9];
            Form1.Protocol_ways = textb[6];
            Form1.Protocol_saved = textb[7];
        }

        private void Form_ways_change_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox8.Text = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + openFileDialog1.FileName;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox9.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox10.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
