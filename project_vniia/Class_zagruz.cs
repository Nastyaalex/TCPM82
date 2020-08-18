using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace project_vniia
{
    class Class_zagruz
    {
        public static string Try_(string conString, OpenFileDialog openFileDialog1)
        {
            bool del = true;

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!Directory.Exists(path + "\\TestWay"))
                Directory.CreateDirectory(path + "\\TestWay");
            if (!File.Exists(path + "\\TestWay\\savings.txt"))
            {
                File.Create(path + "\\TestWay\\savings.txt").Close();
            }

            try
            {
                bool zap = false;
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(path + "\\TestWay\\savings.txt"))
                {
                    string line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    if ((line = sr.ReadLine()) == null)
                    {
                        if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + openFileDialog1.FileName;
                            zap = true;
                        } 
                        else
                        {
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        do
                        {
                            int g = 0;
                            try
                            {
                                OleDbConnection dbCon = new OleDbConnection(line);
                                dbCon.Open();
                                g = 1;
                                dbCon.Close();
                            }
                            catch (Exception p)
                            {
                                Console.WriteLine(p.Message);
                                del = false;
                            }

                            if(g==1)
                            {
                                Console.WriteLine(line);
                                conString = line;
                            }
                        
                        }
                        while ((line = sr.ReadLine()) != null);
                    }

                }
                if (zap)
                {
                    using (StreamWriter sw = new StreamWriter(path + "\\TestWay\\savings.txt"))
                    {
                        sw.WriteLine("{0}", conString);
                    }
                }
                if (!del)
                    File.Delete(path + "\\TestWay\\savings.txt");
            }
            catch (Exception k)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(k.Message);
            }
            return conString;
        }

        public static void Combobox_(string conString, ComboBox comboBox, DataSet ds, Form1.MyDB myDB, Dictionary<string, Form1.MyDB> myDBs)
        {
            comboBox.Items.Clear();
            OleDbConnection dbCon = new OleDbConnection(conString);
            dbCon.Open();
            DataTable tbls = dbCon.GetSchema("Tables", new string[] { null, null, null, "TABLE" }); //список всех таблиц
            foreach (DataRow row in tbls.Rows)
            {
                string TableName = row["TABLE_NAME"].ToString();
                comboBox.Items.Add(TableName);
            };
            
            foreach (string str in comboBox.Items)
            {
                OleDbDataAdapter dataAdapter_ = new OleDbDataAdapter(String.Format("SELECT * FROM {0}", "[" + str + "]"), conString);
                dataAdapter_.Fill(ds, str);

                Form1.MyDB myDb = new Form1.MyDB();
                myDBs["[" + str + "]"] = myDb;
                myDb.adapter = dataAdapter_;
                myDb.table = ds.Tables[str];
            }
            comboBox.SelectedItem = comboBox.Items[0];
            comboBox.Items.RemoveAt(1);
            dbCon.Close();
        }

    }
}
