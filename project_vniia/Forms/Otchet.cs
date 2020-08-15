using PickBoxTest;
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
    public partial class Otchet : Form
    {
        TabPage tabPage2;
        DataGridView dataGrid = new DataGridView();
        CheckBox checkBox = new CheckBox();
        DataGridView dataGrid_1; //new DataGridView[kolvo];
        CheckBox checkBox1;
        List<Object> rows_Type_obj = new List<Object>();
        DataSet ds = new DataSet();
        Button[] buttons;
        Button[] buttons_1;
        Button[] buttons_2;
        Button[] buttons_3;
        Button[] buttons_4;
        static int kolvo, kolvo1;

        private PickBox pb = new PickBox();
        

        public Otchet()
        {
            InitializeComponent();
            
            dataGrid.Location = new Point(20, 70 + 90 + 90+ 120);
            dataGrid.Size = new Size(900,200);
            dataGrid.Anchor = (AnchorStyles)(AnchorStyles.Top|AnchorStyles.Bottom | AnchorStyles.Left);
            dataGrid.Name = "dataGrid";
            dataGrid.Visible = false;
            tabPage1.Controls.Add(dataGrid);

            dataGrid.DataError += DataGrid_DataError;
            Control c = tabPage1.Controls[0];
            pb.WireControl(c);

            checkBox.Location = new Point(20, 70 + 90 + 90 + 85);
            checkBox.Size = new Size(110,23);
            checkBox.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left);
            checkBox.Text = "Зафиксировать";
            tabPage1.Controls.Add(checkBox);
            Control c1 = tabPage1.Controls[1];
            pb.WireControl(c1);

            checkBox.CheckedChanged += CheckBox_CheckedChanged;
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox.Checked == true)
            {
                for (int i = 0; i < tabPage1.Controls.Count; i++)
                {
                    if (tabPage1.Controls[i].Name == "dataGrid")
                    {
                        Control c = tabPage1.Controls[i];
                        pb.WireControl1(c);
                    }
                }
            }
            else
            {
                for (int i = 0; i < tabPage1.Controls.Count; i++)
                {
                    if (tabPage1.Controls[i].Name == "dataGrid")
                    {
                        Control c = tabPage1.Controls[i];
                        pb.WireControl(c);
                    }
                }
            }
        }

        private void DataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void Otchet_Load(object sender, EventArgs e)
        {
            MyDB_Type myDB = new MyDB_Type();
            Otrchet_BD(myDBs, myDBs_Type, myDB);
        }
        public Dictionary<string, Form1.MyDB> myDBs;

        private Dictionary<string, MyDB_Type> myDBs_Type = new Dictionary<string, MyDB_Type>();

        public class MyDB_Type
        {
            public DataSet ds;
        }

        public void Otrchet_BD(Dictionary<string, Form1.MyDB> myDBs, Dictionary<string, MyDB_Type> myDBs_Type, MyDB_Type dB_Type)
        {
            // if цифра в столбце- проверить были ли такие раньше/ нет - это новая цифра, значит новое количество для неё

            var table1 = myDBs["[Блоки]"].table.Copy();

            var rows_to_delete = new List<DataRow>();
            
            
            var rows_Location_obj = new List<Object>();
            var rows_Otmetka_obj = new List<Object>();

            var rows = table1.Rows;
            int t=0, t1=0,t2=0;
            int  kolvo2 = 5;
            kolvo = 0;
            kolvo1 = 0;

            if (table1.Columns.Contains("Тип БД"))
            {
                t = table1.Columns["Тип БД"].Ordinal;
            }
            if (table1.Columns.Contains("Местоположение"))
            {
                t1 = table1.Columns["Местоположение"].Ordinal;
            }
            if (table1.Columns.Contains("Отметка выполнения"))
            {
                t2 = table1.Columns["Отметка выполнения"].Ordinal;
            }
            
            foreach (DataRow r in rows)
            {
                if (!rows_Type_obj.Contains(r[t]))
                {
                    kolvo++;
                    rows_Type_obj.Add(r[t]);
                    
                }
               
                string a = r[t1].ToString();
                int value;
                int.TryParse(string.Join("", a.Where(c => char.IsDigit(c))), out value);

                if (!rows_Location_obj.Contains(value) && !r[t1].ToString().Contains("сдан") && !r[t1].ToString().Contains("Сдан"))
                {
                    kolvo1++;
                    rows_Location_obj.Add(value);
                }

                //if (!rows_Otmetka_obj.Contains(r[t2]) && !rows_Otmetka_obj.Contains("+") && !rows_Otmetka_obj.Contains("г") && 
                //    !r[t2].ToString().Contains("в")  && !r[t2].ToString().Contains("В"))
                //{
                //    kolvo2++;
                //    rows_Otmetka_obj.Add(r[t2]);
                //}
            }
            string[] text_ = new string[9] { "Типы блоков:", "Количество:", "Местонахождение в отделе:", "Количество в отделе:", "Блоков прошедших проверку:","Всего гермет. блоков", "?:", "Всего не гермет. блоков" , "Блоков не прошедших проверку" };
            TextBox[] box_text = new TextBox[9];
            for (int i = 0; i < box_text.Length; i++)
            {
                box_text[i] = new System.Windows.Forms.TextBox();
                int d = i > 3 ? '1' :
                    (i > 1 && i < 4) ? '2' :
                    '3';
                switch(d)
                {
                    case '1':
                        box_text[i].Location = new Point(20, 70 + i * 30);
                        break;
                    case '2':
                        box_text[i].Location = new Point(20, 50 + i * 30);
                        break;
                    case '3':
                        box_text[i].Location = new Point(20, 30 + i * 30);
                        break;
                }
                
                box_text[i].Size = new Size(150, 23);
                box_text[i].Text = text_[i];
                tabPage1.Controls.Add(box_text[i]);
            }
            TextBox[] box = new TextBox[kolvo];
            for (int i = 0; i < box.Length; i++)
            {
                box[i] = new System.Windows.Forms.TextBox();
                box[i].Location = new Point(195 + i * 130, 30);
                box[i].Size = new Size(80, 23);
                box[i].Text = rows_Type_obj[i].ToString();

                var TempFont = box[i].Font;
                var FontSt = FontStyle.Bold;
                box[i].Font = new Font(TempFont.FontFamily, TempFont.Size, FontSt);

                tabPage1.Controls.Add(box[i]);
            }

            
            TextBox[] box_ = new TextBox[kolvo1];
            for (int i = 0; i < box_.Length; i++)
            {
                box_[i] = new System.Windows.Forms.TextBox();
                box_[i].Location = new Point(195 + i * 130, 50+60);
                box_[i].Size = new Size(75, 23);
                box_[i].Text = rows_Location_obj[i].ToString();

                var TempFont = box_[i].Font;
                var FontSt = FontStyle.Bold;
                box_[i].Font = new Font(TempFont.FontFamily, TempFont.Size, FontSt);

                tabPage1.Controls.Add(box_[i]);
            }
            rows_Otmetka_obj.Add("+");
            rows_Otmetka_obj.Add("г");
            rows_Otmetka_obj.Add("?");

           
            DataTable table = new DataTable();

            for (int i=0; i<kolvo; i++)
            {
                table = table1.Clone();
                table.TableName = "Table_Type_"+i;
                table.BeginLoadData();
                ds.Tables.AddRange(new DataTable[] { table.Copy() });
            }
            for (int i = 0; i < kolvo1; i++)
            {
                table = table1.Clone();
                table.TableName = "Location_" + i;
                table.BeginLoadData();
                ds.Tables.AddRange(new DataTable[] { table.Copy() });
            }
            for (int i = 0; i < kolvo2; i++)
            {
                table = table1.Clone();
                table.TableName = "Otmetka_" + i;
                table.BeginLoadData();
                ds.Tables.AddRange(new DataTable[] { table.Copy() });
            }
            
            foreach (DataRow r in rows)
            {
                bool f = false ;

                object text = r[t];

                var ttt = r[t2].ToString();
                var tr = r[t1].ToString();

                if (ttt.Contains("в") || ttt.Contains("В") || tr.Contains("сдан") || tr.Contains("Сдан"))
                {
                  f = true;
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

            Razsortirovka(table1, ds, t, t1, t2, kolvo, kolvo1, rows_Type_obj, rows_Location_obj, rows_Otmetka_obj);


            buttons = new Button[kolvo];
            TextBox[] box1 = new TextBox[kolvo];
            for (int i = 0; i < box1.Length; i++)
            {
                var kolvo_blocks = ds.Tables[i].Rows.Count;

                box1[i] = new System.Windows.Forms.TextBox();
                box1[i].Location = new Point(195 + i * 130, 30+30);
                box1[i].Size = new Size(80, 23);
                box1[i].Text = kolvo_blocks.ToString();
                tabPage1.Controls.Add(box1[i]);

                buttons[i] = new Button();
                buttons[i].Location = new Point(195 + i * 130 + 85, 60);
                buttons[i].Size = new Size(30, 23);
                buttons[i].Text = i.ToString();
                buttons[i].Name = i.ToString();
                buttons[i].Click += Otchet_Click;
                tabPage1.Controls.Add(buttons[i]);
            }

            buttons_1 = new Button[kolvo1];
            TextBox[] box2 = new TextBox[kolvo1];
            var h = kolvo;
            for (int i = 0; i < box2.Length; i++)
            {
                var kolvo_blocks = ds.Tables[h].Rows.Count;

                box2[i] = new System.Windows.Forms.TextBox();
                box2[i].Location = new Point(195 + i * 130, 50 + 30+60);
                box2[i].Size = new Size(75, 23);
                box2[i].Text = kolvo_blocks.ToString();
                tabPage1.Controls.Add(box2[i]);

                buttons_1[i] = new Button();
                buttons_1[i].Location = new Point(195 + i * 130 + 85, 60+80);
                buttons_1[i].Size = new Size(30, 23);
                buttons_1[i].Text = i.ToString();
                buttons_1[i].Name = i.ToString();
                buttons_1[i].Click += Otchet_Click1;
                tabPage1.Controls.Add(buttons_1[i]);

                h++;
            }

            buttons_2 = new Button[kolvo2];
            TextBox[] box3 = new TextBox[kolvo2];
            var h1 = kolvo+kolvo1;
            for (int i = 0; i < box3.Length; i++)
            {
                var kolvo_blocks = ds.Tables[h].Rows.Count;

                box3[i] = new System.Windows.Forms.TextBox();
                box3[i].Location = new Point(195, (70 + 30 + 90)+i*30);
                box3[i].Size = new Size(75, 23);
                box3[i].Text = kolvo_blocks.ToString();
                tabPage1.Controls.Add(box3[i]);

                buttons_2[i] = new Button();
                buttons_2[i].Location = new Point(195+85, 70 + 30 + 90 + i * 30);
                buttons_2[i].Size = new Size(30, 23);
                buttons_2[i].Text = i.ToString();
                buttons_2[i].Name = i.ToString();
                buttons_2[i].Click += Otchet_Click2; 
                tabPage1.Controls.Add(buttons_2[i]);

                h++;
            }
            
            foreach (var row in rows_Type_obj)
            {
                DataSet dataSet = new DataSet();
                dB_Type = new MyDB_Type();
                string rrr = row.ToString();
                myDBs_Type["[" + rrr + "]"] = dB_Type;
                dB_Type.ds = dataSet;                                      
                
                DataTable type_ = new DataTable();  

                for (int i = 0; i < kolvo1 * kolvo2 + kolvo1; i++)
                {
                    type_ = table1.Clone();
                    type_.TableName = "T_" + i;
                    type_.BeginLoadData();
                    dB_Type.ds.Tables.AddRange(new DataTable[] { type_.Copy() });
                }
            }
            int j = 0;
            foreach (var row in rows_Type_obj)
            {
                string rrr = row.ToString();
                int p = Convert.ToInt32(rows_Otmetka_obj.LongCount());
                int ppp = kolvo1;
                Sort_po_type(myDBs_Type["[" + rrr + "]"].ds, ds.Tables[j], t1, rows_Location_obj);
                for (int i = 0; i < kolvo1; i++)
                {
                    Sort_po_location(myDBs_Type["[" + rrr + "]"].ds, myDBs_Type["[" + rrr + "]"].ds.Tables[i], t2, ppp, rows_Otmetka_obj); /// for 0 to kolvo
                    ppp = ppp + p + 2;
                }
                Page(j, rrr, myDBs_Type["[" + rrr + "]"].ds, kolvo, kolvo1, kolvo2, rows_Location_obj);
                j++;
            }

        }

        private void Otchet_Click2(object sender, EventArgs e)
        {
            string msg = ((Button)sender).Text;
            int msg_ = Convert.ToInt32(msg)+kolvo + kolvo1;
            dataGrid.DataSource = ds.Tables[msg_];
            dataGrid.Visible = true;
        }

        private void Otchet_Click1(object sender, EventArgs e)
        {
            string msg = ((Button)sender).Text;
            int msg_ = Convert.ToInt32(msg)+kolvo;
            dataGrid.DataSource = ds.Tables[msg_];
            dataGrid.Visible = true;
        }

        private void Otchet_Click(object sender, EventArgs e)
        {
            string msg = ((Button)sender).Text;
            int msg_ = Convert.ToInt32(msg);
            dataGrid.DataSource = ds.Tables[msg_];
            
            dataGrid.Visible = true;

        }

        public void Razsortirovka(DataTable table, DataSet ds, int t, int t1, int t2, int kolvo, int kolvo1, List<Object> rows_Type_obj, List<Object> rows_Location_obj, List<Object> rows_Otmetka_obj)
        {
            var rows = table.Rows;
            foreach (DataRow r in rows)
            {
                var rt = r[t].ToString();
                var rt1 = r[t1].ToString();
                var rt2 = r[t2].ToString();
                int k = 0;
                foreach (var p in rows_Type_obj)
                {
                    if (p.ToString().Contains(rt))
                    {
                        ds.Tables[k].LoadDataRow(r.ItemArray, true);
                        break;
                    }
                    k++;
                }
                k = kolvo;
                foreach (var p in rows_Location_obj)
                {
                    var pp = p.ToString();
                    if (rt1.Contains(pp) || (rt1 == "" && pp =="0"))
                    {
                        ds.Tables[k].LoadDataRow(r.ItemArray, true);
                        break;
                    }
                    k++;
                }
                k = kolvo1 + kolvo;

                foreach (var p in rows_Otmetka_obj)
                {
                    var pp = p.ToString();
                    if (rt2.Contains(pp))
                    {
                        if(pp =="+")
                        {
                            if (!rt2.Contains("г"))
                            {
                                ds.Tables[k].LoadDataRow(r.ItemArray, true);
                            }
                        }
                        else
                            ds.Tables[k].LoadDataRow(r.ItemArray, true);
                    }
                    
                    k++;
                }
                if (!rt2.Contains("г"))
                {
                    int l = ds.Tables.Count;
                    ds.Tables[l - 2].LoadDataRow(r.ItemArray, true);
                }
                if (!rt2.Contains("+"))
                {
                    int l = ds.Tables.Count;
                    ds.Tables[l - 1].LoadDataRow(r.ItemArray, true);
                }
            }
        }


        public void Sort_po_type(DataSet ds, DataTable table, int t1, List<Object> rows_Location_obj)
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
        public void Sort_po_location( DataSet ds, DataTable table, int t2, int kolvo1, List<Object> rows_Otmetka_obj)
        {
            var rows = table.Rows;
            foreach (DataRow r in rows)
            {
                var rt2 = r[t2].ToString();
                int k = kolvo1;

                foreach (var p in rows_Otmetka_obj)
                {
                    var pp = p.ToString();
                    if (rt2.Contains(pp))
                    {
                        if (pp == "+")
                        {
                            if (!rt2.Contains("г"))
                            {
                                ds.Tables[k].LoadDataRow(r.ItemArray, true);
                            }
                        }
                        else
                            ds.Tables[k].LoadDataRow(r.ItemArray, true);
                    }
                    k++;
                }
                if (!rt2.Contains("г"))
                {
                    int l = Convert.ToInt32(rows_Otmetka_obj.LongCount());
                    ds.Tables[kolvo1 + l].LoadDataRow(r.ItemArray, true);
                }
                if (!rt2.Contains("+"))
                {
                    int l = Convert.ToInt32(rows_Otmetka_obj.LongCount());
                    ds.Tables[kolvo1 + l + 1].LoadDataRow(r.ItemArray, true);
                }
            }
        }

        public void Page(int j, string rrr, DataSet ds, int kolvo, int kolvo1, int kolvo2, List<Object> rows_Location_obj)
        {
            tabPage2 = new TabPage();
            tabPage2.Text = rrr;
            tabPage2.Name = "tabPage" + (j + 2);
            tabControl1.Controls.Add(tabPage2);

            dataGrid_1 = new DataGridView();

            dataGrid_1.Location = new Point(20, 70 + 90+120);
            dataGrid_1.Size = new Size(900, 300);
            dataGrid_1.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
            dataGrid_1.Name = "data_" + (j+2);
            dataGrid_1.Visible = false; 
            dataGrid_1.DataError += DataGrid_1_DataError1;
            tabPage2.Controls.Add(dataGrid_1);

            Control c = tabPage2.Controls[0];
            pb.WireControl(c);

            checkBox1 = new CheckBox();
            checkBox1.Location = new Point(20, 70 + 90 + 90);
            checkBox1.Size = new Size(110, 23);
            checkBox1.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left);
            checkBox1.Name = "check_" + (j + 2);
            checkBox1.Text = "Зафиксировать";
            tabPage2.Controls.Add(checkBox1);
            Control c1 = tabPage2.Controls[1];
            pb.WireControl(c1);

            checkBox1.CheckedChanged += CheckBox1_CheckedChanged;


            string[] text_ = new string[7] { "Местонахождение в отделе:", "Количество в отделе:", "Блоков прошедших проверку:", "Всего гермет. блоков", "?:", "Всего не гермет. блоков", "Блоков не прошедших проверку" };
            TextBox[] box_text = new TextBox[7];
            for (int i = 0; i < box_text.Length; i++)
            {
                box_text[i] = new System.Windows.Forms.TextBox();
                box_text[i].Location = new Point(20, 30 + i * 30);
                box_text[i].Size = new Size(150, 23);
                box_text[i].Text = text_[i];
                tabPage2.Controls.Add(box_text[i]);
            }
            
            TextBox[] box_ = new TextBox[kolvo1];
            for (int i = 0; i < box_.Length; i++)
            {
                box_[i] = new System.Windows.Forms.TextBox();
                box_[i].Location = new Point(195 + i * 200, 30);
                box_[i].Size = new Size(75, 23);
                box_[i].Text = rows_Location_obj[i].ToString();

                var TempFont = box_[i].Font;
                var FontSt = FontStyle.Bold;
                box_[i].Font = new Font(TempFont.FontFamily, TempFont.Size, FontSt);

                tabPage2.Controls.Add(box_[i]);
                
            }

            buttons_3 = new Button[kolvo1];
            TextBox[] box2 = new TextBox[kolvo1];
            var h = 0;

            for (int i = 0; i < box2.Length; i++)
            {
                var kolvo_blocks = ds.Tables[h].Rows.Count;

                box2[i] = new System.Windows.Forms.TextBox();
                box2[i].Location = new Point(195 + i * 200, 30 + 30);
                box2[i].Size = new Size(75, 23);
                box2[i].Text = kolvo_blocks.ToString();
                tabPage2.Controls.Add(box2[i]);
                
                buttons_3[i] = new Button();
                buttons_3[i].Location = new Point(195 + 85 + i * 200, 30+30);
                buttons_3[i].Size = new Size(30, 23);
                buttons_3[i].Text = i.ToString();
                buttons_3[i].Name = "but_" + (j+2);
                buttons_3[i].Click += Otchet_Click3; 
                tabPage2.Controls.Add(buttons_3[i]);

                h++;
            }
            buttons_4 = new Button[kolvo2];
            TextBox[] box3 = new TextBox[kolvo2];
            var h1 = kolvo1;
            for (int f=0; f< kolvo1; f++)
            for (int i = 0; i < box3.Length; i++)
            {
                var kolvo_blocks = ds.Tables[h].Rows.Count;

                box3[i] = new System.Windows.Forms.TextBox();
                box3[i].Location = new Point(195 + f*200, 30 + 60+ i*30);
                box3[i].Size = new Size(75, 23);
                box3[i].Text = kolvo_blocks.ToString();
                tabPage2.Controls.Add(box3[i]);

                buttons_4[i] = new Button();
                buttons_4[i].Location = new Point(195 + 85 + f * 200, 30 + 60 + i * 30);
                buttons_4[i].Size = new Size(30, 23);
                buttons_4[i].Text = h.ToString();
                buttons_4[i].Name = "butt_" + (j + 2);
                buttons_4[i].Click += Otchet_Click3;   
                tabPage2.Controls.Add(buttons_4[i]);

                h++;
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            string name = ((CheckBox)sender).Name;
            bool change = ((CheckBox)sender).Checked;
            int value;
            int.TryParse(string.Join("", name.Where(c => char.IsDigit(c))), out value);
            
            string prom = "tabPage" + (value);
            string rom = "data_" + (value);
            bool end = false;
            for (int i = 0; i < tabControl1.Controls.Count; i++)
            {
                if (tabControl1.Controls[i].Name == prom)
                {
                    TabPage tab = tabControl1.Controls[i] as TabPage;

                    for (int h = 0; h < tab.Controls.Count; h++)
                    {
                        if (tab.Controls[h].Name == rom)
                        {
                            Control c = tab.Controls[h];
                            if (change)
                                pb.WireControl1(c);
                            else
                                pb.WireControl(c);
                            end = true;
                            break;
                        }
                    }
                }
                if (end)
                    break;
            }
            
        }

        private void DataGrid_1_DataError1(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }
       
        private void Otchet_Click3(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name;
            int value;
            int.TryParse(string.Join("", name.Where(c => char.IsDigit(c))), out value);
            
            string msg = ((Button)sender).Text;
            int msg_ = Convert.ToInt32(msg);
            string prom = "tabPage" + (value);
            string rom = "data_" + (value);
            bool end = false;
            for (int i = 0; i < tabControl1.Controls.Count; i++)
            {
                if (tabControl1.Controls[i].Name == prom)
                {
                    TabPage tab = tabControl1.Controls[i] as TabPage;
                    
                    for (int h = 0; h < tab.Controls.Count; h++)
                    {
                        if (tab.Controls[h].Name == rom )
                        {
                            DataGridView data = tab.Controls[h] as DataGridView;
                            string type = rows_Type_obj[value-2].ToString();
                            data.DataSource = myDBs_Type["[" + type + "]"].ds.Tables[msg_];
                            data.Visible = true;
                            end = true;
                            break;
                        }
                    }
                }
                if (end)
                    break;
            }
            
        }
    }
}
// убрать где + букву г