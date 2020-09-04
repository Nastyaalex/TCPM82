using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using PickBoxTest;
using System.Drawing;
using System.IO;
using project_vniia.Properties;
using System.Text.RegularExpressions;

namespace project_vniia
{
    public partial class Form1 : Form
    {

        DataSet ds = new DataSet();

        public int i;
        public bool flag_filtr = false;
        public static bool flag_filtr_1 = false;
        public static string[] cmdText = new string[13] { "SELECT * FROM [CANNote] ORDER BY Номер_КАН ASC",
        "SELECT * FROM [БлокиМетро]","SELECT * FROM [Замечания по БД]","SELECT * FROM [КАН]",
        "SELECT * FROM [КАНы]","SELECT * FROM [ОперацииМетро]","SELECT * FROM [Проверка]",
            "SELECT * FROM [Проверка ФЭУ]","SELECT * FROM [ПроверкаТСРМ61]","SELECT * FROM [Работы по БД]",
        "SELECT * FROM [Системы в сборе]","SELECT * FROM [Термокалибровка] ORDER BY Номер_БД ASC",
        "SELECT * FROM Блоки ORDER BY [Номер БД] ASC"};// если понадобиться порядок по определённому столбцу

        //public static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "change_2_rows.txt");
        //public static string filePath_calibr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "calibration_check.txt");
        
        Button button_filtr = new Button();

        //
        // Create an instance of the PickBox class
        //
        private PickBox pb = new PickBox();

        public static string conString;// = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\nasty\\Desktop\\_TCPM82_New.mdb";

        public static string[] F2 = new string[9];

        public static string Log_ways;
        public static string Log_ways_peremesti;
        public static string Zamech_ways;
        public static string Zamech_ways_peremesti;
        /// <summary>
        /// /дописать к проверке запись пути к папкам
        /// </summary>
        public static string Proverka_ways;
        public static string Proverka_ways_perem;
        public static string Protocol_ways;
        public static string Protocol_saved;

        public static bool Flags = false;
        public static bool Flags_ = false;
        public static bool Flags_1 = false;

        public static string[] _ways_=new string[8] {"\\log_ways.txt", "\\log_peremesti.txt", "\\zamech_ways.txt", "\\zamech_peremesti.txt", "\\prov_ways.txt", "\\prov_peremesti.txt", "\\protocol_ways.txt", "\\protocol_saved.txt"};
        
        public Form1()
        {

            Form4_splash.ShowSplashScreen();
            InitializeComponent();

             
            #region
            //this.KeyPreview = true;
            ////////////////////
            ////"change_2_rows.txt"///"calibration_check.txt"
            /////////////////
            //try
            //{
            //    if (!File.Exists(filePath))
            //    {
            //        File.Create(filePath).Close();
            //    }
            //    if (!File.Exists(filePath_calibr))
            //    {
            //        File.Create(filePath_calibr).Close();
            //    }
            //}
            //catch(Exception k)
            //{ MessageBox.Show(k.ToString()); }
            #endregion
            for (int t = 0; t < Controls.Count; t++)
            {
                if (Controls[t].Name == "dataGridView2" || Controls[t].Name == "dataGridView1" || Controls[t].Name == "checkBox1")
                {
                    Control c = this.Controls[t];
                    pb.WireControl(c);
                }
            }

            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(DataGridView1_DataError);
            dataGridView2.DataError += new DataGridViewDataErrorEventHandler(DataGridView2_DataError);
            dataGridView2.RowPrePaint += DataGridView2_RowPrePaint;
            dataGridView1.RowPrePaint += DataGridView1_RowPrePaint;
            textBox1.KeyUp += TextBox1_KeyUp;

            textBox2.KeyUp += TextBox2_KeyUp;
            
            //резервное копирование
            //File.Copy(openFileDialog1.FileName, "C:\\Users\\APM\\Desktop\\2.mdb", true);

            dataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            dataGridView2.EditingControlShowing += DataGridView2_EditingControlShowing;
            dataGridView1.UserDeletedRow += DataGridView1_UserDeletedRow;
            dataGridView2.UserDeletedRow += DataGridView2_UserDeletedRow;
            dataGridView1.UserDeletingRow += DataGridView1_UserDeletingRow;
            dataGridView2.UserDeletingRow += DataGridView2_UserDeletingRow;
            this.FormClosing += Form1_FormClosing;
            var rre = menuStrip1.ClientRectangle;
            var verh = menuStrip1.Top;
            var nignaagran = textBox1.ClientRectangle;
            var verh_text = textBox1.Top;
            try
            {
                string loc = Properties.Settings.Default.Table1_loc;
                
                var rast_between_table = Convert.ToInt32(loc);

                var gloc = (verh + rre.Height + 25).ToString();

                var w_1 = Properties.Settings.Default.Table1_w;
                if (Convert.ToInt32(w_1) > this.Width)
                {
                    this.Width = Convert.ToInt32(w_1);
                }
                loc = Properties.Settings.Default.Table1_h;

                loc = (verh_text - 70).ToString();

                dataGridView1.Width = Convert.ToInt32(w_1);
                dataGridView1.Height = Convert.ToInt32(loc);
                //////////////////////////////
                string loc2 = Properties.Settings.Default.Table2_loc;
                var locc2 = loc2.Split(',');
                string tloc2 = Regex.Match(locc2[0], @"\d+").Value;
                string gloc2 = Regex.Match(locc2[1], @"\d+").Value;
                var rrr = dataGridView1.Right + 10;//rast_between_table;
                if (tloc2 != rrr.ToString())
                {
                    tloc2 = rrr.ToString();
                }
                
                if (gloc2 != gloc)
                {
                    gloc2 = gloc;
                }
                dataGridView2.Location = new Point(Convert.ToInt32(dataGridView1.Right+10), Convert.ToInt32(gloc2));
                loc2 = Properties.Settings.Default.Table2_w;
                tloc2 = Properties.Settings.Default.Table2_h;
                
                    this.Width = Convert.ToInt32(loc2) + dataGridView1.Right + 10 + 50;//rast_between_table
                
                tloc2 = (verh_text - 70).ToString();

                dataGridView2.Width = Convert.ToInt32(loc2);
                dataGridView2.Height = Convert.ToInt32(tloc2);
                ///////////////////
                string ch = Properties.Settings.Default.Galka;
                if (ch == "True")
                {
                    checkBox1.Checked = true;
                }
            }
            catch (Exception p)
            { }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                var rast = -dataGridView1.Right + dataGridView2.Left;
                Properties.Settings.Default.Table1_loc = rast.ToString();
                Properties.Settings.Default.Table1_w = dataGridView1.Width.ToString();
                Properties.Settings.Default.Table1_h = dataGridView1.Height.ToString();
                Properties.Settings.Default.Table2_loc = dataGridView2.Location.ToString();
                Properties.Settings.Default.Table2_w = dataGridView2.Width.ToString();
                Properties.Settings.Default.Table2_h = dataGridView2.Height.ToString();
                Properties.Settings.Default.Galka = checkBox1.Checked.ToString();
                Properties.Settings.Default.Save();
            }
            catch(Exception p)
            { MessageBox.Show(p.ToString()); }
        }

        private void DataGridView2_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (flag_filtr)
            {
                try
                {
                    int number = 0;
                    if (comboBox1.Text == "БлокиМетро")
                    {
                        number = myDBs["[" + comboBox1.Text + "]"].table.Columns["Номер блока"].Ordinal;
                    }
                    else
                    {
                        char numb = myDBs["[" + comboBox1.Text + "]"].table.Columns.Contains("Номер записи") ? 'a' :
                            myDBs["[" + comboBox1.Text + "]"].table.Columns.Contains("Номер Замечания") ? 'b' :
                             myDBs["[" + comboBox1.Text + "]"].table.Columns.Contains("Номер Записи") ? 'c' :
                             'd';
                        switch (numb)
                        {
                            case 'a':
                                number = myDBs["[" + comboBox1.Text + "]"].table.Columns["Номер записи"].Ordinal;
                                break;
                            case 'b':
                                number = myDBs["[" + comboBox1.Text + "]"].table.Columns["Номер Замечания"].Ordinal;
                                break;
                            case 'c':
                                number = myDBs["[" + comboBox1.Text + "]"].table.Columns["Номер Записи"].Ordinal;
                                break;
                        }
                    }
                    int ggg = dataGridView2.CurrentCell.RowIndex;
                    string kok = dataGridView2.Rows[ggg].Cells[number].Value.ToString();

                    for (int i = 0; i < myDBs["[" + comboBox1.Text + "]"].table.Rows.Count; i++)
                    {
                        DataRow t_ = myDBs["[" + comboBox1.Text + "]"].table.Rows[i];
                        if (kok == t_[number].ToString())
                        {
                            myDBs["[" + comboBox1.Text + "]"].table.Rows[i].Delete();
                            myDBs["[" + comboBox1.Text + "]"].table.AcceptChanges();
                            myDBs["[" + comboBox1.Text + "]"].adapter.Update(myDBs["[" + comboBox1.Text + "]"].table);
                            break;
                        }

                    }
                }
                catch (Exception p)
                {
                    //MessageBox.Show(p.ToString());
                }
            }
        }

        private void DataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (flag_filtr)
            {
                try
                {
                    int ggg = dataGridView1.CurrentCell.RowIndex;
                    string kok = dataGridView1.Rows[ggg].Cells[0].Value.ToString();

                    for (int i=0;i< myDBs["[Блоки]"].table.Rows.Count; i++)
                    {
                        DataRow t_ = myDBs["[Блоки]"].table.Rows[i];
                        if (kok == t_[0].ToString())
                        {
                            myDBs["[Блоки]"].table.Rows[i].Delete();
                            myDBs["[Блоки]"].table.AcceptChanges();
                            myDBs["[Блоки]"].adapter.Update(myDBs["[Блоки]"].table);
                            break;
                        }

                    }
                }
                catch (Exception p)
                {
                    //MessageBox.Show(p.ToString());
                }
            }
        }

        private void DataGridView2_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (!flag_filtr)
            {
                myDBs["[" + comboBox1.Text + "]"].table.AcceptChanges();
                myDBs["[" + comboBox1.Text + "]"].adapter.Update(myDBs["[" + comboBox1.Text + "]"].table);
            }
        }

        private void DataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (!flag_filtr)
            {
                myDBs["[Блоки]"].table.AcceptChanges();
                myDBs["[Блоки]"].adapter.Update(myDBs["[Блоки]"].table);
            }
        }

        private void DataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tbb = e.Control as TextBox;
            if (tbb != null)
            {
                tbb.TextChanged += new EventHandler(Tbb_TextChanged);
            }
        }

        private void Tbb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox dg = (TextBox)sender;
                string text_ = dg.Text;

                int ggg = dataGridView2.CurrentCell.RowIndex;
                int ggg_ = dataGridView2.CurrentCell.ColumnIndex;
                int number = 0;
                if (comboBox1.Text == "БлокиМетро")
                {
                    number = myDBs["[" + comboBox1.Text + "]"].table.Columns["Номер блока"].Ordinal;
                }
                else
                {
                    char numb = myDBs["[" + comboBox1.Text + "]"].table.Columns.Contains("Номер записи") ? 'a' :
                        myDBs["[" + comboBox1.Text + "]"].table.Columns.Contains("Номер Замечания") ? 'b' :
                         myDBs["[" + comboBox1.Text + "]"].table.Columns.Contains("Номер Записи") ? 'c' :
                         'd';
                    switch (numb)
                    {
                        case 'a':
                            number = myDBs["[" + comboBox1.Text + "]"].table.Columns["Номер записи"].Ordinal;
                            break;
                        case 'b':
                            number = myDBs["[" + comboBox1.Text + "]"].table.Columns["Номер Замечания"].Ordinal;
                            break;
                        case 'c':
                            number = myDBs["[" + comboBox1.Text + "]"].table.Columns["Номер Записи"].Ordinal;
                            break;
                    }
                }
                int k = 0;
                if (myDBs["[" + comboBox1.Text + "]"].table.Columns.Contains("s_ColLineage") == true)
                    k++; 
                if (myDBs["[" + comboBox1.Text + "]"].table.Columns.Contains("s_Generation") == true)
                    k++; 
                if (myDBs["[" + comboBox1.Text + "]"].table.Columns.Contains("s_GUID") == true)
                    k++; 
                if (myDBs["[" + comboBox1.Text + "]"].table.Columns.Contains("s_Lineage") == true)
                    k++;
                foreach (DataRow t_ in myDBs["[" + comboBox1.Text + "]"].table.Rows)
                {
                    string kok = dataGridView2.Rows[ggg].Cells[number].Value.ToString();
                    if (kok == t_[number].ToString())
                    {
                        for (int tt = 0; tt < myDBs["[" + comboBox1.Text + "]"].table.Columns.Count - k; tt++)
                        {
                            if (tt == ggg_)
                            {
                                t_[tt] = text_;
                            }
                            //else
                            //    t_[tt] = dataGridView2.Rows[ggg].Cells[tt].Value;
                        }

                        break;
                    }

                }
            }
            catch (Exception p)
            {
                //MessageBox.Show(p.ToString());
            }
        }

        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = e.Control as TextBox;
            if(tb !=null)
            {
                tb.TextChanged += new EventHandler(tb_TextChanged);
            }
        }
        //не работает для удаления строки- только для ячеек

        private void tb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox dg = (TextBox)sender;
                string text_ = dg.Text;

                int ggg = dataGridView1.CurrentCell.RowIndex;
                int ggg_ = dataGridView1.CurrentCell.ColumnIndex;
                foreach (DataRow t_ in myDBs["[Блоки]"].table.Rows)
                {
                    string kok = dataGridView1.Rows[ggg].Cells[0].Value.ToString();
                    if (kok == t_[0].ToString())
                    {
                        for (int tt = 1; tt < myDBs["[Блоки]"].table.Columns.Count - 4; tt++)
                        {
                            if (tt == ggg_)
                            {
                                t_[tt] = text_;
                            }
                            else
                                t_[tt] = dataGridView1.Rows[ggg].Cells[tt].Value;
                        }

                        break;
                    }

                }
            }
            catch(Exception p)
            {
                //MessageBox.Show(p.ToString());
            }
        }

        private void TextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            Filtr_2.Tabl2(myDBs, comboBox1, textBox1,textBox2, dataGridView1, dataGridView2);
            if(Filtr_2.Bbb == true)
            {
                TextBox1_KeyUp(sender, e);
                Filtr_2.Bbb = false;
            }
            if (Filtr_2.Ccc == true)
            {
                TextBox1_KeyUp(sender, e);
                Filtr_2.Ccc = false;
            }
            if (textBox2.Text=="")
                Filtr_2.Filtr2 = false;
            Datagrid_columns_delete();
            Datagrid_columns_delete_blocks();
        }

        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            button_filtr_Click(myDBs, comboBox1, textBox1, textBox2);
            Datagrid_columns_delete();
            Datagrid_columns_delete_blocks();
            if (Filtr_2.Filtr2 == true && Filtr_2.Ccc == false)
            { TextBox2_KeyUp(sender, e); }
        }


        private void DataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridView1.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                this.dataGridView1.Rows[index].HeaderCell.Value = indexStr;
        }

        private void DataGridView2_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridView2.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                this.dataGridView2.Rows[index].HeaderCell.Value = indexStr;
        }


        private void DataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show("Ошибка");
            e.ThrowException = false;
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            
            do
            {
                conString = Class_zagruz.Try_(conString, openFileDialog1);
            }
            while (conString == null);

            bool[] flag_sysh = new bool[8];
            
            for (int i = 0; i < 8; i++)
            {
                bool pusto = Class_ways.Pusto_(_ways_[i]);
                flag_sysh[i] = Class_ways.Log_pusto(_ways_[i], pusto);
            }
            int k_tr=0;
            
            foreach (bool f in flag_sysh)
            {
                if (f) { k_tr++; }
            }
            try
            {
                if (k_tr != 8 && k_tr < 8)
                {
                    do
                    {
                        F2 = Class_ways.Forma2_();
                        if (Form2.close_all)
                        {
                            Environment.Exit(0);
                        }
                    } while (Array.Exists(F2, element => element == "") || Array.Exists(F2, element => element == null));
                }
            }
            catch (Exception p)
            {
                Console.WriteLine(p.Message);
            }

            Class_ways.Zap_(_ways_, F2, k_tr);
            //
            //ready and work
            try
            {
                Calibr calibr = new Calibr();
                calibr.Main_calibr(this);
            }
            catch(Exception p)
            { MessageBox.Show("Ошибка! Файл НЕ для добавления данных в таблицу Термокалибровка, переместите в другую папку!"); }
            try
            {
                Zamech_BD zamech_BD = new Zamech_BD();
                zamech_BD.Main_Zamech_BD(this);
            }
            catch (Exception p)
            { MessageBox.Show("Ошибка! Файл НЕ для добавления данных в таблицу Замечания по БД, переместите в другую папку!");
            }
            
            Proverka proverka = new Proverka();
            //
            MyDB myDB = new MyDB();

            Class_zagruz.Combobox_(conString, comboBox1, ds, myDB, myDBs);

            Form_System _System = new Form_System();

            proverka.Main_Proverka(this, myDBs["[Проверка]"].table, myDBs["[Блоки]"].table, myDBs["[Системы в сборе]"].table);
            //try
            //{
            //    if (Proverka.parts.Length == 1)
            //    {
            //        MessageBox.Show("Введите номера блоков входящих в систему:" + Proverka.parts[0] + "  в таблице Проверка. Вместо: ?дополнить.");
            //    }
            //    else
            //    {
            //        string yy = "";
            //        for (int y = 0; y < Proverka.parts.Length; y++)
            //        { yy = yy + "_" + Proverka.parts[y]; }
            //        MessageBox.Show("Введите номер системы для блоков:" + yy + "  в таблице Проверка. Вместо: ?дополнить.");

            //    }
            //}
            //catch (Exception h)
            //{ Console.WriteLine(h.Message); }
            try
            {
                foreach (var my in comboBox1.Items)
                {
                    myDBs["[" + my + "]"].table.Clear();
                }
                myDBs["[Блоки]"].table.Clear();
            }
            catch(Exception p)
            { MessageBox.Show(p.ToString()); }

            Class_zagruz.Combobox_(conString, comboBox1, ds, myDB, myDBs);

            dataGridView1.DataSource = myDBs["[Блоки]"].table.DefaultView;
            dataGridView1.Columns["Номер БД"].ReadOnly = true;

            Datagrid_columns_delete_blocks();
            Datagrid_columns_delete();

            Form4_splash.CloseForm();
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        string[] stolbez = new string[5] { "Номер блока", "Номер КАН", "Номер БД", "Номер изделия", "Номер системы"};

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = myDBs["[" + comboBox1.Text + "]"].table.DefaultView;
            if (comboBox1.Text != "Проверка")
            {
                for (i = 0; i < 5; i++)
                {
                    if (dataGridView2.Columns.Contains(stolbez[i]))
                    {
                        dataGridView2.Columns[stolbez[i]].ReadOnly = true;
                        break;
                    }
                }
            }
            Datagrid_columns_delete();
            if (flag_filtr)
            {
                button_filtr_Click(myDBs, comboBox1, textBox1, textBox2);
            }
            if(flag_filtr_1)
            {
                Filtr_2.Tabl2(myDBs, comboBox1, textBox1, textBox2, dataGridView1, dataGridView2);
                Datagrid_columns_delete();
                Datagrid_columns_delete_blocks();
                if (Filtr_2.Ccc == true)
                {
                    button_filtr_Click(myDBs, comboBox1, textBox1, textBox2);
                    Filtr_2.Ccc = false;
                }
                if (textBox2.Text == "")
                    Filtr_2.Filtr2 = false;
            }

        }
        public void Datagrid_columns_delete()
        {
            if (dataGridView2.Columns.Contains("s_ColLineage") == true)
                dataGridView2.Columns.Remove("s_ColLineage");
            if (dataGridView2.Columns.Contains("s_Generation") == true)
                dataGridView2.Columns.Remove("s_Generation");
            if (dataGridView2.Columns.Contains("s_GUID") == true)
                dataGridView2.Columns.Remove("s_GUID");
            if (dataGridView2.Columns.Contains("s_Lineage") == true)
                dataGridView2.Columns.Remove("s_Lineage");

        }
        public void Datagrid_columns_delete_blocks()
        {
            if (dataGridView1.Columns.Contains("s_ColLineage") == true)
                dataGridView1.Columns.Remove("s_ColLineage");
            if (dataGridView1.Columns.Contains("s_Generation") == true)
                dataGridView1.Columns.Remove("s_Generation");
            if (dataGridView1.Columns.Contains("s_GUID") == true)
                dataGridView1.Columns.Remove("s_GUID");
            if (dataGridView1.Columns.Contains("s_Lineage") == true)
                dataGridView1.Columns.Remove("s_Lineage");
        }

        
        public Add_Blocks CreateForm()
        {
            // Проверяем существование формы
            foreach (Form frm in Application.OpenForms)
                if (frm is Add_Blocks)
                {
                    frm.Activate();
                    return frm as Add_Blocks;
                }
            // Создаем новую форму
           
            Add_Blocks add_ = new Add_Blocks();
            add_.blocks_T = myDBs["[Блоки]"].table;
            add_.zamech_T = myDBs["[Замечания по БД]"].table;
            add_.peregr = this.but_peregruzka;
            add_.Show();
            return add_;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public string text;
        public void filtr(Dictionary<string, MyDB> myDBs, TextBox textBox1, TextBox textBox2)
        {
            if (Filtr_2.Ccc == true)
            { text = textBox2.Text; }
            else text = textBox1.Text;


            if (text == "")
            {
                dataGridView1.DataSource = myDBs["[Блоки]"].table;
            }
            else
            {
                var table1 = myDBs["[Блоки]"].table;
                int k = 0; bool s_ = false;
                if (table1.Columns.Contains("s_ColLineage") == true)
                    k++; //table1.Columns.Remove("s_ColLineage");
                if (table1.Columns.Contains("s_Generation") == true)
                    k++; // table1.Columns.Remove("s_Generation");
                if (table1.Columns.Contains("s_GUID") == true)
                    k++; // table1.Columns.Remove("s_GUID");
                if (table1.Columns.Contains("s_Lineage") == true)
                    k++;// table1.Columns.Remove("s_Lineage");
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

                dataGridView1.DataSource = table2;

                Datagrid_columns_delete_blocks();

            }
        }
        public void button_filtr_Click(Dictionary<string, MyDB> myDBs, ComboBox comboBox1, TextBox textBox1, TextBox textBox2)
        {
            if (Filtr_2.Filtr2 == false || Filtr_2.Ccc == true)
            { 
                filtr(myDBs, textBox1, textBox2); // for 1 table
                if (Filtr_2.Ccc == true)
                { text = textBox2.Text; }
                else text = textBox1.Text;

                if (text == "")
                {
                    dataGridView2.DataSource = myDBs["[" + comboBox1.Text + "]"].table;

                    Datagrid_columns_delete();
                    Datagrid_columns_delete_blocks();
                }
                else
                {
                    var table1 = myDBs["[" + comboBox1.Text + "]"].table;
                    int k = 0; bool s_ = false;
                    if (table1.Columns.Contains("s_ColLineage") == true)
                        k++; //table1.Columns.Remove("s_ColLineage");
                    if (table1.Columns.Contains("s_Generation") == true)
                        k++; // table1.Columns.Remove("s_Generation");
                    if (table1.Columns.Contains("s_GUID") == true)
                        k++; // table1.Columns.Remove("s_GUID");
                    if (table1.Columns.Contains("s_Lineage") == true)
                        k++;// table1.Columns.Remove("s_Lineage");
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
                                //Console.Write (c.ToString() + " "); // для проверки
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

                    dataGridView2.DataSource = table2;

                    Datagrid_columns_delete();
                    flag_filtr = true;

                }
            }
        }
        
        private void добавитьБлокиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateForm();
        }
        public class MyDB
        {
            public DataSet ds;
            public OleDbDataAdapter adapter;
            public DataTable table;
        }
        private Dictionary<string, MyEnd> myEnds = new Dictionary<string, MyEnd>();
        public class MyEnd
        {
            public int del;
            public int izm;
            public int dob;
        }
        private Dictionary<string, MyDB> myDBs = new Dictionary<string, MyDB>();

        private void but_peregruzka_Click(object sender, EventArgs e)
        {   //работает- для замены строк
            dataGridView1.DataSource = myDBs["[Блоки]"].table.DefaultView;
            Datagrid_columns_delete_blocks();
        }
        
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        public Form_cod CreateForm_zamena()
        {
            // Проверяем существование формы
            foreach (Form frm in Application.OpenForms)
                if (frm is Form_cod)
                {
                    frm.Activate();
                    return frm as Form_cod;
                }
            // Создаем новую форму
            Form_cod cod = new Form_cod();
            cod.dataTables[0] = myDBs["[Блоки]"].table;
            i = 1;
            foreach (string str in comboBox1.Items)
            {
                cod.dataTables[i] = myDBs["[" + str + "]"].table;
                i++;
            }
            cod.Show();

            return cod;
        }
        
        private void поменятьСтрокиМестамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateForm_zamena();
        }
        
        public static bool zam = false;
        private void заменитьНомерБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zam = true;
            CreateForm_zamena();
        }

        private void сложнаяФильтрацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox2.Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public Otchet CreateForm_otchet()
        {
            // Проверяем существование формы
            foreach (Form frm in Application.OpenForms)
                if (frm is Otchet)
                {
                    frm.Activate();
                    return frm as Otchet;
                }
            // Создаем новую форму
            Otchet otchet = new Otchet();
            otchet.myDBs = myDBs;
            
            otchet.Show();

            return otchet;
        }
        private void отчётПоТипамБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateForm_otchet();
        }

        public Form_System CreateForm_Form_System()
        {
            // Проверяем существование формы
            foreach (Form frm in Application.OpenForms)
                if (frm is Form_System)
                {
                    frm.Activate();
                    return frm as Form_System;
                }
            // Создаем новую форму
            Form_System system = new Form_System();
            
            if (Form_way_system.close_all1 == true)
            {
                return null;
            }
            else
            {
                system.myDBs = myDBs;
                system.Show();

                return system;
            }
        }
        private void сборСистемыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateForm_Form_System();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("При сложной фильтрации приоритет у значения из первой ячейки.\n\t");
            
        }

        public Form4_func_new CreateForm_Form4()
        {
            // Проверяем существование формы
            foreach (Form frm in Application.OpenForms)
                if (frm is Form4_func_new)
                {
                    frm.Activate();
                    return frm as Form4_func_new;
                }
            // Создаем новую форму
            Form4_func_new system = new Form4_func_new();
            
                system.myDBs = myDBs;
                system.Show();

                return system;
        }

        private void переместитьМеждуПодразделениямиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateForm_Form4();
        }
        bool k = false;
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                for (int i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i].Name == "dataGridView2"|| Controls[i].Name == "dataGridView1")
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
                    if (Controls[i].Name == "dataGridView2" || Controls[i].Name == "dataGridView1")
                    {
                        Control c = Controls[i];
                        pb.WireControl(c);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreateForm_Form_way();
        }
        public Form_ways_change CreateForm_Form_way()
        {
            // Проверяем существование формы
            foreach (Form frm in Application.OpenForms)
                if (frm is Form_ways_change)
                {
                    frm.Activate();
                    return frm as Form_ways_change;
                }
            // Создаем новую форму
            Form_ways_change system = new Form_ways_change();
            
            system.Show();

            return system;
        }

        private void сохранитьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            //сохранение для таблицы(авто)

            ds = new DataSet();
            myDBs["[Блоки]"].adapter.Fill(ds);
            myDBs["[Блоки]"].ds = ds;

            Class_Save_blocks.AnalizTable(myDBs["[Блоки]"].ds.Tables[0], myDBs["[Блоки]"].table, myDBs["[Блоки]"].adapter, myEnds);

            foreach (string str in comboBox1.Items)
            {
                ds = new DataSet();
                myDBs["[" + str + "]"].adapter.Fill(ds);
                myDBs["[" + str + "]"].ds = ds;
            }

            #region SAVE
            Class_Save_cannote.AnalizTable(myDBs["[CANNote]"].ds.Tables[0], myDBs["[CANNote]"].table, myDBs["[CANNote]"].adapter, myEnds);

            Class_Save_blockMetro.AnalizTable(myDBs["[БлокиМетро]"].ds.Tables[0], myDBs["[БлокиМетро]"].table, myDBs["[БлокиМетро]"].adapter, myEnds);

            Class_Save_kan.AnalizTable(myDBs["[КАН]"].ds.Tables[0], myDBs["[КАН]"].table, myDBs["[КАН]"].adapter, myEnds);

            Class_Save_kanS.AnalizTable(myDBs["[КАНы]"].ds.Tables[0], myDBs["[КАНы]"].table, myDBs["[КАНы]"].adapter, myEnds);

            Class_Save_operMetro.AnalizTable(myDBs["[ОперацииМетро]"].ds.Tables[0], myDBs["[ОперацииМетро]"].table, myDBs["[ОперацииМетро]"].adapter, myEnds);

            Class_Save_prov.AnalizTable(myDBs["[Проверка]"].ds.Tables[0], myDBs["[Проверка]"].table, myDBs["[Проверка]"].adapter, myEnds);

            Class_Save_provFey.AnalizTable(myDBs["[Проверка ФЭУ]"].ds.Tables[0], myDBs["[Проверка ФЭУ]"].table, myDBs["[Проверка ФЭУ]"].adapter, myEnds);

            Class_Save_provTCPM.AnalizTable(myDBs["[ПроверкаТСРМ61]"].ds.Tables[0], myDBs["[ПроверкаТСРМ61]"].table, myDBs["[ПроверкаТСРМ61]"].adapter, myEnds);

            Class_Save_rabotBD.AnalizTable(myDBs["[Работы по БД]"].ds.Tables[0], myDBs["[Работы по БД]"].table, myDBs["[Работы по БД]"].adapter, myEnds);

            Class_Save_systemVsbore.AnalizTable(myDBs["[Системы в сборе]"].ds.Tables[0], myDBs["[Системы в сборе]"].table, myDBs["[Системы в сборе]"].adapter, myEnds);

            Class_Save_termocalibr.AnalizTable(myDBs["[Термокалибровка]"].ds.Tables[0], myDBs["[Термокалибровка]"].table, myDBs["[Термокалибровка]"].adapter, myEnds);

            Class_Save_zamechPoBD.AnalizTable(myDBs["[Замечания по БД]"].ds.Tables[0], myDBs["[Замечания по БД]"].table, myDBs["[Замечания по БД]"].adapter, myEnds);

            #endregion
            string ends = "\r\a";
            foreach (var end in myEnds)
            {
                ends = ends + end.Key + "       добавлено:"+ end.Value.dob +"       изменено:"+ end.Value.izm + "       удалено:" + end.Value.del+"\r\a";
            }
            var kolvo = ends.Split(' ');
            kolvo[kolvo.Length-1] = kolvo[kolvo.Length-1].Replace("\r\a","");
            ends = "";
            foreach (var en in kolvo)
            {
                if(en=="")
                    ends = ends + " ";
                ends = ends + en;
            }
            MessageBox.Show(ends);
        }

        private void протоколToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateForm_Form_protokol();
        }
        public Form_protokol CreateForm_Form_protokol()
        {
            // Проверяем существование формы
            foreach (Form frm in Application.OpenForms)
                if (frm is Form_protokol)
                {
                    frm.Activate();
                    return frm as Form_protokol;
                }
            // Создаем новую форму
            Form_protokol system = new Form_protokol();
            system.myDBs = myDBs;
            system.Show();

            return system;
        }
    }
    
}
