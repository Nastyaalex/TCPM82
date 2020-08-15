using PickBoxTest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace project_vniia
{
    public partial class Form4_func_new : Form
    {
        Button button_filtr = new Button();
        DataSet ds = new DataSet();
        private PickBox pb = new PickBox();
        public bool flag_filtr = false;
        public int t1;
        public Form4_func_new()
        {
            InitializeComponent();
            dataGridViewLeft.DataError += DataGridViewLeft_DataError;
            dataGridViewRight.DataError += DataGridViewRight_DataError;
            textBox1.KeyUp += TextBox1_KeyUp;

            for (int t = 0; t < Controls.Count; t++)
            {
                if (Controls[t].Name == "dataGridViewLeft" || Controls[t].Name == "dataGridViewRight" || Controls[t].Name == "checkBox1")
                {
                    Control c = this.Controls[t];
                    pb.WireControl(c);
                }
            }
        }

        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            button_filtr_Click(ds, comboBox1, dataGridViewLeft);
            button_filtr_Click(ds, comboBox2, dataGridViewRight);
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
                    DataRow new_ = ds.Tables[comboBox1.Text].NewRow();

                    string l = row.Cells["Номер БД"].Value.ToString();
                    foreach (DataRow ror in ds.Tables[comboBox1.Text].Rows)
                    {
                        if (ror["Номер БД"].ToString() == l)
                        {
                            string value_ = "п." + comboBox2.Text;
                            ror.SetField("Местоположение", value_);
                            new_ = ror;
                            break;
                        }
                    }
                    
                    ds.Tables[comboBox2.Text].ImportRow(new_);
                    foreach (DataRow row_ in myDBs["[Блоки]"].table.Rows)
                    {
                        if (row_["Номер БД"] == new_["Номер БД"])
                        {
                            if (row_["Местоположение"] == DBNull.Value)
                            {
                                string value_ = "п." + comboBox2.Text;
                                row_.SetField("Местоположение", value_);
                                break;
                            }
                            else
                            {
                                string value = row_.Field<string>("Местоположение").Replace(comboBox1.Text, comboBox2.Text);

                                // Then we update the value.
                                row_.SetField("Местоположение", value);
                                break;
                            }
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
                    string l = row.Cells["Номер БД"].Value.ToString();
                    foreach (DataRow ror in ds.Tables[comboBox2.Text].Rows)
                    {
                        if (ror["Номер БД"] == l)
                        {
                            string value_ = "п." + comboBox1.Text;
                            ror.SetField("Местоположение", value_);
                            new_ = ror;
                            break;
                        }
                    }
                   
                    ds.Tables[comboBox1.Text].ImportRow(new_);
                    
                    foreach (DataRow row_ in myDBs["[Блоки]"].table.Rows)
                    {
                        if (row_["Номер БД"] == new_["Номер БД"])
                        {
                            if (row_["Местоположение"] == DBNull.Value)
                            {
                                string value_ = "п." + comboBox1.Text;
                                row_.SetField("Местоположение", value_);
                                break;
                            }
                            else
                            {
                                string value = row_.Field<string>("Местоположение").Replace(comboBox2.Text, comboBox1.Text);

                                // Then we update the value.
                                row_.SetField("Местоположение", value);
                                break;
                            }
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

            if (flag_filtr)
            {
                button_filtr_Click(ds, comboBox1, dataGridViewLeft);
            }
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

            if (flag_filtr)
            {
                button_filtr_Click(ds, comboBox2, dataGridViewRight);
            }
        }
        
        private void dataGridViewLeft_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void button_filtr_Click(DataSet df, ComboBox comboBox_, DataGridView view)
        {
                string text = textBox1.Text;

                if (text == "")
                {
                    view.DataSource = df.Tables[comboBox_.Text];
                
                }
                else
                {
                    var table1 = df.Tables[comboBox_.Text];
                    int k = 0; bool s_ = false;
                    if (table1.Columns.Contains("s_ColLineage") == true)
                        k++; 
                    if (table1.Columns.Contains("s_Generation") == true)
                        k++; 
                    if (table1.Columns.Contains("s_GUID") == true)
                        k++; 
                    if (table1.Columns.Contains("s_Lineage") == true)
                        k++;
                    var table2 = table1.Copy();
                    if (k != 0)
                        s_ = true;
                    //переписать t1 -> t2 С учетом фильтра

                    var rows_to_delete = new List<DataRow>();

                    var rows = table2.Rows;
                    foreach (DataRow r in rows)
                    {
                        bool f = true;
                        int kolvo = r.ItemArray.Length;
                        k = 1;
                        foreach (var c in r.ItemArray)
                        {
                            if (s_)
                            {
                                if ((k < kolvo) && (k < kolvo - 1) && (k < kolvo - 2) && (k < kolvo - 3))
                                {
                                    if (c.ToString().Contains(text))
                                    {
                                        f = false;
                                    }
                                }
                                else { break; }
                            }
                            else
                            {
                                if (c.ToString().Contains(text))
                                {
                                    f = false;
                                }
                            }
                            k++;
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

                    rows_to_delete.Clear();

                    view.DataSource = table2;
                
                    flag_filtr = true;

                }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                for (int i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i].Name == "dataGridViewLeft" || Controls[i].Name == "dataGridViewRight")
                    {
                        Control c = Controls[i];
                        pb.WireControl1(c);
                    }
                }
            }
            else
            {
                for (int i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i].Name == "dataGridViewLeft" || Controls[i].Name == "dataGridViewRight")
                    {
                        Control c = Controls[i];
                        pb.WireControl(c);
                    }
                }
            }
        }
    }

}
