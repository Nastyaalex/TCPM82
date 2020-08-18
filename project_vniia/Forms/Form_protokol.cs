using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_vniia
{
    public partial class Form_protokol : Form
    {
        public Form_protokol()
        {
            InitializeComponent();
        }
        public Dictionary<string, Form1.MyDB> myDBs;

        private void Form_protokol_Load(object sender, EventArgs e)
        {
            Object templatePathObj_1 = Form1.Protocol_saved + "\\doc1";
            if (templatePathObj_1 == null || templatePathObj_1.ToString() == "")
            {
                WindowsIdentity wi = WindowsIdentity.GetCurrent();
                string w = wi.Name;
                var www = w.Split('\\');
                templatePathObj_1 = "C:\\Users\\" + www[1] + "\\Desktop\\doc1";
                textBox1.Text = templatePathObj_1.ToString();
            }
            else
                textBox1.Text = templatePathObj_1.ToString();
            Delen(myDBs);
        }
        public void Peremen()
        {
            string srtTypeSystem = "";
            string srtNumberSystem = textBox_system.Text;
            Object templatePathObj_1 = textBox1.Text;
            if (templatePathObj_1 == null || templatePathObj_1.ToString() == "")
            {
                WindowsIdentity wi = WindowsIdentity.GetCurrent();
                string w = wi.Name;
                var www = w.Split('\\');
                templatePathObj_1 = "C:\\Users\\" + www[1] + "\\Desktop\\doc1";
            }
            if (srtNumberSystem == "")
            {
                MessageBox.Show("Выберите 'Номер системы'");
                return;
            }

           
            //
            var par = srtNumberSystem.Split(',');
            foreach (var parr in par)
            {
                if (parr.Contains("(н)"))
                {
                    MessageBox.Show("Нейтронные номера систем '(н)' не требуется указывать, они автоматически запишутся после ввода номера системы блоков гамма-излучения!");
                    return;
                }
            }
            if (par.Length > 6)
            {
                MessageBox.Show("Количество систем превышает лимит в 6 штук!");
                return;
            }
            for (int i = 0; i < par.Length; i++)
            {
                if (par[i].Contains(" "))
                {
                    char[] Mychar = { ',', ' ' };
                    par[i] = par[i].TrimStart(Mychar);
                }
            }
            string[,] blocks = new string[par.Length, 8];
            string[,] blocksN = new string[par.Length, 8];
            int k_, k = 0;
            int pered = par.Length;
            foreach (var par_ in par)
            {
                string par_N = par_ + "(н)";
                string par_N_1 = par_ + " (н)";
                foreach (DataRow srt_ in myDBs["[Системы в сборе]"].table.Rows)
                {
                    if (srt_[t1].ToString() == par_ && "" != srt_[t2].ToString() && !srtTypeSystem.Contains(srt_[t2].ToString()))
                    {
                        if (srtTypeSystem == "")
                            srtTypeSystem = srt_[t2].ToString();
                        else
                        {
                            srtTypeSystem = srtTypeSystem + ", " + srt_[t2].ToString();
                        }
                        break;
                    }
                }
                foreach (DataRow srt_ in myDBs["[Системы в сборе]"].table.Rows)
                {
                    if (srt_[t1].ToString() == par_ && "" != srt_[t2].ToString())
                    {
                        k_ = 6;
                        for (int j = 0; j < 8; j++)
                        {
                            blocks[k, j] = srt_[k_].ToString();
                            k_++;
                        }
                        
                    }
                    if((srt_[t1].ToString() == par_N && "" != srt_[t2].ToString())||(srt_[t1].ToString() == par_N_1 && "" != srt_[t2].ToString()))
                    {
                        k_ = 6;
                        for (int j = 0; j < 8; j++)
                        {
                            blocksN[k, j] = srt_[k_].ToString();
                            k_++;
                        }
                    }
                }
                k++;
            }
            var srtT = srtTypeSystem.Split(',');
            if(srtT.Length>1)
            MessageBox.Show("Более одного типа! " + srtTypeSystem);

            if (srtTypeSystem == "")
            {
                MessageBox.Show("У системы с номером " + srtNumberSystem + " нет типа системы!");
                return;
            }
            //
            List<string> BL_type = new List<string>();
            Type_bl(blocks, BL_type, blocksN, pered);

            if(BL_type.Count>2)
            {
                MessageBox.Show("Более двух типов у блоков!");
                return;
            }
            //
            string[,] znach_SU50 = new string[par.Length, 8];
            string[,] znach_SCs50 = new string[par.Length, 8];
            string[,] znach_SCs10 = new string[par.Length, 8];
            string[,] znach_SCfCm = new string[par.Length, 8];
            Iz_tabl_proverka(blocks, blocksN, znach_SU50, znach_SCs50, znach_SCs10, znach_SCfCm);
            //
            if (radioButton_PK.Checked)
            {
                Object templatePathObj = null;

                templatePathObj = Form1.Protocol_ways + "\\ПИ  ТСРМ82-09.07 №55918528 55918530 55918531.docx";
                
                //if (templatePathObj == null || templatePathObj.ToString() == "")
                //{
                //    string s = Environment.CurrentDirectory;
                //    templatePathObj = s + "\\ПИ  ТСРМ82-09.07 №55918528 55918530 55918531.docx";
                //}

                Class_Protokol.Protokol(srtNumberSystem, srtTypeSystem, templatePathObj, blocks, BL_type,
                    blocksN, znach_SU50, znach_SCs50, znach_SCs10, znach_SCfCm, templatePathObj_1);
            }
            if (radioButton_PSI.Checked)
            {
                Object templatePathObj = null;

                templatePathObj =Form1.Protocol_ways+ "\\ПСИ  образец.docx";

                //if (templatePathObj == null || templatePathObj.ToString() == "")
                //{
                //    string s = Environment.CurrentDirectory;
                //    templatePathObj = s + "\\ПСИ  образец.docx";
                //}
                Class_Protokol.Protokol_for_PSI(srtNumberSystem, srtTypeSystem, templatePathObj, blocks, BL_type,
                    blocksN, znach_SU50, znach_SCs50, znach_SCs10, znach_SCfCm, templatePathObj_1);
            }
        }
        public void Iz_tabl_proverka(string[,] blocks, string[,] blocksN, string[,] znach_SU50, string[,] znach_SCs50, string[,] znach_SCs10, string[,] znach_SCfCm)
        {
            int tt = 0, tt1 = 0, tt2=0, tt3=0, tt4=0;
            if (myDBs["[Проверка]"].table.Columns.Contains("S U 50 см"))
            {
                tt = myDBs["[Проверка]"].table.Columns["S U 50 см"].Ordinal;
            }
            if (myDBs["[Проверка]"].table.Columns.Contains("S Cs 50 см"))
            {
                tt1 = myDBs["[Проверка]"].table.Columns["S Cs 50 см"].Ordinal;
            }
            if (myDBs["[Проверка]"].table.Columns.Contains("S Cs 10 см"))
            {
                tt2 = myDBs["[Проверка]"].table.Columns["S Cs 10 см"].Ordinal;
            }
            if (myDBs["[Проверка]"].table.Columns.Contains("Номер БД"))
            {
                tt3 = myDBs["[Проверка]"].table.Columns["Номер БД"].Ordinal;
            }
            if (myDBs["[Проверка]"].table.Columns.Contains("S Pu/Cf 50 см"))
            {
                tt4 = myDBs["[Проверка]"].table.Columns["S Pu/Cf 50 см"].Ordinal;
            }
            int kolvo = 0, lol=0;
            foreach (var typee in blocks)
            {
                if (kolvo >= 8)
                {
                    kolvo = 0;
                    lol++;
                }
                if (typee != "")
                {
                    foreach (DataRow srt_ in myDBs["[Проверка]"].table.Rows)
                    {
                        if (srt_[tt3].ToString() == typee)
                        {
                            if(srt_[tt].ToString() != "")
                                znach_SU50[lol, kolvo] = srt_[tt].ToString();
                            if (srt_[tt1].ToString() != "")
                                znach_SCs50[lol, kolvo] = srt_[tt1].ToString();
                            if (srt_[tt2].ToString() != "")
                                znach_SCs10[lol, kolvo] = srt_[tt2].ToString();
                            //if (srt_[tt4].ToString() != "")
                            //    znach_SCfCm[lol, kolvo] = srt_[tt4].ToString();
                        }
                    }
                }
                kolvo++;
            }
            kolvo = 0; lol = 0;
            foreach (var typee in blocksN)
            {
                if (kolvo >= 8)
                {
                    kolvo = 0;
                    lol++;
                }
                
                    if (typee == null)
                    { }
                    else
                    {
                        foreach (DataRow srt_ in myDBs["[Проверка]"].table.Rows)
                        {
                            if (srt_[tt3].ToString() == typee)
                            {
                                if (srt_[tt4].ToString() != "")
                                    znach_SCfCm[lol, kolvo] = srt_[tt4].ToString();
                            }
                        }
                    }
                
                kolvo++;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(!radioButton_PK.Checked && !radioButton_PSI.Checked)
            {
                MessageBox.Show("Не выбран тип отчёта!");
                return;
            }
            Peremen();
        }
        int t1,t2;

        private void comboBox_number_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textBox_system.Text == "")
                textBox_system.Text = comboBox_number.Text;
            else
           textBox_system.Text = textBox_system.Text + ", "+comboBox_number.Text;
        }

        public void Delen(Dictionary<string, Form1.MyDB> myDBs)
        {
            var table1 = myDBs["[Системы в сборе]"].table.Copy();

            var rows_Location_obj = new List<Object>();

            var rows = table1.Rows;
           
            if (table1.Columns.Contains("Номер системы"))
            {
                t1 = table1.Columns["Номер системы"].Ordinal;
            }
            if (table1.Columns.Contains("Тип системы"))
            {
                t2 = table1.Columns["Тип системы"].Ordinal;
            }
            foreach (DataRow r in rows)
            {
                string a = r[t1].ToString();
                rows_Location_obj.Add(a);
            }
            foreach (var obj in rows_Location_obj)
            {
                comboBox_number.Items.Add(obj);
            }
            //comboBox_number.SelectedItem = comboBox_number.Items[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        public void Type_bl(string[,] blocks, List<string> BL_type, string[,] blocksN, int pered)
        {
            int tt = 0, tt1=0;
            if (myDBs["[Блоки]"].table.Columns.Contains("Номер БД"))
            {
                tt = myDBs["[Блоки]"].table.Columns["Номер БД"].Ordinal;
            }
            if (myDBs["[Блоки]"].table.Columns.Contains("Тип БД"))
            {
                tt1 = myDBs["[Блоки]"].table.Columns["Тип БД"].Ordinal;
            }
            string[,] blocks_ = new string[pered,8];
            int i = 0,j=0;
            foreach (var bl in blocks)
            {
                if(bl!="")
                foreach (DataRow srt_ in myDBs["[Блоки]"].table.Rows)
                {
                        if(j>=8)
                        {
                            i++;
                            j = 0;
                        }
                        if (srt_[tt].ToString() == bl && srt_[tt1].ToString().Contains("ТСРМ85"))
                        {
                            blocks_[i,j] = bl;
                        }
                        if (srt_[tt].ToString() == bl && !BL_type.Contains(srt_[tt1].ToString()))
                    {
                            
                        BL_type.Add(srt_[tt1].ToString());
                        break;
                    }
                }j++;
            }
                for(int h=0;h<pered;h++)
                {
                    for(int l=0;l<8;l++)
                    {
                        if(blocks_[h,l] != null || blocks_[h, l] != "")
                        {
                            if (blocks_[h, l] == null)
                            { }
                            else
                            {
                                blocks[h, l] = "";
                                for (int t_2 = 0; t_2 < 8; t_2++)
                                {
                                    if (blocksN[h, t_2] == "" || blocksN[h, t_2] == null)
                                    {
                                        blocksN[h, t_2] = blocks_[h, l];
                                        break;
                                    }

                                }
                            }
                        }
                    }
                }
            foreach (var bl_ in blocksN)
            {
                if (bl_ != "")
                    foreach (DataRow srt_ in myDBs["[Блоки]"].table.Rows)
                    {
                        if (srt_[tt].ToString() == bl_ && !BL_type.Contains(srt_[tt1].ToString()))
                        {
                            BL_type.Add(srt_[tt1].ToString());
                            break;
                        }
                    }
            }
        }
    }
}
