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
    public partial class Form4_func_new : Form
    {
        DataSet ds = new DataSet();
        public int t1;
        public Form4_func_new()
        {
            InitializeComponent();
            dataGridViewLeft.DataError += DataGridViewLeft_DataError;
            dataGridViewRight.DataError += DataGridViewRight_DataError;
            
        }

        private void DataGridViewRight_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void DataGridViewLeft_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = dataGridViewLeft.RowCount - 1; i >= 0; i--)
            {
                DataGridViewRow row = dataGridViewLeft.Rows[i];
                if (Convert.ToBoolean(row.Cells["Выбрать"].Value))
                {
                    DataRow new_ = ds.Tables[comboBox2.Text].NewRow();
                    ds.Tables[comboBox1.Text].Rows[i][t1] = "п." + comboBox2.Text;
                    new_ = ds.Tables[comboBox1.Text].Rows[i];
                    ds.Tables[comboBox2.Text].ImportRow(new_);
                    foreach (DataRow row_ in myDBs["[Блоки]"].table.Rows)
                    {
                        if (row_["Номер БД"] == new_["Номер БД"])
                        {
                            string value = row_.Field<string>("Местоположение").Replace(comboBox1.Text, comboBox2.Text);

                            // Then we update the value.
                            row_.SetField("Местоположение", value);
                        }
                    }
                    ds.Tables[comboBox1.Text].Rows.Remove(new_);
                }
            }

        }
        public Dictionary<string, Form1.MyDB> myDBs;

        public void Delen(Dictionary<string, Form1.MyDB> myDBs)
        {
            var table1 = myDBs["[Блоки]"].table.Copy();
            
            var rows_Location_obj = new List<Object>();

            var rows = table1.Rows;
            t1 = 0;
            int kolvo = 5;
            
            if (table1.Columns.Contains("Местоположение"))
            {
                t1 = table1.Columns["Местоположение"].Ordinal;
            }
            DataTable table = new DataTable();
            foreach (DataRow r in rows)
            {

                string a = r[t1].ToString();
                int value;
                int.TryParse(string.Join("", a.Where(c => char.IsDigit(c))), out value);

                if (!rows_Location_obj.Contains(value) && !r[t1].ToString().Contains("сдан") && !r[t1].ToString().Contains("Сдан"))
                {
                    bool vvv = false;
                    foreach(var loc in rows_Location_obj)
                    {
                        string v =value.ToString();
                        if (v.Contains(loc.ToString()))
                        {
                            vvv = true;
                            break;
                        }
                    }
                    if (!vvv)
                    {
                        kolvo++;
                        rows_Location_obj.Add(value);
                        table = table1.Clone();
                        table.TableName = value.ToString();
                        table.BeginLoadData();
                        ds.Tables.AddRange(new DataTable[] { table.Copy() });
                    }
                }

            }
           
            Razsortirovka(table1, ds, t1, rows_Location_obj);
            foreach (var obj in rows_Location_obj)
            {
                comboBox1.Items.Add(obj);
                comboBox2.Items.Add(obj);
            }
            comboBox2.SelectedItem = comboBox2.Items[0];
            comboBox1.SelectedItem = comboBox1.Items[0];
        }
        public void Razsortirovka(DataTable table, DataSet ds, int t1, List<Object> rows_Location_obj)
        {
            var rows = table.Rows;
            foreach (DataRow r in rows)
            {
                var rt1 = r[t1].ToString();
                int k = 0;
                foreach (var p in rows_Location_obj)
                {
                    var pp = p.ToString();
                    if (rt1.Contains(pp) || (rt1 == "" && pp == "0"))
                    {
                        ds.Tables[k].LoadDataRow(r.ItemArray, true);
                        break;
                    }
                    k++;
                }
            }
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            for (int i = dataGridViewRight.RowCount - 1; i >= 0; i--)
            {
                DataGridViewRow row = dataGridViewRight.Rows[i];
                if (Convert.ToBoolean(row.Cells["Выбрать"].Value))
                {
                    DataRow new_ = ds.Tables[comboBox1.Text].NewRow();
                    ds.Tables[comboBox2.Text].Rows[i][t1] = "п." + comboBox1.Text;
                    new_ = ds.Tables[comboBox2.Text].Rows[i];
                    ds.Tables[comboBox1.Text].ImportRow(new_);
                    
                    foreach (DataRow row_ in myDBs["[Блоки]"].table.Rows)
                    {
                        if (row_["Номер БД"] == new_["Номер БД"])
                        {
                            string value = row_.Field<string>("Местоположение").Replace(comboBox2.Text, comboBox1.Text);

                            // Then we update the value.
                            row_.SetField("Местоположение", value);
                        }
                    }
                    ds.Tables[comboBox2.Text].Rows.Remove(new_);
                }
            }
        }

        private void Form4_func_new_Load(object sender, EventArgs e)
        {
            Delen(myDBs);
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string comb = comboBox1.Text;
                if (!dataGridViewLeft.Columns.Contains("Выбрать"))
                {
                    DataGridViewCheckBoxColumn dataColumn = new DataGridViewCheckBoxColumn();
                    dataColumn.Name = "Выбрать";
                    dataColumn.DefaultCellStyle = null;
                    dataGridViewLeft.Columns.Add(dataColumn);
                }
                dataGridViewLeft.DataSource = ds.Tables[comb].DefaultView;
                
            }
            catch (Exception p)
            { MessageBox.Show(p.ToString()); }
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string comb = comboBox2.Text;
                if (!dataGridViewRight.Columns.Contains("Выбрать"))
                {
                    DataGridViewCheckBoxColumn dataColumn = new DataGridViewCheckBoxColumn();
                    dataColumn.Name = "Выбрать";
                    dataColumn.DefaultCellStyle = null;
                    dataGridViewRight.Columns.Add(dataColumn);
                }
                dataGridViewRight.DataSource = ds.Tables[comb].DefaultView;

            }
            catch (Exception p)
            { MessageBox.Show(p.ToString()); }
        }
        public void AnalizTable(DataTable First, DataTable Second)
        {//сравнение 2-х таблиц
            DataTable table = new DataTable("Различия");
            DataTable table1 = new DataTable("Различия1");
            DataTable table_up = new DataTable("UPDATE");

            using (DataSet ds = new DataSet())
            {
                //Добавление таблиц в DS
                ds.Tables.AddRange(new DataTable[] { First.Copy(), Second.Copy() });

                //Получение столбцов для DataRelation (1-я таблица)
                DataColumn[] firstcolumns = new DataColumn[ds.Tables[0].Columns.Count];
                for (int i = 0; i < firstcolumns.Length; i++)
                {
                    firstcolumns[i] = ds.Tables[0].Columns[i];
                }

                //Получение столбцов для DataRelation (2-я таблица)
                DataColumn[] secondcolumns = new DataColumn[ds.Tables[1].Columns.Count];
                for (int i = 0; i < secondcolumns.Length; i++)
                {
                    secondcolumns[i] = ds.Tables[1].Columns[i];
                }

                //Создание DataRelation (отношений)
                DataRelation r1 = new DataRelation(string.Empty, firstcolumns, secondcolumns, false);
                ds.Relations.Add(r1);
                DataRelation r2 = new DataRelation(string.Empty, secondcolumns, firstcolumns, false);
                ds.Relations.Add(r2);

                //Создание столбцов результирующей таблицы
                table = First.Clone();
                table1 = First.Clone();

                table.BeginLoadData();
                table1.BeginLoadData();

                table_up = First.Clone();
                table_up.BeginLoadData();
                //Если строки из 1-й нет во 2-й, то добавляем в результирующую таблицу
                foreach (DataRow parentrow in ds.Tables[0].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r1);
                    if (childrows == null || childrows.Length == 0)
                        table.LoadDataRow(parentrow.ItemArray, true);
                }
                //table.Rows.Add(000, "Akademic", "Iangal");

                //Если строки из 2-й нет в 1-й, то добавляем в результирующую таблицу
                foreach (DataRow parentrow in ds.Tables[1].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r2);
                    if (childrows == null || childrows.Length == 0)
                        table1.LoadDataRow(parentrow.ItemArray, true);
                }

                table.EndLoadData();
                table1.EndLoadData();
            }
           // CompareRows_BLOCKS(table, table1, adapter, table_up);

        }
        private void dataGridViewLeft_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
///сохрвнить в таблицы