using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace project_vniia
{
    class Class_Save_rabotBD
    {
        static void CompareRows_rabotBD(DataTable table_del, DataTable table_in, OleDbDataAdapter adapter, DataTable table_up, Dictionary<string, Form1.MyEnd> myEnds)
        {

            foreach (DataRow row1 in table_del.Rows)
            {
                int k = 0;
                foreach (DataRow row2 in table_in.Rows)
                {
                    if (k != 2)
                    {
                        var array1 = row1.ItemArray;
                        var array2 = row2.ItemArray;

                        if ((array1[0].ToString() == array2[0].ToString()) && (array1[1].ToString() == array2[1].ToString()))
                        {
                            table_up.LoadDataRow(row2.ItemArray, true);
                            row2.Delete();
                            row1.Delete();
                            k = 2;
                        }
                    }

                }
                table_in.AcceptChanges();
            }
            table_del.AcceptChanges();

            Form1.MyEnd myEnd = new Form1.MyEnd();
            myEnds["Работы по БД"] = myEnd;
            myEnd.del = table_del.Rows.Count;
            myEnd.dob = table_in.Rows.Count;
            myEnd.izm = table_up.Rows.Count;

            OleDbConnection dbCon = new OleDbConnection(Form1.conString);
            dbCon.Open();
            foreach (DataRow row_ in table_up.Rows)
            {
                var array1 = row_.ItemArray;
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "UPDATE `Работы по БД` SET `Номер изделия` = ?, `Выполняемая работа` = ?, `Отметка` = ?," +
                    " `s_ColLineage` = ?, `s_Generation` = ?, `s_GUID` = ?, `s_Lineage` = ? " +
                    "WHERE ((`Номер записи` = ?) AND (`Номер изделия` = ?)) ";

                cmd.Parameters.AddWithValue("@NumberB", array1[1]);
                cmd.Parameters.AddWithValue("@Rabota", array1[2]);
                cmd.Parameters.AddWithValue("@Otmetka", array1[3]);
                cmd.Parameters.AddWithValue("@s_C", array1[4]);
                cmd.Parameters.AddWithValue("@s_G", array1[5]);
                cmd.Parameters.AddWithValue("@s_GUID", array1[6]);
                cmd.Parameters.AddWithValue("@s_L", array1[7]);
                cmd.Parameters.AddWithValue("@ID", array1[0]);
                cmd.Parameters.AddWithValue("@NumberB", array1[1]);

                cmd.Connection = dbCon;
                cmd.ExecuteNonQuery();
            }

            foreach (DataRow row_ in table_del.Rows)
            {
                var array1 = row_.ItemArray;
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM `Работы по БД` WHERE ((`Номер записи` = ?) AND (`Номер изделия` = ?))";

                cmd.Parameters.AddWithValue("@Number", array1[0]);
                cmd.Parameters.AddWithValue("@NumberBD", array1[1]);

                cmd.Connection = dbCon;
                cmd.ExecuteNonQuery();

            }

            foreach (DataRow row_ in table_in.Rows)
            {
                var array1 = row_.ItemArray;
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO `Работы по БД` (`Номер изделия`, `Выполняемая работа`, " +
                    "`Отметка`, `s_ColLineage`,`s_Generation`, `s_GUID`, `s_Lineage`) VALUES (?, ?, ?, ?, ?, ?, ?)";

                cmd.Parameters.AddWithValue("@NumberB", array1[1]);
                cmd.Parameters.AddWithValue("@Rabota", array1[2]);
                cmd.Parameters.AddWithValue("@Otmetka", array1[3]);
                cmd.Parameters.AddWithValue("@s_C", array1[4]);
                cmd.Parameters.AddWithValue("@s_G", array1[5]);
                cmd.Parameters.AddWithValue("@s_GUID", array1[6]);
                cmd.Parameters.AddWithValue("@s_L", array1[7]);
                
                cmd.Connection = dbCon;
                cmd.ExecuteNonQuery();

            }
            dbCon.Close();

        }

        public static void AnalizTable(DataTable First, DataTable Second, OleDbDataAdapter adapter, Dictionary<string, Form1.MyEnd> myEnds)
        {//сравнение 2-х таблиц
            DataTable table = new DataTable("Различия");
            DataTable table1 = new DataTable("Различия1");
            DataTable table_up = new DataTable("UPDATE");

            using (DataSet ds = new DataSet())
            {
                //Добавление таблиц в DS
                ds.Tables.AddRange(new DataTable[] { First.Copy(), Second.Copy() });

                //Получение столбцов для DataRelation (1-я таблица)
                DataColumn[] firstcolumns = new DataColumn[ds.Tables[0].Columns.Count];
                for (int i = 0; i < firstcolumns.Length; i++)
                {
                    firstcolumns[i] = ds.Tables[0].Columns[i];
                }

                //Получение столбцов для DataRelation (2-я таблица)
                DataColumn[] secondcolumns = new DataColumn[ds.Tables[1].Columns.Count];
                for (int i = 0; i < secondcolumns.Length; i++)
                {
                    secondcolumns[i] = ds.Tables[1].Columns[i];
                }

                //Создание DataRelation (отношений)
                DataRelation r1 = new DataRelation(string.Empty, firstcolumns, secondcolumns, false);
                ds.Relations.Add(r1);
                DataRelation r2 = new DataRelation(string.Empty, secondcolumns, firstcolumns, false);
                ds.Relations.Add(r2);

                //Создание столбцов результирующей таблицы
                table = First.Clone();
                table1 = First.Clone();

                table.BeginLoadData();
                table1.BeginLoadData();

                table_up = First.Clone();
                table_up.BeginLoadData();
                //Если строки из 1-й нет во 2-й, то добавляем в результирующую таблицу
                foreach (DataRow parentrow in ds.Tables[0].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r1);
                    if (childrows == null || childrows.Length == 0)
                        table.LoadDataRow(parentrow.ItemArray, true);
                }
                //table.Rows.Add(000, "Akademic", "Iangal");

                //Если строки из 2-й нет в 1-й, то добавляем в результирующую таблицу
                foreach (DataRow parentrow in ds.Tables[1].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r2);
                    if (childrows == null || childrows.Length == 0)
                        table1.LoadDataRow(parentrow.ItemArray, true);
                }

                table.EndLoadData();
                table1.EndLoadData();
            }
            CompareRows_rabotBD(table, table1, adapter, table_up, myEnds);
            
        }
    }
}
