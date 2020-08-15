using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace project_vniia
{
    public partial class Form_cod : Form
    {
        public DataTable[] dataTables = new DataTable[13];
      
        TextBox zamena = new TextBox();
        TextBox zamena_1 = new TextBox();
        Button _zamena = new Button();
        Label name = new Label();

        public Form_cod()
        {
            InitializeComponent();
            
            zamena_1.Location = new System.Drawing.Point(180, 60);
            zamena_1.Size = new System.Drawing.Size(100, 25);
            Controls.Add(zamena_1);

            zamena.Location = new System.Drawing.Point(40, 60);
            zamena.Size = new System.Drawing.Size(100, 25);
            Controls.Add(zamena);

            
            _zamena.Location = new System.Drawing.Point(110, 100);
            _zamena.Size = new System.Drawing.Size(95, 25);
            _zamena.Text = "Замена";
            _zamena.Click += _zamena_Click;
            Controls.Add(_zamena);

            name.Location = new Point(40, 25);
            name.Size = new System.Drawing.Size(400,25);
            name.Text = "Введите номера блоков (сначала КАКОЙ заменить, далее НА какой):";
            Controls.Add(name);

            FormClosing += Form_cod_FormClosing;
        }

        private void Form_cod_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.zam = false;
        }

        public int[] vs = new int[8] {2,3,5,6,7,9,10,12 };
        public int v;
        //{"БлокиМетро", "Замечания по БД", "КАНы", "ОперацииМетро", "Проверка",
        //    "ПроверкаТСРМ61", "Работы по БД", "Термокалибровка"}; // системы в сборе---


        private void _zamena_Click(object sender, EventArgs e)
        {
            if (Form1.zam == true)
            {
                bool sysh = exists(zamena.Text);
                bool sysh_1 = exists(zamena_1.Text);
                if (sysh && !sysh_1)
                {
                    Change_BD_();
                }
                else
                {
                    if(!sysh)
                    {
                        MessageBox.Show("Блок с введённым номером: " + zamena.Text + " не существует!");
                    }
                    else
                        MessageBox.Show("Блок с введённым номером: " + zamena_1.Text + " существует!");
                }
                   
            }
            else
            {
                bool sysh = exists(zamena.Text);
                bool sysh_1 = exists(zamena_1.Text);
                if (sysh && sysh_1)
                {
                    int one = 0, two = 0, colom = 0;

                    if (dataTables[0].Columns.Contains("s_ColLineage") == true)
                        colom++;
                    if (dataTables[0].Columns.Contains("s_Generation") == true)
                        colom++;
                    if (dataTables[0].Columns.Contains("s_GUID") == true)
                        colom++;
                    if (dataTables[0].Columns.Contains("s_Lineage") == true)
                        colom++;

                    string[] mass = new string[dataTables[0].Columns.Count - colom];
                    string[] mass1 = new string[dataTables[0].Columns.Count - colom];

                    for (int i = 0; i < dataTables[0].Rows.Count; i++)
                    {
                        if (dataTables[0].Rows[i][0].ToString() == zamena.Text)
                        {
                            for (int j = 0; j < dataTables[0].Columns.Count - colom; j++)
                            {
                                mass[j] = dataTables[0].Rows[i][j].ToString();
                            }

                            one = i;
                        }
                        if (dataTables[0].Rows[i][0].ToString() == zamena_1.Text)
                        {
                            for (int j = 0; j < dataTables[0].Columns.Count - colom; j++)
                            {
                                mass1[j] = dataTables[0].Rows[i][j].ToString();
                            }

                            two = i;
                        }
                    }
                    for (int i = 1; i < dataTables[0].Columns.Count - colom; i++)
                    {
                        dataTables[0].Rows[one][i] = mass1[i];
                        dataTables[0].Rows[two][i] = mass[i];
                    }

                    ////////////////////////////

                    int k = 0, g = 0, m = 0;
                    for (v = 0; v < vs.Length; v++)
                    {

                        string[] stolbez = new string[3] { "Номер блока", "Номер БД", "Номер изделия" }; //"Номер системы" };

                        for (int i = 0; i < stolbez.Length; i++)
                        {
                            string stolb = dataTables[vs[v]].Columns.Contains(stolbez[i]) ? "is" : "is not";
                            if (stolb == "is")
                            {
                                k = i;
                                break;
                            }
                        }
                        number_filtr(zamena);
                        m = items.Count;
                        string[] mass_sv = new string[m];
                        items.Clear();

                        number_filtr(zamena_1);
                        if (items.Count == 0 && m == 0)
                        { }
                        else
                        //if(items.Count != 0)
                        {
                            for (int i = 0; i < dataTables[vs[v]].Rows.Count; i++)
                            {
                                if (dataTables[vs[v]].Rows[i][stolbez[k]].ToString() == zamena_1.Text)
                                {
                                    dataTables[vs[v]].Rows[i][stolbez[k]] = zamena.Text;
                                }
                            }
                            for (int i = 0; i < dataTables[vs[v]].Rows.Count; i++)
                            {
                                if (dataTables[vs[v]].Rows[i][stolbez[k]].ToString() == zamena.Text)
                                {
                                    if (items.Contains(dataTables[vs[v]].Rows[i]["Номер записи"].ToString()))
                                    { }
                                    else
                                    {
                                        dataTables[vs[v]].Rows[i][stolbez[k]] = zamena_1.Text;
                                        mass_sv[g] = dataTables[vs[v]].Rows[i]["Номер записи"].ToString();
                                        g++;
                                    }
                                }
                            }
                            //using (StreamWriter writer_sv = new StreamWriter(Form1.filePath, true, Encoding.Default))
                            //{
                            //    writer_sv.WriteLine("Замена номера блока в связной табл. " + vs[v] + ":"
                            //        + zamena.Text + "  На номер блока:" + zamena_1.Text + "   В строках номер:");

                            //    foreach (string str in mass_sv)
                            //    {
                            //        writer_sv.WriteLine(str);
                            //    }
                            //    writer_sv.WriteLine("////////////////////////////////");
                            //    foreach (string str in items)
                            //    {
                            //        writer_sv.WriteLine(str);
                            //    }
                            //    writer_sv.WriteLine("////////////////////////////////");
                            //    writer_sv.WriteLine("////////////////////////////////");
                            //}
                            //////////////////////


                            items.Clear();
                            g = 0;
                        }
                    }
                    try
                    {
                        string datanow = DateTime.Now.ToString("dd MMMM yyyy");

                        string prim = "Заменён на " + zamena_1.Text + ". Был ФЭУ " + mass[2] + " U = " + mass[3] + ".(Блок " + zamena.Text + ")";

                        string prim1 = "Заменён на " + zamena.Text + ". Был ФЭУ " + mass1[2] + " U = " + mass1[3] + ".(Блок " + zamena_1.Text + ")";

                        foreach (var r in dataTables)
                        {
                            if (r.TableName == "Замечания по БД")
                            {
                                DataRow row = r.NewRow();
                                row["Номер блока"] = zamena_1.Text;
                                row["Дата заметки"] = datanow;
                                row["Заметка"] = prim;
                                r.Rows.Add(row);

                                var rorr = r.NewRow();
                                rorr["Номер блока"] = zamena.Text;
                                rorr["Дата заметки"] = datanow;
                                rorr["Заметка"] = prim1;
                                r.Rows.Add(rorr);
                            }
                        }
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.ToString());
                    }
                }
            }
                // доработать таймер
                timer.Interval = 500;
                timer.Tick += Timer_Tick;
                timer.Start();
               
        }
      
        private void Timer_Tick(object sender, EventArgs e)
        {
            _zamena.BackColor = colors[counter++];
            if (counter == colors.Length)
            {
                counter = 0;
                timer.Stop();
            }
        }
        Timer timer = new Timer();
        Color[] colors = { Color.AliceBlue, Color.AntiqueWhite, Color.Aqua, Color.Aquamarine, Color.Azure };
        int counter = 0;

        private void Form_cod_Load(object sender, EventArgs e)
        {
            
        }

        static List<string> items = new List<string>();
        
        public bool exists(string text)
        {
            bool sysh = false;
            var table1 = dataTables[0];
            var table2 = table1.Copy();
            var rows_to_delete = new List<DataRow>();

            var rows = table2.Rows;
            foreach (DataRow r in rows)
            {
                bool f = true;
                var ch = r.ItemArray[0].GetType();

                var c = r.ItemArray[1];

                if (ch.Name.ToString() == "String")
                {
                    c = r.ItemArray[0];
                }

                if (c.ToString() == text)//сравнение с номером бд
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
            
            char nim1 = (rows.Count==1) ? 'a' :
                       (rows.Count > 1) ? 'b' :
                        'c' ;
            switch (nim1)
            {
                case 'a':
                    sysh = true;
                    break;
                case 'b':
                    MessageBox.Show("Введённый номер блока "+ text +" повторяется в строках! Проверьте на правильность!");
                    break;
                case 'c':
                    if(Form1.zam == false)
                    MessageBox.Show("Блок с введённым номером: " + text +" не существует!");
                    break;
            }
                    return sysh;
        }
        public void number_filtr(TextBox box)
        {
            var table1 = dataTables[vs[v]];
            var table2 = table1.Copy();

            //переписать t1 -> t2 С учетом фильтра

            var rows_to_delete = new List<DataRow>();

            var rows = table2.Rows;
            foreach (DataRow r in rows)
            {
                bool f = true;
                var ch = r.ItemArray[0].GetType();
                
                var c = r.ItemArray[1];

                if (ch.Name.ToString() == "String")
                {
                   c = r.ItemArray[0];
                }
                
                if (c.ToString() == box.Text)//сравнение с номером бд
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

            /// записать значения table2.Rows[i][0] столбца найденного(номер строки)
            /// в массив статический или в лист и передать обратно
            /// в калибр + изменить там запрос
            /// + колво значений узнать для массива
            for (int i = 0; i < table2.Rows.Count; i++)
            {
                items.Add(table2.Rows[i]["Номер записи"].ToString());
                //MessageBox.Show(table2.Rows[i][0].ToString());
            }
        }

        public void Change_BD_()
        {
            int one = 0, colom = 0;

            if (dataTables[0].Columns.Contains("s_ColLineage") == true)
                colom++;
            if (dataTables[0].Columns.Contains("s_Generation") == true)
                colom++;
            if (dataTables[0].Columns.Contains("s_GUID") == true)
                colom++;
            if (dataTables[0].Columns.Contains("s_Lineage") == true)
                colom++;

            string[] mass = new string[dataTables[0].Columns.Count - colom];

            for (int i = 0; i < dataTables[0].Rows.Count; i++)
            {
                if (dataTables[0].Rows[i][0].ToString() == zamena.Text)
                {
                    for (int j = 0; j < dataTables[0].Columns.Count - colom; j++)
                    {
                        mass[j] = dataTables[0].Rows[i][j].ToString();
                    }
                    one = i;
                }
            }

            dataTables[0].Rows[one][0] = zamena_1.Text;
            
            //using (StreamWriter writer = new StreamWriter(Form1.filePath, true, Encoding.Default))
            //{
            //    writer.WriteLine("Замена номера блока:" + zamena.Text + "  На номер блока:" + zamena_1.Text);
            //    foreach (string str in mass)
            //    {
            //        writer.WriteLine(str);
            //    }
            //}

            ////////////////////////////

            int k = 0, m = 0;
            for (v = 0; v < vs.Length; v++)
            {
                string[] stolbez = new string[3] { "Номер блока", "Номер БД", "Номер изделия" }; //"Номер системы" };

                for (int i = 0; i < stolbez.Length; i++)
                {
                    string stolb = dataTables[vs[v]].Columns.Contains(stolbez[i]) ? "is" : "is not";
                    if (stolb == "is")
                    {
                        k = i;
                        break;
                    }
                }
                number_filtr(zamena);
                m = items.Count;
                
                if (items.Count == 0 && m == 0)
                { }
                else
                {
                    for (int i = 0; i < dataTables[vs[v]].Rows.Count; i++)
                    {
                        if (dataTables[vs[v]].Rows[i][stolbez[k]].ToString() == zamena.Text)
                        {
                            dataTables[vs[v]].Rows[i][stolbez[k]] = zamena_1.Text;
                        }
                    }
                    
                    //using (StreamWriter writer_sv = new StreamWriter(Form1.filePath, true, Encoding.Default))
                    //{
                    //    writer_sv.WriteLine("Замена поля 'номер блока' в связной табл. " + vs[v] + ":"
                    //        + zamena.Text + "  На номер блока:" + zamena_1.Text + "   В строках номер:");

                    //    foreach (string str in items)
                    //    {
                    //        writer_sv.WriteLine(str);
                    //    }
                    //    writer_sv.WriteLine("////////////////////////////////");
                    //    writer_sv.WriteLine("////////////////////////////////");
                    //}

                    items.Clear();
                }
            }
            ////////////////
            ///просто добавить в табл - без сохранения

            string datanow = DateTime.Now.ToString("dd MMMM yyyy");

            string prim = "Заменён на " + zamena_1.Text + ". Был ФЭУ " + mass[2] + " U = " + mass[3] + ".(Блок " + zamena.Text + ")";


            try
            {

                foreach (var r in dataTables)
                {
                    if (r.TableName == "Замечания по БД")
                    {
                        DataRow row = r.NewRow();
                        row["Номер блока"] = zamena_1.Text;
                        row["Дата заметки"] = datanow;
                        row["Заметка"] = prim;
                        r.Rows.Add(row);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
        }
    }
}
//изменить нумерацию-так как столбик c номером то нулевой то первый-> +
//была проблема с номером столбца в фильтре в табл ОперацииМетро-> как выше описано-> +

//в табл КАН название счётчика выглядит иначе "Номер_Записи", а везде "Номер_записи"