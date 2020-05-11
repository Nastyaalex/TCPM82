using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_vniia
{
    class Item_Zamech_BD
    {
        public static string BD_;
        public string BD { get; private set; } // [Номер БД] (Текстовый, 50)
        public DateTime Data { get; private set; } // [Дата] (Дата/время)
        public int Cs_Unom { get; private set; } // [Температура (КОД)] (Числовой, целое, авто)
        public string Prim { get; private set; } // [Примечание] (Текстовый, 250)

        public Item_Zamech_BD(string str)
        {
                string[] parts = str.Split('\t');
                BD = parts[0];
                BD_ = BD;
                Data = Convert.ToDateTime(parts[3]);
                Cs_Unom = int.Parse(parts[4]);
                Prim = parts[5];
            
        }
    } 
    class Zamech_BD
    {

        public void Main_Zamech_BD(Form1 form1)
        {
            List<Item_Zamech_BD> items = new List<Item_Zamech_BD>();
            List<string> Fil = Directory.GetFiles(Form1.Zamech_ways, "*.log").ToList<string>();
            foreach (var fil in Fil)
            {
                string[] allStringFromFile = File.ReadAllLines(fil, Encoding.Default);

                int len = allStringFromFile.Length;

                items.Clear();
                
                for (int i = 0; i < len; i++)
                {
                    items.Add(new Item_Zamech_BD(allStringFromFile[i]));
                }
                
                foreach (Item_Zamech_BD item in items)
                {

                    var conn_tabl_sv = new OleDbConnection(Form1.conString);
                    try
                    {
                        conn_tabl_sv.Open();

                        var command2_sv = new OleDbCommand();
                        command2_sv.Connection = conn_tabl_sv;
                        command2_sv.CommandText = "UPDATE `Замечания по БД` SET `Дата заметки` = ?, `Cs при Uном` = ?," +
                                       "`Заметка`= ? WHERE `Номер блока` = ?";

                        command2_sv.Parameters.AddWithValue("?", item.Data);
                        command2_sv.Parameters.AddWithValue("?", item.Cs_Unom);
                        command2_sv.Parameters.AddWithValue("?", item.Prim);
                        command2_sv.Parameters.AddWithValue("?", item.BD);

                        int com2_rez_sv = command2_sv.ExecuteNonQuery();
                        command2_sv.Parameters.Clear();

                        Console.WriteLine("--->" + com2_rez_sv);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.ToString());
                        return;
                    }
                    finally
                    {
                        conn_tabl_sv.Close();
                    }
                }
                
                string file = Path.GetFileName(fil);
                string newPath = Path.Combine(Form1.Zamech_ways_peremesti, file);
                File.Move(fil, newPath);
            }
            
        }
    }
}
