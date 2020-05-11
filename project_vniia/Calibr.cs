using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace project_vniia
{   class Item
    {
        public static string BD_;

        public string BD { get; private set;} // [Номер БД] (Текстовый, 50)
        public DateTime Data { get; private set; } // [Дата] (Дата/время)
        public int T_cod { get; private set; } // [Температура (КОД)] (Числовой, целое, авто)
        public float T_proz { get; private set; } // [Температура (Проц)] (Числовой, Одинарное с плавающей точкой)
        public int U_cod { get; private set; } // [U (код)] (Числовой, целое, авто)
        public int U_sh { get; private set; } // [U (ШИМ)] (Числовой, целое, авто)
        public int U_izm { get; private set; } // [U (измеренное)] (Числовой, целое, авто)
        public int Sv { get; private set; } // [Код светодиода] (Числовой, Длинное целое, авто)
        public string Prim { get; private set; } // [Примечание] (Текстовый, 250)

        public Item(string str)
        {
            if (Calibr.one == true)
            {
                string[] parts = str.Split('\t');
                BD = parts[0];
                BD_ = BD;
                Data = Convert.ToDateTime(parts[1]);
                T_cod = int.Parse(parts[2]);
                parts[3] = parts[3].Replace(".", ",");
                T_proz = float.Parse(parts[3]);
                U_sh = int.Parse(parts[4]);
                U_cod = int.Parse(parts[5]);
                U_izm = int.Parse(parts[6]);
                Sv = int.Parse(parts[7]);
                Prim = parts[8];
            }
            
        }
    }
    
    class Calibr
    {
        public static bool one = true;
        public void Main_calibr(Form1 form1)
        {
            List<Item> items = new List<Item>();
            List<string> Fil = Directory.GetFiles(Form1.Log_ways, "*.log").ToList<string>();
            foreach (var fil in Fil)
            {
                string[] allStringFromFile = File.ReadAllLines(fil, Encoding.Default);

                int len = allStringFromFile.Length;

                items.Clear();
                one = false;
                
                for (int i = 0; i < len; i++)
                {
                    items.Add(new Item(allStringFromFile[i]));
                    one = true;
                }
                
                one = false;
                bool del = true;
                foreach (Item item in items)
                {
                    if (one == true)
                    {
                        var conn_tabl_sv = new OleDbConnection(Form1.conString);
                        try
                        {
                            conn_tabl_sv.Open();

                            var command2_sv = new OleDbCommand();
                            command2_sv.Connection = conn_tabl_sv;

                            var command2_sv_0 = new OleDbCommand();
                            command2_sv_0.Connection = conn_tabl_sv;
                            if (del)
                            {
                                command2_sv_0.CommandText = "DELETE FROM `Термокалибровка` WHERE `Номер БД` = ?";
                                command2_sv_0.Parameters.AddWithValue("?", item.BD);

                                del = false;

                                int com2_rez_sv_0 = command2_sv_0.ExecuteNonQuery();
                                command2_sv_0.Parameters.Clear();

                                Console.WriteLine("--->" + com2_rez_sv_0);

                            }

                            command2_sv.CommandText = "INSERT INTO `Термокалибровка` (`Дата`, `Температура (КОД)`," +
                                "`Температура (Проц)`, `U (при коде Uном)`, `U (код)`, `U (измеренное)`," +
                                "`Код светодиода`, `Примечание`, `Номер БД`) VALUES (?,?,?,?,?,?,?,?,?)";

                            command2_sv.Parameters.AddWithValue("?", item.Data);
                            command2_sv.Parameters.AddWithValue("?", item.T_cod);
                            command2_sv.Parameters.AddWithValue("?", item.T_proz);
                            command2_sv.Parameters.AddWithValue("?", item.U_sh);
                            command2_sv.Parameters.AddWithValue("?", item.U_cod);
                            command2_sv.Parameters.AddWithValue("?", item.U_izm);
                            command2_sv.Parameters.AddWithValue("?", item.Sv);
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
                    one = true;
                 
                }
                string file = Path.GetFileName(fil);
                string newPath = Path.Combine(Form1.Log_ways_peremesti, file);
                File.Move(fil, newPath);
            }
        } 
    }   
}


