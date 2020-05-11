using System.Data;
using System.Data.OleDb;

namespace project_vniia
{
    class Class_Save_cannote
    {
        static void CompareRows_cannote(DataTable table_del, DataTable table_in, OleDbDataAdapter adapter, DataTable table_up)
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

                        if (array1[1].ToString() == array2[1].ToString())
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

            OleDbConnection dbCon = new OleDbConnection(Form1.conString);
            dbCon.Open();
            foreach (DataRow row_ in table_up.Rows)
            {
                var array1 = row_.ItemArray;
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "UPDATE `CANNote` SET `Номер КАН` = ?, `Номер Замечания` = ?," +
                    " `s_ColLineage` = ?, `s_Generation` = ?, `s_GUID` = ?, `s_Lineage` = ? " +
                    "WHERE `Номер Замечания` = ?";
                cmd.Parameters.AddWithValue("@NumberKan", array1[0]);
                cmd.Parameters.AddWithValue("@NumberZamech", array1[1]);
                cmd.Parameters.AddWithValue("@s_C", array1[2]);
                cmd.Parameters.AddWithValue("@s_G", array1[3]);
                cmd.Parameters.AddWithValue("@s_GUID", array1[4]);
                cmd.Parameters.AddWithValue("@s_L", array1[5]);
                cmd.Parameters.AddWithValue("@ID", array1[1]);

                cmd.Connection = dbCon;
                cmd.ExecuteNonQuery();
            }

            foreach (DataRow row_ in table_del.Rows)
            {
                var array1 = row_.ItemArray;
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM `CANNote` WHERE `Номер Замечания` = ?";
                
                cmd.Parameters.AddWithValue("@NumberBD", array1[1]);
                
                cmd.Connection = dbCon;
                cmd.ExecuteNonQuery();

            }

            foreach (DataRow row_ in table_in.Rows)
            {
                var array1 = row_.ItemArray;
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO `CANNote` ( `Номер КАН`, `Номер Замечания`," +
                    " `s_ColLineage`, `s_Generation`, `s_GUID`, `s_Lineage`) " +
                    "VALUES (?, ?, ?, ?, ?, ?)";

                cmd.Parameters.AddWithValue("@NumberKan", array1[0]);
                cmd.Parameters.AddWithValue("@NumberZamech", array1[1]);
                cmd.Parameters.AddWithValue("@s_C", array1[2]);
                cmd.Parameters.AddWithValue("@s_G", array1[3]);
                cmd.Parameters.AddWithValue("@s_GUID", array1[4]);
                cmd.Parameters.AddWithValue("@s_L", array1[5]);
              
                cmd.Connection = dbCon;
                cmd.ExecuteNonQuery();

            }
            dbCon.Close();

        }

        public static void AnalizTable(DataTable First, DataTable Second, OleDbDataAdapter adapter)
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
            CompareRows_cannote(table, table1, adapter, table_up);
          
        }
    }
}
