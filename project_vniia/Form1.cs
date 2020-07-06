using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using PickBoxTest;
using System.Drawing;
using System.IO;
using project_vniia.Properties;

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

        public static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "change_2_rows.txt");
        public static string filePath_calibr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "calibration_check.txt");
        Button button_filtr = new Button();

        //
        // Create an instance of the PickBox class
        //
        private PickBox pb = new PickBox();

        public static string conString;// = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\nasty\\Desktop\\_TCPM82_New.mdb";

        public static string[] F2 = new string[7];

        public static string Log_ways;
        public static string Log_ways_peremesti;
        public static string Zamech_ways;
        public static string Zamech_ways_peremesti;
        /// <summary>
        /// /дописать к проверке запись пути к папкам
        /// </summary>
        public static string Proverka_ways;
        public static string Proverka_ways_perem;

        public static bool Flags = false;
        public static bool Flags_ = false;
        public static bool Flags_1 = false;

        public static string[] _ways_=new string[6] {"\\log_ways.txt", "\\log_peremesti.txt", "\\zamech_ways.txt", "\\zamech_peremesti.txt", "\\prov_ways.txt", "\\prov_peremesti.txt" };
        
        public Form1()
        {
            InitializeComponent();
            //this.KeyPreview = true;

            for (int t = 5; this.Controls[t] != this.Controls[7]; t++)
            {
                Control c = this.Controls[t];
                pb.WireControl(c);
            }

            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(DataGridView1_DataError);
            dataGridView2.DataError += new DataGridViewDataErrorEventHandler(DataGridView2_DataError);
            dataGridView2.RowPrePaint += DataGridView2_RowPrePaint;
            dataGridView1.RowPrePaint += DataGridView1_RowPrePaint;
            textBox1.KeyUp += TextBox1_KeyUp;

            textBox2.KeyUp += TextBox2_KeyUp;
            
            //резервное копирование
            //File.Copy(openFileDialog1.FileName, "C:\\Users\\APM\\Desktop\\2.mdb", true);

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

            bool[] flag_sysh = new bool[6];
            
            for (int i = 0; i < 6; i++)
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
                if (k_tr != 6 && k_tr < 6)
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
            MyDB myDB = new MyDB();

            Class_zagruz.Combobox_(conString, comboBox1, ds, myDB, myDBs);

            dataGridView1.DataSource = myDBs["[Блоки]"].table.DefaultView;
            dataGridView1.Columns["Номер БД"].ReadOnly = true;

            Datagrid_columns_delete_blocks();
            Datagrid_columns_delete();


            //ready and work
            Calibr calibr = new Calibr();
            calibr.Main_calibr(this);

            Zamech_BD zamech_BD = new Zamech_BD();
            zamech_BD.Main_Zamech_BD(this);

            Proverka proverka = new Proverka();
            proverka.Main_Proverka(this, myDBs["[Проверка]"].table);
            try
            {
                if (Proverka.parts.Length == 1)
                {
                    MessageBox.Show("Введите номера блоков входящих в систему:" + Proverka.parts[0] + "  в таблице Проверка. Вместо: ?дополнить.");
                }
                else
                {
                    string yy = "";
                    for (int y = 0; y < Proverka.parts.Length; y++)
                    { yy = yy + "_" + Proverka.parts[y]; }
                    MessageBox.Show("Введите номер системы для блоков:" + yy + "  в таблице Проверка. Вместо: ?дополнить.");

                }
            }
            catch (Exception h)
            { Console.WriteLine(h.Message); }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        string[] stolbez = new string[5] { "Номер блока", "Номер КАН", "Номер БД", "Номер изделия", "Номер системы"};

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = myDBs["[" + comboBox1.Text + "]"].table.DefaultView;
            
            for (i = 0; i < 5; i++)
            {
                if (dataGridView2.Columns.Contains(stolbez[i]))
                {
                    dataGridView2.Columns[stolbez[i]].ReadOnly = true;
                    break;
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
                dataGridView1.DataSource = myDBs["[Блоки]"].table;
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

        private Dictionary<string, MyDB> myDBs = new Dictionary<string, MyDB>();

        private void but_peregruzka_Click(object sender, EventArgs e)
        {//работает- для замены строк
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

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //сохранение для таблицы(авто)

            ds = new DataSet();
            myDBs["[Блоки]"].adapter.Fill(ds);
            myDBs["[Блоки]"].ds = ds;

            Class_Save_blocks.AnalizTable(myDBs["[Блоки]"].ds.Tables[0], myDBs["[Блоки]"].table, myDBs["[Блоки]"].adapter);

            foreach (string str in comboBox1.Items)
            {
                ds = new DataSet();
                myDBs["[" + str + "]"].adapter.Fill(ds);
                myDBs["[" + str + "]"].ds = ds;
            }

            #region SAVE
            Class_Save_cannote.AnalizTable(myDBs["[CANNote]"].ds.Tables[0], myDBs["[CANNote]"].table, myDBs["[CANNote]"].adapter);

            Class_Save_blockMetro.AnalizTable(myDBs["[БлокиМетро]"].ds.Tables[0], myDBs["[БлокиМетро]"].table, myDBs["[БлокиМетро]"].adapter);

            Class_Save_kan.AnalizTable(myDBs["[КАН]"].ds.Tables[0], myDBs["[КАН]"].table, myDBs["[КАН]"].adapter);

            Class_Save_kanS.AnalizTable(myDBs["[КАНы]"].ds.Tables[0], myDBs["[КАНы]"].table, myDBs["[КАНы]"].adapter);

            Class_Save_operMetro.AnalizTable(myDBs["[ОперацииМетро]"].ds.Tables[0], myDBs["[ОперацииМетро]"].table, myDBs["[ОперацииМетро]"].adapter);

            Class_Save_prov.AnalizTable(myDBs["[Проверка]"].ds.Tables[0], myDBs["[Проверка]"].table, myDBs["[Проверка]"].adapter);

            Class_Save_provFey.AnalizTable(myDBs["[Проверка ФЭУ]"].ds.Tables[0], myDBs["[Проверка ФЭУ]"].table, myDBs["[Проверка ФЭУ]"].adapter);

            Class_Save_provTCPM.AnalizTable(myDBs["[ПроверкаТСРМ61]"].ds.Tables[0], myDBs["[ПроверкаТСРМ61]"].table, myDBs["[ПроверкаТСРМ61]"].adapter);

            Class_Save_rabotBD.AnalizTable(myDBs["[Работы по БД]"].ds.Tables[0], myDBs["[Работы по БД]"].table, myDBs["[Работы по БД]"].adapter);

            Class_Save_systemVsbore.AnalizTable(myDBs["[Системы в сборе]"].ds.Tables[0], myDBs["[Системы в сборе]"].table, myDBs["[Системы в сборе]"].adapter);

            Class_Save_termocalibr.AnalizTable(myDBs["[Термокалибровка]"].ds.Tables[0], myDBs["[Термокалибровка]"].table, myDBs["[Термокалибровка]"].adapter);

            Class_Save_zamechPoBD.AnalizTable(myDBs["[Замечания по БД]"].ds.Tables[0], myDBs["[Замечания по БД]"].table, myDBs["[Замечания по БД]"].adapter);

            #endregion
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
    }
    
}
