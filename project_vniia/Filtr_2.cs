using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace project_vniia
{
    class Filtr_2
    {
        public static bool Filtr2 = false;

        public static bool Ccc = false;
        public static bool Bbb = false;

        public static void Tabl2(Dictionary<string, Form1.MyDB> myDBs, ComboBox comboBox1, TextBox textBox1, TextBox textBox2, DataGridView dataGridView1, DataGridView dataGridView2)
        {
            Filtr2 = true;
            Filltr(comboBox1, textBox1, textBox2, dataGridView1, dataGridView2, myDBs);
            
        }

        public static void Filltr(ComboBox comboBox1, TextBox textBox1, TextBox textBox2, DataGridView dataGridView1, DataGridView dataGridView2, Dictionary<string, Form1.MyDB> myDBs)
        {
            string text1 = textBox2.Text;
            string text = textBox1.Text;
            char nim1 = (text == "" && text1 == "" ) ? 'a' :
                                       (text == "" ) ? 'b' :
                                      (text1 == "" ) ? 'c' :
                                      'd';
            switch(nim1)
            {
                case 'a':
                    dataGridView1.DataSource = myDBs["[Блоки]"].table;
                    dataGridView2.DataSource = myDBs["[" + comboBox1.Text + "]"].table;
                    break;
                case 'b':
                    Ccc = true;
                    break;
                case 'c':
                    Bbb = true;
                    Filtr2 = false;
                    break;
                case 'd':
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
                        char nim = 'g', aaa = 'o', bbb = 'o';
                        foreach (var c in r.ItemArray)
                        {
                            if (s_)
                            {
                                if ((k < kolvo) && (k < kolvo - 1) && (k < kolvo - 2) && (k < kolvo - 3))
                                {
                                    if (aaa == 'a')
                                        nim = c.ToString().Contains(text1) ? 'b' :
                                               'c';
                                    else
                                        nim = c.ToString().Contains(text) ? 'a' :
                                               c.ToString().Contains(text1) ? 'b' :
                                               'c';
                                    
                                    if (nim == 'a')
                                    {
                                        aaa = nim;
                                    }
                                    if (nim == 'b')
                                    { bbb = nim; }
                                    if (aaa == 'a' && bbb == 'b')
                                    {
                                        f = false;
                                        break;
                                    }
                                }
                                else { break; }
                            }
                            else
                            {
                                if (aaa == 'a')
                                    nim = c.ToString().Contains(text1) ? 'b' :
                                           'c';
                                else
                                    nim = c.ToString().Contains(text) ? 'a' :
                                           c.ToString().Contains(text1) ? 'b' :
                                           'c';
                                
                                if (nim == 'a')
                                {
                                    aaa = nim;
                                }
                                if (nim == 'b')
                                { bbb = nim; }
                                if (aaa == 'a' && bbb == 'b')
                                {
                                    f = false;
                                    break;
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
                    ////////////////////////////////////
                    ///
                    var table3 = myDBs["[" + comboBox1.Text + "]"].table;
                    k = 0; s_ = false;
                    if (table3.Columns.Contains("s_ColLineage") == true)
                        k++; 
                    if (table3.Columns.Contains("s_Generation") == true)
                        k++; 
                    if (table3.Columns.Contains("s_GUID") == true)
                        k++; 
                    if (table3.Columns.Contains("s_Lineage") == true)
                        k++;
                    var table4 = table3.Copy();
                    if (k != 0)
                        s_ = true;
                    //переписать t1 -> t2 С учетом фильтра

                    var rows_to_delete_1 = new List<DataRow>();

                    var rows_1 = table4.Rows;
                    foreach (DataRow r in rows_1)
                    {
                        bool f = true;
                        int kolvo = r.ItemArray.Length;
                        k = 1;
                        char nim = 'g', aaa = 'o', bbb = 'o';
                        foreach (var c in r.ItemArray)
                        {
                            if (s_)
                            {
                                if ((k < kolvo) && (k < kolvo - 1) && (k < kolvo - 2) && (k < kolvo - 3))
                                {
                                    if(aaa=='a')
                                    nim = c.ToString().Contains(text1) ? 'b' :
                                           'c';
                                    else
                                        nim = c.ToString().Contains(text) ? 'a' :
                                               c.ToString().Contains(text1) ? 'b' :
                                               'c';
                                    if (nim == 'a')
                                    {
                                        aaa = nim;
                                    }
                                    if (nim == 'b')
                                    { bbb = nim; }
                                    if (aaa == 'a' && bbb == 'b')
                                    {
                                        f = false;
                                        break;
                                    }
                                }
                                else { break; }
                            }
                            else
                            {
                                if (aaa == 'a')
                                    nim = c.ToString().Contains(text1) ? 'b' :
                                           'c';
                                else
                                    nim = c.ToString().Contains(text) ? 'a' :
                                           c.ToString().Contains(text1) ? 'b' :
                                           'c';

                                if (nim == 'a')
                                {
                                    aaa = nim;
                                }
                                if (nim == 'b')
                                { bbb = nim; }
                                if (aaa == 'a' && bbb == 'b')
                                {
                                    f = false;
                                    break;
                                }
                            }
                            k++;
                        }
                        if (f)
                        {
                            rows_to_delete_1.Add(r);
                        }
                        Console.WriteLine();
                    }

                    foreach (var r in rows_to_delete_1)
                    {
                        rows_1.Remove(r);
                    }

                    rows_to_delete_1.Clear();

                    dataGridView2.DataSource = table4;

                    Form1.flag_filtr_1 = true;

                    break;
                default:
                    break;
            }
            
        }
    }
}
