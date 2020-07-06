using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_vniia
{
    class Item_Proverka
    {
        
        public static float b0 { get; private set; }
        public float b { get; private set; }
        public float b1 { get; private set; }
        public float b2 { get; private set; }
        public float b3 { get; private set; }
        public float b4 { get; private set; }
        public float b5 { get; private set; }
        public float b6 { get; private set; }
        public float b7 { get; private set; }
        
        public Item_Proverka(string str, int r)
        {
            string[] parts = str.Split(new char[] { ' ' });
            for(int i=0; i<parts.Length; i++)
            {
                if (Proverka.k == 2)
                {
                    Form1.Flags_1 = true;
                    break;
                }
                if (Form1.Flags == true || Form1.Flags_1 == true)
                {
                   int j = 0;
                    foreach (string p in parts)
                    {
                        float num;
                        if (p != "")
                        {
                                try
                                {
                                    string pp = p.Replace('.', ',');
                                    bool no = float.TryParse(pp, out num);

                                    if (no)
                                    {
                                        switch (j)
                                        {
                                        case 0:
                                            b = float.Parse(pp);
                                            b0 = b;
                                            j = j + 1;
                                            break;
                                        case 1:
                                            b1 =float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 2:
                                            b2 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 3:
                                            b3 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 4:
                                            b4 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 5:
                                            b5 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 6:
                                            b6 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 7:
                                            b7 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                    }
                                    //b = Convert.ToDouble(pp);
                                    if (j == r)
                                        break;
                                    }
                                }
                                catch (Exception l)
                                {
                                    Console.WriteLine(l.Message);
                                }
                            
                        }
                    }
                    if (j == r)
                    {
                        Form1.Flags_1 = false;
                        break;
                    }
                }
                if (parts[i].Contains("Погрешность")&& parts[i+1].Contains("порогов") && parts[i+2].Contains("дискриминации,%"))
                { Form1.Flags = true;
                    break; 
                }
                b0 = b;
                Form1.Flags = false;
            } 
             
        }
    }

    

    class Item_Prov_Obnarug
    {
        public static float b0 { get; private set; }
        public float b { get; private set; }
        public float b1 { get; private set; }
        public float b2 { get; private set; }
        public float b3 { get; private set; }
        public float b4 { get; private set; }
        public float b5 { get; private set; }
        public float b6 { get; private set; }
        public float b7 { get; private set; }

        public Item_Prov_Obnarug(string str, int r)
        {
            string[] parts = str.Split(new char[] { ' ' });
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].Contains("ПРОВЕРКА") && parts[i + 1].Contains("ОБНАРУЖЕНИЯ") && parts[i + 2].Contains("ЯМ"))
                {
                    Form1.Flags = true;
                    break;
                }
                if (parts[i].Contains("Сигнал") && parts[i + 1].Contains("(имп/с)"))
                {
                    Form1.Flags_1 = true;
                    break;
                }
                if (parts[i].Contains("Количество") && parts[i + 1].Contains("обнаружений"))
                {
                    Form1.Flags_1 = true;
                    break;
                }
                
                if (Form1.Flags == true && Form1.Flags_1 == true)
                {
                    int j = 0;
                    foreach (string p in parts)
                    {
                        float num;
                        if (p != "")
                        {
                            try
                            {
                                string pp = p.Replace('.', ',');
                                bool no = float.TryParse(pp, out num);

                                if (no)
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            b = float.Parse(pp);
                                            b0 = b;
                                            j = j + 1;
                                            break;
                                        case 1:
                                            b1 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 2:
                                            b2 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 3:
                                            b3 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 4:
                                            b4 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 5:
                                            b5 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 6:
                                            b6 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 7:
                                            b7 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                    }
                                    //b = Convert.ToDouble(pp);
                                    if (j == r)
                                        break;
                                }
                            }
                            catch (Exception l)
                            {
                                Console.WriteLine(l.Message);
                            }

                        }
                    }
                    if (j == r)
                    {
                        Form1.Flags_1 = false;
                        break;
                    }
                    
                }
            }
        }
    }

    class Item_Prov_Chuvstv
    {
        public static float b0 { get; set; }
        public static string istochnik { get; private set; }
        public float b { get; private set; }
        public float b1 { get; private set; }
        public float b2 { get; private set; }
        public float b3 { get; private set; }
        public float b4 { get; private set; }
        public float b5 { get; private set; }
        public float b6 { get; private set; }
        public float b7 { get; private set; }

        public Item_Prov_Chuvstv(string str, int r)
        {
            string[] parts = str.Split(new char[] { ' ' });
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].Contains("ПРОВЕРКА") && parts[i + 1].Contains("ЧУВСТВИТЕЛЬНОСТИ") && parts[i + 2].Contains("БД"))
                {
                    Form1.Flags = true;
                    break;
                }
                if (parts[i].Contains("Тип") && parts[i + 1].Contains("источника:Cs") || parts[i].Contains("Тип") && parts[i + 1].Contains("источника:U")|| parts[i].Contains("Тип") && parts[i + 1].Contains("источника:Pu"))
                {
                    Form1.Flags_1 = true;
                    istochnik = parts[i+1];
                    break;
                }
                try
                {
                    if (parts[i].Contains("Чувствительность") && parts[i + 1].Contains("(имп/(с*г)"))
                    {
                        Form1.Flags_ = true;
                        break;
                    }
                }
                catch (Exception p)
                { Console.WriteLine(p.Message); }

                if (parts[i].Contains("Нестабильность") && parts[i + 1].Contains("фона") && parts[i+2].Contains("Nf1/Nf2:"))
                {
                    Form1.Flags_ = true;
                    Form1.Flags_1 = true;
                    istochnik = parts[i];
                    break;
                }

                if (Form1.Flags == true && Form1.Flags_1 == true && Form1.Flags_== true)
                {
                    int j = 0;
                    foreach (string p in parts)
                    {
                        float num;
                        if (p != "")
                        {
                            try
                            {
                                string pp = p.Replace('.', ',');
                                if (pp.Contains(":"))
                                {
                                    pp = pp.Substring(pp.IndexOf(':')+1);
                                }
                                bool no = float.TryParse(pp, out num);

                                if (no)
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            b = float.Parse(pp);
                                            b0 = b;
                                            j = j + 1;
                                            break;
                                        case 1:
                                            b1 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 2:
                                            b2 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 3:
                                            b3 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 4:
                                            b4 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 5:
                                            b5 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 6:
                                            b6 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 7:
                                            b7 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                    }
                                    //b = Convert.ToDouble(pp);
                                    if (j == r)
                                        break;
                                }
                            }
                            catch (Exception l)
                            {
                                Console.WriteLine(l.Message);
                            }

                        }
                    }
                    if (j == r)
                    {
                        Form1.Flags_1 = false;
                        Form1.Flags_ = false;
                        break;
                    }

                }
            }
        }
    }

    class Item_Prov_LognS
    {
        
        public static float b0 { get; set; }
        public float b { get; private set; }
        public float b1 { get; private set; }
        public float b2 { get; private set; }
        public float b3 { get; private set; }
        public float b4 { get; private set; }
        public float b5 { get; private set; }
        public float b6 { get; private set; }
        public float b7 { get; private set; }

        public Item_Prov_LognS(string str, int r)
        {
            string[] parts = str.Split(new char[] { ' ' });
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].Contains("ПРОВЕРКА") && parts[i + 1].Contains("НА") && parts[i + 2].Contains("ЛОЖНЫЕ") && parts[i + 3].Contains("СРАБАТЫВАНИЯ"))
                {
                    Form1.Flags = true;
                    break;
                }
                if (Form1.Flags == true && parts[i].Contains("Нестабильность") && parts[i + 1].Contains("фона") && parts[i + 2].Contains("Nf1/Nf2"))
                {
                    Proverka.Logn = true;
                    break;
                }
                //if (parts[i].Contains("Количество") && parts[i + 1].Contains("ложных") && parts[i + 2].Contains("срабатываний") && parts[i + 3].Contains("за")
                //    && parts[i + 4].Contains("5000") && parts[i + 5].Contains("измерений") && parts[i + 6].Contains("по") && parts[i + 7].Contains("1с"))
                //{
                    if (parts[i].Contains("Количество") && parts[i + 1].Contains("ложных") && parts[i + 2].Contains("срабатываний") && parts[i + 3].Contains("за")
                     && parts[i + 5].Contains("измерений") && parts[i + 6].Contains("по") && parts[i + 7].Contains("1с"))
                {
                    Form1.Flags_1 = true;
                    break;
                }
                if (Form1.Flags == true && Form1.Flags_1 == true)
                {
                    int j = 0;
                    foreach (string p in parts)
                    {
                        float num;
                        if (p != "")
                        {
                            try
                            {
                                string pp = p.Replace('.', ',');
                                if (pp.Contains(":"))
                                {
                                    pp = pp.Substring(pp.IndexOf(':') + 1);
                                }
                                bool no = float.TryParse(pp, out num);

                                if (no)
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            b = float.Parse(pp);
                                            b0 = 1022;
                                            j = j + 1;
                                            break;
                                        case 1:
                                            b1 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 2:
                                            b2 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 3:
                                            b3 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 4:
                                            b4 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 5:
                                            b5 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 6:
                                            b6 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                        case 7:
                                            b7 = float.Parse(pp);
                                            j = j + 1;
                                            break;
                                    }
                                    //b = Convert.ToDouble(pp);
                                    if (j == r)
                                        break;
                                }
                            }
                            catch (Exception l)
                            {
                                Console.WriteLine(l.Message);
                            }

                        }
                    }
                    if (j == r)
                    {
                        Form1.Flags_1 = false;
                        break;
                    }
                }
            }
        }
    }

    class Proverka
    {
        public static int k = 0;
        
        public static string[] parts;
        public double b { get; private set; }
        public static bool Logn = false;

        public int Kolvo(Item_Proverka item_o)
        {
            int h = 0;
            if (item_o.b != 0)
                h++;
            if (item_o.b1 != 0)
                h++;
            if (item_o.b2 != 0)
                h++;
            if (item_o.b3 != 0)
                h++;
            if (item_o.b4 != 0)
                h++;
            if (item_o.b5 != 0)
                h++;
            if (item_o.b6 != 0)
                h++;
            if (item_o.b7 != 0)
                h++;
            return h;
        }
        public void Main_Proverka(Form1 form1, DataTable First)
        {
            List<Item_Proverka> items = new List<Item_Proverka>();
            List<string> Fil = Directory.GetFiles(Form1.Proverka_ways, "*.txt").ToList<string>();
            foreach (var fil in Fil)
            {
                string[] allStringFromFile = File.ReadAllLines(fil, Encoding.Default);

                int len = allStringFromFile.Length;

                items.Clear();

                string name = Path.GetFileNameWithoutExtension(fil); // returns File
                int r=8;
                 parts = name.Split(new char[] {'_'});
                int rr = parts.Length;
                for (int i = 0; i < len; i++)
                {
                    if (k == 4)
                        break;
                    
                    items.Add(new Item_Proverka(allStringFromFile[i], r));
                   
                    if (Item_Proverka.b0 != 0 || k == 2)
                    {
                        if (k == 0)
                        {
                            Item_Proverka item_o = items.LastOrDefault();
                            r = Kolvo(item_o);
                            Form1.Flags = true;
                        }
                        k++;
                    }

                }
                DataTable table = new DataTable("Добавление_проверок");
                table = First.Clone();
                table.BeginLoadData();

                float[] bbb = new float[8];
                float[] chuvstvit = new float[8];

                string curDate = DateTime.Now.ToShortDateString();

                foreach (Item_Proverka item_ in items)
                {
                    if (item_.b1 != 0)
                    {
                        switch(k)
                        {
                            case 4:
                                bbb[0] = item_.b;
                                bbb[1] = item_.b1;
                                bbb[2] = item_.b2;
                                bbb[3] = item_.b3;
                                bbb[4] = item_.b4;
                                bbb[5] = item_.b5;
                                bbb[6] = item_.b6;
                                bbb[7] = item_.b7;
                                break;
                            case 5:
                                if (item_.b > bbb[0]) { bbb[0] = item_.b; }
                                if (item_.b1 > bbb[1]) { bbb[1] = item_.b1; }
                                if (item_.b2 > bbb[2]) { bbb[2] = item_.b2; }
                                if (item_.b3 > bbb[3]) { bbb[3] = item_.b3; }
                                if (item_.b4 > bbb[4]) { bbb[4] = item_.b4; }
                                if (item_.b5 > bbb[5]) { bbb[5] = item_.b5; }
                                if (item_.b6 > bbb[6]) { bbb[6] = item_.b6; }
                                if (item_.b7 > bbb[7]) { bbb[7] = item_.b7; }
                                break;
                            case 6:
                                chuvstvit[0] = item_.b;
                                chuvstvit[1] = item_.b1;
                                chuvstvit[2] = item_.b2;
                                chuvstvit[3] = item_.b3;
                                chuvstvit[4] = item_.b4;
                                chuvstvit[5] = item_.b5;
                                chuvstvit[6] = item_.b6;
                                chuvstvit[7] = item_.b7;

                                if (rr != 1)
                                {
                                    for (int i = 0; i < r; i++)
                                    {
                                        DataRow mynewrow = table.NewRow();
                                        mynewrow["Номер БД"] = parts[i];
                                        mynewrow["Пороги"] = bbb[i];
                                        mynewrow["S Cs 10 см"] = chuvstvit[i];
                                        mynewrow["Дата проверки"] = curDate;
                                        mynewrow["Примечание"] = "В системе " + " ?дополнить";
                                        table.Rows.Add(mynewrow);
                                        
                                    }
                                }
                                else
                                {
                                    if (r == 1)
                                    {
                                        for (int i = 0; i < r; i++)
                                        {
                                            DataRow mynewrow = table.NewRow();
                                            mynewrow["Номер БД"] = name;
                                            mynewrow["Пороги"] = bbb[i];
                                            mynewrow["S Cs 10 см"] = chuvstvit[i];
                                            mynewrow["Дата проверки"] = curDate;
                                            table.Rows.Add(mynewrow);
                                        }
                                    }
                                    else
                                    {
                                        for (int i = 0; i < r; i++)
                                        {
                                            DataRow mynewrow = table.NewRow();
                                            mynewrow["Номер БД"] = "?дополнить";
                                            mynewrow["Пороги"] = bbb[i];
                                            mynewrow["S Cs 10 см"] = chuvstvit[i];
                                            mynewrow["Дата проверки"] = curDate;
                                            mynewrow["Примечание"] = "В системе " + name;
                                            table.Rows.Add(mynewrow);
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        k++;
                    }
                }

                items.Clear();

                List<Item_Prov_Obnarug> itemr = new List<Item_Prov_Obnarug>();
                itemr.Clear();
                Form1.Flags = false;

                for (int i = 0; i < len; i++)
                {
                    itemr.Add(new Item_Prov_Obnarug(allStringFromFile[i], r));
                }
                k = 0;
                
                foreach (Item_Prov_Obnarug item_ in itemr)
                {
                    if (item_.b1 != 0)
                    {
                        if (k == 1)
                        {
                            chuvstvit[0] = item_.b;
                            chuvstvit[1] = item_.b1;
                            chuvstvit[2] = item_.b2;
                            chuvstvit[3] = item_.b3;
                            chuvstvit[4] = item_.b4;
                            chuvstvit[5] = item_.b5;
                            chuvstvit[6] = item_.b6;
                            chuvstvit[7] = item_.b7;

                            for (int i = 0; i < r; i++)
                            {
                                table.Rows[i]["Q"] = bbb[i];
                                table.Rows[i]["Колличество срабатываний"] = chuvstvit[i];
                            }
                        }
                        else
                        {
                            bbb[0] = item_.b;
                            bbb[1] = item_.b1;
                            bbb[2] = item_.b2;
                            bbb[3] = item_.b3;
                            bbb[4] = item_.b4;
                            bbb[5] = item_.b5;
                            bbb[6] = item_.b6;
                            bbb[7] = item_.b7;
                            k++;
                        }
                       
                    }
                }

                itemr.Clear();
                //////////////////////////////////////
                List<Item_Prov_Chuvstv> items_ = new List<Item_Prov_Chuvstv>();
                items_.Clear();

                Form1.Flags = false;
                Form1.Flags_ = false;

                for (int i = 0; i < len; i++)
                {
                    items_.Add(new Item_Prov_Chuvstv(allStringFromFile[i], r));
                    if(Item_Prov_Chuvstv.b0 !=0)
                    {
                        Item_Prov_Chuvstv item_o = items_.LastOrDefault();

                        if (Item_Prov_Chuvstv.istochnik.Contains("Cs") && item_o.b!=0 && (item_o.b == Item_Prov_Chuvstv.b0))
                            {
                                bbb[0] = item_o.b;
                                bbb[1] = item_o.b1;
                                bbb[2] = item_o.b2;
                                bbb[3] = item_o.b3;
                                bbb[4] = item_o.b4;
                                bbb[5] = item_o.b5;
                                bbb[6] = item_o.b6;
                                bbb[7] = item_o.b7;

                                for ( int j = 0; j < r; j++)
                                {
                                    table.Rows[j]["S Cs 50 см"] = bbb[j];
                                }
                            }
                            if (Item_Prov_Chuvstv.istochnik.Contains("U") && item_o.b != 0 && (item_o.b == Item_Prov_Chuvstv.b0))
                            {
                                bbb[0] = item_o.b;
                                bbb[1] = item_o.b1;
                                bbb[2] = item_o.b2;
                                bbb[3] = item_o.b3;
                                bbb[4] = item_o.b4;
                                bbb[5] = item_o.b5;
                                bbb[6] = item_o.b6;
                                bbb[7] = item_o.b7;

                                for (int j = 0; j < r; j++)
                                {
                                    table.Rows[j]["S U 50 см"] = bbb[j];
                                }
                            }
                            if (Item_Prov_Chuvstv.istochnik.Contains("Pu") && item_o.b != 0 && (item_o.b == Item_Prov_Chuvstv.b0))
                            {
                                bbb[0] = item_o.b;
                                bbb[1] = item_o.b1;
                                bbb[2] = item_o.b2;
                                bbb[3] = item_o.b3;
                                bbb[4] = item_o.b4;
                                bbb[5] = item_o.b5;
                                bbb[6] = item_o.b6;
                                bbb[7] = item_o.b7;

                                for (int j = 0; j < r; j++)
                                {
                                    table.Rows[j]["S Pu 50 см"] = bbb[j];
                                }
                            }

                            if (Item_Prov_Chuvstv.istochnik.Contains("Нестабильность") && item_o.b != 0 && (item_o.b == Item_Prov_Chuvstv.b0))
                            {
                                bbb[0] = item_o.b;
                                bbb[1] = item_o.b1;
                                bbb[2] = item_o.b2;
                                bbb[3] = item_o.b3;
                                bbb[4] = item_o.b4;
                                bbb[5] = item_o.b5;
                                bbb[6] = item_o.b6;
                                bbb[7] = item_o.b7;

                                for (int j = 0; j < r; j++)
                                {
                                    table.Rows[j]["Нестабильность фона"] = bbb[j];
                                }
                                break;
                            }
                        
                        Item_Prov_Chuvstv.b0 = 0;
                        
                    }
                }
                items_.Clear();
                ////////////////////////////////

                List<Item_Prov_LognS> item_s = new List<Item_Prov_LognS>();
                item_s.Clear();

                Form1.Flags = false;
                Form1.Flags_1 = false;

                for (int i = 0; i < len; i++)
                {
                    item_s.Add(new Item_Prov_LognS(allStringFromFile[i], r));
                    
                    if (Item_Prov_LognS.b0 == 1022)
                    {
                        Item_Prov_LognS item_o = item_s.LastOrDefault();
                        bbb[0] = item_o.b;
                        bbb[1] = item_o.b1;
                        bbb[2] = item_o.b2;
                        bbb[3] = item_o.b3;
                        bbb[4] = item_o.b4;
                        bbb[5] = item_o.b5;
                        bbb[6] = item_o.b6;
                        bbb[7] = item_o.b7;

                    }
                    Item_Prov_LognS.b0 = 0;
                    if (Logn == true)
                    {
                        for (int j = 0; j < r; j++)
                        {
                            table.Rows[j]["Ложные срабатывания"] = bbb[j];
                        }
                    }
                }
                ////////////////////////////////////////
                var conn_tabl = new OleDbConnection(Form1.conString);
                conn_tabl.Open();

                foreach (DataRow row_ in table.Rows)
                {
                    var array1 = row_.ItemArray;
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO `Проверка` (`Номер БД`, `Тип проверки`, `Дата проверки`, " +
                    "`Канал Cs`, `Канал Св`, `Пороги`, `S Cs 10 см`, `S Cs 50 см`, `S U 50 см`," +
                    " `S Pu 50 см`, `Нестабильность фона`, `Колличество срабатываний`, `Q`," +
                    " `Ложные срабатывания`, `Примечание`, `s_ColLineage`," +
                    " `s_Generation`, `s_GUID`, `s_Lineage`) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    cmd.Parameters.AddWithValue("@NumberB", array1[1]);
                    cmd.Parameters.AddWithValue("@Type", array1[2]);
                    cmd.Parameters.AddWithValue("@Data", array1[3]);
                    cmd.Parameters.AddWithValue("@Cs", array1[4]);
                    cmd.Parameters.AddWithValue("@Cv", array1[5]);
                    cmd.Parameters.AddWithValue("@Porog", array1[6]);
                    cmd.Parameters.AddWithValue("@S_Cs", array1[7]);
                    cmd.Parameters.AddWithValue("@S_Cs_5", array1[8]);
                    cmd.Parameters.AddWithValue("@S_U", array1[9]);
                    cmd.Parameters.AddWithValue("@S_Pu", array1[10]);
                    cmd.Parameters.AddWithValue("@Fon", array1[11]);
                    cmd.Parameters.AddWithValue("@Kolvo", array1[12]);
                    cmd.Parameters.AddWithValue("@Q", array1[13]);
                    cmd.Parameters.AddWithValue("@L_srab", array1[14]);
                    cmd.Parameters.AddWithValue("@Prim", array1[15]);
                    cmd.Parameters.AddWithValue("@s_C", array1[16]);
                    cmd.Parameters.AddWithValue("@s_G", array1[17]);
                    cmd.Parameters.AddWithValue("@s_GUID", array1[18]);
                    cmd.Parameters.AddWithValue("@s_L", array1[19]);
                    
                    cmd.Connection = conn_tabl;
                    cmd.ExecuteNonQuery();

                }

                conn_tabl.Close();

                string file = Path.GetFileName(fil);
                string newPath = Path.Combine(Form1.Proverka_ways_perem, file);
                File.Move(fil, newPath);
            }
        } 
    }
}
