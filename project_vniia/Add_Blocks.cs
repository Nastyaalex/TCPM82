using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static project_vniia.Form1;

namespace project_vniia
{
    public partial class Add_Blocks : Form
    {
        public DataTable blocks_T;
        public DataTable zamech_T;
        public Button peregr;

        public Add_Blocks()
        {
            InitializeComponent();
            dataGridView1.RowsAdded += DataGridView1_RowsAdded;
            
        }
        int ttt = 0, tt2 = 0, ttt_;
        char tt, tt1;
       
       
        private void button_add_blocks_Click(object sender, EventArgs e)
        {
            bool[] f = new bool[dataGridView1.Rows.Count];
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                f[i] = true;
            }
            int br = 0;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                string BD = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);

                f[i] = Proverka_add(BD);//проверка на наличие этого блока
                if (!f[i])
                {
                    br = i;
                    break;
                }
            }
            if (!f[br])
            {
                MessageBox.Show("Попытка добавить существующий блок!"+"\n\r"+ dataGridView1.Rows[br].Cells[0].Value);
            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    DataRow newrow = blocks_T.NewRow();
                    newrow["Номер БД"] = dataGridView1.Rows[i].Cells[0].Value;
                    newrow["Тип БД"] = dataGridView1.Rows[i].Cells[1].Value;
                    newrow["Номер ФЭУ"] = dataGridView1.Rows[i].Cells[2].Value;
                    newrow["Номинальное U"] = dataGridView1.Rows[i].Cells[3].Value;

                    newrow["Примечания"] = " ";
                    newrow["Местоположение"] = "п.561";
                    newrow["Отметка выполнения"] = "?";

                    blocks_T.Rows.Add(newrow);
                }
                
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                        DataRow newrow = zamech_T.NewRow();
                        var ppp = zamech_T.Rows[zamech_T.Rows.Count - 1].ItemArray;
                        var pp = Convert.ToUInt32(ppp[0]) + 1;
                        newrow["Номер блока"] = dataGridView1.Rows[i].Cells[0].Value;
                        newrow["Дата заметки"] = dataGridView1.Rows[i].Cells[4].Value;
                        //newrow["Канал_Св"] = " "; нельзя так как ожидает int
                        newrow["Cs при Uном"] = "0";
                        newrow["Заметка"] = "ЭЭСШ = " + dataGridView1.Rows[i].Cells[5].Value;
                        newrow["Номер записи"] = pp;
                        zamech_T.Rows.Add(newrow);
                }
                peregr.PerformClick();
            }
            // доработать таймер
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            button_add_blocks.BackColor = colors[counter++];
            if (counter == colors.Length)
            {
                counter = 0;
                timer.Stop();
            }
        }
        Timer timer = new Timer();
        Color[] colors = { Color.AliceBlue, Color.AntiqueWhite, Color.Aqua, Color.Aquamarine, Color.Azure };
        int counter = 0;


        bool Proverka_add(string BD)
        {
            bool t = true;
            var table1 = blocks_T;
            
            var table2 = table1.Copy();
            
            var rows_to_delete = new List<DataRow>();

            var rows = table2.Rows;
            foreach (DataRow r in rows)
            {
                bool f = true;
                var c = r.ItemArray[0];
                
                    if (c.ToString() == BD)
                    {
                        f = false;
                    }
                
                if (f)
                {
                    rows_to_delete.Add(r);
                }
                Console.WriteLine();
            }

            foreach (var r in rows_to_delete)
            {
                rows.Remove(r);
            }

            if (table2.Rows.Count >= 1)
            {
                t = false;
                return t;
            }
            else
            {
                return t;
            }
        }
        private void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int index = e.RowIndex;
            if (index > 1)
            {
                index--;
                var row = dataGridView1.Rows[index];
                var row1 = dataGridView1.Rows[index - 1];
                var a = row1.Cells[0].Value;
                var b = row1.Cells[1].Value;
                var с = row1.Cells[4].Value;

                var d = row1.Cells[2].Value;
                var nom_u = row1.Cells[3].Value;
                var shum = row1.Cells[5].Value;
                string aaa = a.ToString();
                var t = aaa.ToCharArray().All(char.IsDigit);
                Console.WriteLine(t);

                if (t)
                {
                    int aa = Convert.ToInt32(a);
                    a = aa + 1;
                }
                else
                {
                    if (tt2 == 0)
                    {
                        a = aaa + ttt;
                        aaa = a.ToString();
                        tt = Convert.ToChar("0");
                        ttt = aaa.IndexOf(tt);
                    }

                    else
                    {
                        tt = Convert.ToChar(aaa.Substring(ttt));
                        ttt_ = Convert.ToInt32(tt);
                        ttt_++;
                        tt1 = Convert.ToChar(ttt_);
                        aaa = aaa.Replace(tt, tt1);
                        a = aaa;
                    }
                    tt2++;
                }
                string d_=Convert.ToString(d);
                Regex regex = new Regex(@"^[a-zA-Z]");
                string gg = Convert.ToString(d_[0]);
                gg = regex.IsMatch(gg) ? "is":"is not";
                if (gg=="is")
                {
                    row.Cells[3].Value = nom_u;
                    row.Cells[5].Value = shum;
                }
                row.Cells[0].Value = a;
                row.Cells[1].Value = b;
                row.Cells[4].Value = с;
               
            }
        }

        private void Add_Blocks_Load(object sender, EventArgs e)
        {

        }
    }
}
