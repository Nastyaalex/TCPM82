﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_vniia
{
    class Class_ways
    {
        public static bool Pusto_(string _ways)
        {
            bool pusto = false;

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!Directory.Exists(path + "\\TestWay"))
                Directory.CreateDirectory(path + "\\TestWay");
            if (!File.Exists(path + "\\TestWay" + _ways))
            {
                File.Create(path + "\\TestWay" + _ways).Close();
            }

            try
            {
                using (StreamReader sr = new StreamReader(path + "\\TestWay" + _ways))
                {
                    string line;
                    
                    if ((line = sr.ReadLine()) == null || line == "")
                    {
                        pusto = true;
                    }
                }
                
            }
            catch (Exception k)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(k.Message);
            }
            return pusto;
        }

        public static int h = 0;

        public static bool Log_pusto(string _ways, bool pusto)
        {
            bool del = true;
            bool sysh = false;
           
            if (!pusto)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                try
                {
                    using (StreamReader sr = new StreamReader(path + "\\TestWay" + _ways))
                    {
                        string line = sr.ReadLine();

                        if (Directory.Exists(line))//del
                        {
                            sysh = true;
                            Form1.F2[h] = line;
                            h++;
                        }
                        else
                        {
                            del = false;
                        }

                    }
                    if (!del)
                    {
                        File.Delete(path + "\\TestWay" + _ways);

                    }
                }
                catch (Exception k)
                {
                    // Let the user know what went wrong.
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(k.Message);
                }
            }
            return sysh;
        }

        public static string[] Forma2_()
        {
            string[] F2=new string[8];

            Form2 form2 = new Form2();
            form2.ShowDialog();
            
            if (Form2.close_all == true)
            {
                return null;
            }
            F2[0] = Form2.textbox1_;
            F2[1] = Form2.textbox2_;
            F2[2] = Form2.textbox3_;
            F2[3] = Form2.textbox4_;
            F2[4] = Form2.textbox5_;
            F2[5] = Form2.textbox6_;
            F2[6] = Form2.textbox7_;
            F2[7] = Form2.textbox8_;
            int g = 0;
            foreach (string t in F2)
            {
                if (Directory.Exists(t))//del
                {
                    g++;
                }
                else
                {
                    if (t.Contains("\\Done"))
                    {
                      Directory.CreateDirectory(t);
                    }
                    else
                    {
                        F2[g] = null;
                        g++;
                    }
                }
            }

            return F2;
        }

        public static void Zap_(string[] _ways, string[] F2, int k_tr)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (k_tr != 8 && k_tr < 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    using (StreamWriter sw = new StreamWriter(path + "\\TestWay" + _ways[i]))
                    {
                        sw.WriteLine("{0}", F2[i]);
                    }
                }
            }
            Form1.Log_ways = F2[0];
            Form1.Log_ways_peremesti = F2[1];
            Form1.Zamech_ways = F2[2];
            Form1.Zamech_ways_peremesti = F2[3];
            Form1.Proverka_ways = F2[4];
            Form1.Proverka_ways_perem = F2[5];
            Form1.Protocol_ways = F2[6];
            Form1.Protocol_saved = F2[7];
        }
    }
}
