using System;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Office.Interop.Word;

namespace project_vniia
{
    class Class_Protokol
    {

        static Word._Document document;
        public static Word.Range FindMethod(object _missingObj, Word._Document _document, object stringToFind)
        {
            object stringToFindObj = stringToFind;
            Word.Range wordRange;
            bool rangeFound;

            for (int i = 1; i <= _document.Sections.Count; i++)
            {
                wordRange = _document.Sections[i].Range;
                Word.Find wordFindObj = wordRange.Find;
                object[] wordFindParameters = new object[15] { stringToFindObj, _missingObj, _missingObj,
                    _missingObj, _missingObj,_missingObj, _missingObj,_missingObj, _missingObj,
                    _missingObj, _missingObj,_missingObj, _missingObj,_missingObj, _missingObj};

                rangeFound = (bool)wordFindObj.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, wordFindObj, wordFindParameters);
                if (rangeFound)
                {
                    return wordRange;
                }
            }
            return null;
        }
        public static void Protokol(string srtNumberSystem, string srtTypeSystem, Object templatePathObj, string[,] blocks,
            List<string> BL_type, string[,] blocksN, string[,] znach_SU50, string[,] znach_SCs50, string[,] znach_SCs10, 
            string[,] znach_SCfCm, Object templatePathObj_1)
        {
            Word._Application application;
            Object missingObj = System.Reflection.Missing.Value;
            Object trueObj = true;
            Object falseObj = false;

            application = new Word.Application();
            try
            {
                document = application.Documents.Add(ref templatePathObj, ref missingObj, ref missingObj, ref missingObj);
            }
            catch (Exception p)
            {
                document.Close(ref falseObj, ref missingObj, ref missingObj);
                application.Quit(ref missingObj, ref missingObj, ref missingObj);
                document = null;
                application = null;
                throw p;
            }
            application.Visible = true;
            //Найти и изменить данные
            string stringToFind = "изделий Система радиационного мониторинга ТСРМ82-09.07	";
            Word.Range findRange = FindMethod(missingObj, document, stringToFind);
            findRange.Font.Underline = WdUnderline.wdUnderlineSingle;
            findRange.Text = "изделий Система радиационного мониторинга "+ srtTypeSystem;

            stringToFind = "заводские № 55918528, 55918530, 55918531			";
            findRange = FindMethod(missingObj, document, stringToFind);
            findRange.Font.Underline = WdUnderline.wdUnderlineSingle;
            findRange.Text = "заводские № " + srtNumberSystem + "			";

            stringToFind = "      Изделие (я)	ТСРМ82-09.07				 зав. № 55918528, 55918530, 55918531				__	  ";
            findRange = FindMethod(missingObj, document, stringToFind);
            findRange.Font.Underline = WdUnderline.wdUnderlineSingle;
            findRange.Text = "      Изделие (я)	"+ srtTypeSystem + "    	 зав. № "+ srtNumberSystem ;
            
            Tables_(document, srtNumberSystem, srtTypeSystem, blocks, BL_type, missingObj, blocksN, znach_SU50, znach_SCs50, znach_SCs10, znach_SCfCm);
            //Сохранение
            //string tet = "C:\\Users\\APM\\Desktop\\doc1";
            Save(missingObj, document, templatePathObj_1);
        }
        public static void Save(object _missingObj, Word._Document _document, object pathToSaveString)
        {
            try
            {
                Object pathToSaveObj = pathToSaveString;
                _document.SaveAs(ref pathToSaveObj, Word.WdSaveFormat.wdFormatDocument,
                    ref _missingObj, ref _missingObj,
                    ref _missingObj, ref _missingObj,
                    ref _missingObj, ref _missingObj,
                    ref _missingObj, ref _missingObj,
                    ref _missingObj, ref _missingObj,
                    ref _missingObj, ref _missingObj,
                    ref _missingObj, ref _missingObj);
            }
            catch(Exception p)
            {
                MessageBox.Show(p.ToString());
            }
        }

        public static void Tables_(Word._Document _document, string srtNumberSystem, string srtTypeSystem, string[,] blocks,
            List<string> BL_type, object _missingObj, string[,] blocksN, string[,] znach_SU50, string[,] znach_SCs50,
            string[,] znach_SCs10, string[,] znach_SCfCm)
        {
            var par = srtNumberSystem.Split(',');
            for (int i = 0; i < par.Length; i++)
            {
                if (par[i].Contains(" "))
                {
                    char[] Mychar = { ',', ' ' };
                    par[i] = par[i].TrimStart(Mychar);
                }
            }

            int _length = 0;
            if (par.Length > 2)
            {
                foreach (var block in blocks)
                {
                    _length++;
                }
            }
            int tableNumber = 1;
            Word.Table _table = _document.Tables[tableNumber];
            _length = _length + 3 - _table.Rows.Count;
            if (_length > 0)
            {
                for (int o = 0; o < _length; o++)
                {
                    _table.Rows.Add(ref _missingObj);
                }
            }
            //Cell cell = _table.Cell(24, 1);
            //GetR(cell, _application);

            _table.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

            int v = _table.Rows.Count;
            for (int o = _length; o > 0; o--)
            {
                _table.Cell(v, 1).Merge(_table.Cell(v - 7, 1));
                v = v - 8;
                o = o - 7;
            }
            v = _table.Rows.Count;
            for (int o = _length; o > 0; o--)
            {
                for (int r = 2; r < 9; r++)
                {
                    var _currentRange = _table.Cell(v, r).Range;
                    _currentRange.Text = "-";
                }
                v--;
            }
            int rowIndex = 4, columnIndex = 3;
            int kolvo = 0;
            foreach (var typee in blocks)
            {
                if (typee == null)
                { }
                else
                {
                    if (typee != "")
                    {
                        if (kolvo >= 8)
                            kolvo = 0;
                        var _currentRange = _table.Cell(rowIndex, columnIndex).Range;
                        _currentRange.Text = typee;
                        _currentRange = _table.Cell(rowIndex, columnIndex + 1).Range;
                        _currentRange.Text = kolvo.ToString();
                    }
                }
                kolvo++;
                rowIndex = rowIndex + 1;
            }
            rowIndex = 4; columnIndex = 3;
            kolvo = 8;
            foreach (var net in blocksN)
            {
                if (net == "" || net == null)
                { }
                else
                {
                    if (kolvo >= 16)
                        kolvo = 8;
                    var _currentRange2 = _table.Cell(rowIndex, columnIndex + 2).Range;
                    _currentRange2.Text = net;
                    _currentRange2 = _table.Cell(rowIndex, columnIndex + 3).Range;
                    _currentRange2.Text = kolvo.ToString();
                }
                kolvo++;
                rowIndex = rowIndex + 1;
            }
            rowIndex = 4; columnIndex = 1;

            foreach (var par_ in par)
            {
                var _currentRange = _table.Cell(rowIndex, columnIndex).Range;
                _currentRange.Text = par_;
                _currentRange = _table.Cell(rowIndex, columnIndex + 1).Range;
                _currentRange.Text = par_;
                rowIndex = rowIndex + 8;
            }

            rowIndex = 2; columnIndex = 3;
            try
            {
                foreach (var y in BL_type)
                {
                    var _currentRange = _table.Cell(rowIndex, columnIndex).Range;
                    _currentRange.Text = y;
                    columnIndex = columnIndex + 1;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
            /////////////////////////////////
            //////tabl 4
            ////////////////////////////////
            tableNumber = 4;
            Word.Table _table4 = _document.Tables[tableNumber];
            columnIndex = 1;
            string soxr = "", soxr2 = "", zifr = "", zifr1 = "";
            int p = 0;
            foreach (var par_ in par)
            {
                rowIndex = 2;
                var _currentRange = _table4.Cell(rowIndex, columnIndex).Range;
                _currentRange.Text = par_;
                var _currentRange1 = _table4.Cell(rowIndex + 3, columnIndex).Range;
                var _currentRange2 = _table4.Cell(rowIndex + 5, columnIndex).Range;
                var _currentRange3 = _table4.Cell(16, columnIndex).Range;
                var _currentRange4 = _table4.Cell(25, columnIndex).Range;
                if (_currentRange1.Text != "-\r\a")
                {
                    soxr = _currentRange1.Text;
                    zifr = _currentRange2.Text;
                    soxr = soxr.Replace("\r\a", " ");
                    zifr1 = _currentRange3.Text;
                    soxr2 = _currentRange4.Text;
                    soxr2 = soxr2.Replace("\r\a", " ");
                }
                else
                {
                    _currentRange1.Text = soxr;
                    _currentRange2.Text = zifr;
                    _currentRange3.Text = zifr1;
                    _currentRange4.Text = soxr2;
                }
                for (int j = 1; j < 8; j++)
                {
                    if (blocks[p, j] != "")
                    {
                        rowIndex++;
                        _currentRange2 = _table4.Cell(rowIndex + 5, columnIndex).Range;
                        _currentRange3 = _table4.Cell(rowIndex + 14, columnIndex).Range;
                        _currentRange4 = _table4.Cell(rowIndex + 23, columnIndex).Range;
                        _currentRange2.Text = zifr;
                        _currentRange3.Text = zifr1;
                        _currentRange4.Text = soxr2;
                    }
                }
                p++;
                columnIndex = columnIndex + 1;
            }
            ///////////////////////////////
            ///таблица 8
            ///////////////////////////////
            tableNumber = 8;
            Word.Table _table8 = _document.Tables[tableNumber];
            columnIndex = 1; rowIndex = 2;
            foreach (var par_ in par)
            {
                var _currentRange = _table8.Cell(rowIndex, columnIndex).Range;
                _currentRange.Text = par_;
                for (int j=3;j<7;j++)
                {
                    var _currentRange_ = _table8.Cell(rowIndex+j, columnIndex).Range;
                    _currentRange_.Text = "";
                }
                columnIndex = columnIndex + 1;
            }
            ////////////////////////////
            ///таблица 6
            ///////////////////////////
            tableNumber = 6;
            Word.Table _table6 = _document.Tables[tableNumber];
            columnIndex = 1; rowIndex = 2;
            soxr = "";
            foreach (var par_ in par)
            {
                var _currentRange = _table6.Cell(rowIndex, columnIndex).Range;
                _currentRange.Text = par_;

                var _currentRange1 = _table6.Cell(_table6.Rows.Count - 1, columnIndex).Range;
                if (_currentRange1.Text != "-\r\a")
                {
                    soxr = _currentRange1.Text;
                    soxr = soxr.Replace("\r\a", " ");
                }
                else
                {
                    _currentRange1.Text = soxr;
                    var _currentRange2 = _table6.Cell(_table6.Rows.Count, columnIndex).Range;
                    _currentRange2.Text = soxr;
                }
                columnIndex = columnIndex + 1;
            }
            columnIndex = 1;
            p = 0;
            foreach (var par_ in par)
            {
                rowIndex = 6;
                for (int j = 0; j < 8; j++)
                {
                    if (blocks[p, j] != "" )
                    {
                        var _currentRange_2 = _table6.Cell(rowIndex, columnIndex).Range;
                        var _currentRange_3 = _table6.Cell(rowIndex +9, columnIndex).Range;
                        var _currentRange_4 = _table6.Cell(rowIndex + 9+9, columnIndex).Range;
                        var _currentRange_Cf = _table6.Cell(rowIndex + 9 + 9+9, columnIndex).Range;
                        if (znach_SU50[p, j]==null || znach_SU50[p, j] == "")
                        { }
                        else
                        _currentRange_2.Text = znach_SU50[p, j];
                        if (znach_SCs50[p, j] == null || znach_SCs50[p, j] == "")
                        { }
                        else
                            _currentRange_3.Text = znach_SCs50[p, j];
                        if (znach_SCs10[p, j] == null || znach_SCs10[p, j] == "")
                        { }
                        else
                            _currentRange_4.Text = znach_SCs10[p, j];
                        if (znach_SCfCm[p, j] == null || znach_SCfCm[p, j] == "")
                        { }
                        else
                            _currentRange_Cf.Text = znach_SCfCm[p, j];

                        rowIndex++;
                    }
                }
                p++;
                columnIndex = columnIndex + 1;
            }
        }

        /// <summary>
        /// /////Для разделения строк(объединённых)
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="_application"></param>
        /// <returns></returns>
        static int GetR(Cell cell, Word._Application _application)
        {
            cell.Select();
            return (int)_application.Selection.Information[WdInformation.wdEndOfRangeRowNumber] 
                - (int)_application.Selection.Information[WdInformation.wdStartOfRangeRowNumber] + 1;

        }
        public static void Protokol_for_PSI(string srtNumberSystem, string srtTypeSystem, Object templatePathObj, string[,] blocks,
           List<string> BL_type, string[,] blocksN, string[,] znach_SU50, string[,] znach_SCs50, string[,] znach_SCs10,
           string[,] znach_SCfCm, Object templatePathObj_1)
        {
            Word._Application application;
            Object missingObj = System.Reflection.Missing.Value;
            Object trueObj = true;
            Object falseObj = false;

            application = new Word.Application();
            try
            {
                document = application.Documents.Add(ref templatePathObj, ref missingObj, ref missingObj, ref missingObj);
            }
            catch (Exception p)
            {
                document.Close(ref falseObj, ref missingObj, ref missingObj);
                application.Quit(ref missingObj, ref missingObj, ref missingObj);
                document = null;
                application = null;
                throw p;
            }
            application.Visible = true;
            //Найти и изменить данные
            string stringToFind = "Система радиационного мониторинга ТСРМ82-09.07		";
            Word.Range findRange = FindMethod(missingObj, document, stringToFind);
            findRange.Font.Underline = WdUnderline.wdUnderlineSingle;
            findRange.Text = "Система радиационного мониторинга "+ srtTypeSystem+ "		";

            stringToFind = "55918528, 55918530, 55918531";
            findRange = FindMethod(missingObj, document, stringToFind);
            findRange.Font.Underline = WdUnderline.wdUnderlineSingle;
            findRange.Text = srtNumberSystem;
            
            Tables_for_PSI(document, srtNumberSystem, srtTypeSystem, blocks, BL_type, missingObj, blocksN, znach_SU50, znach_SCs50, znach_SCfCm);
            //Сохранение
            Save(missingObj, document, templatePathObj_1);
        }
        public static void Tables_for_PSI(Word._Document _document, string srtNumberSystem, string srtTypeSystem, string[,] blocks,
           List<string> BL_type, object _missingObj, string[,] blocksN, string[,] znach_SU50, string[,] znach_SCs50,
           string[,] znach_SCfCm)
        {
            var par = srtNumberSystem.Split(',');
            for (int i = 0; i < par.Length; i++)
            {
                if (par[i].Contains(" "))
                {
                    char[] Mychar = { ',', ' ' };
                    par[i] = par[i].TrimStart(Mychar);
                }
            }

            int _length = 0;
            if (par.Length > 2)
            {
                foreach (var block in blocks)
                {
                    _length++;
                }
            }
            int tableNumber = 1;
            Word.Table _table = _document.Tables[tableNumber];
            _length = _length + 3 - _table.Rows.Count;
            if (_length > 0)
            {
                for (int o = 0; o < _length; o++)
                {
                    _table.Rows.Add(ref _missingObj);
                }
            }
            
            _table.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            int v = _table.Rows.Count;
            for (int o = _length; o > 0; o--)
            {
                _table.Cell(v, 1).Merge(_table.Cell(v - 7, 1));
                v = v - 8;
                o = o - 7;
            }
            v = _table.Rows.Count;
            for (int o = _length; o > 0; o--)
            {
                for (int r = 2; r < 9; r++)
                {
                    var _currentRange = _table.Cell(v, r).Range;
                    _currentRange.Text = "-";
                }
                v--;
            }
            int rowIndex = 4, columnIndex = 3;
            int kolvo = 0;
            foreach (var typee in blocks)
            {
                if (typee == null)
                { }
                else
                {
                    if (typee != "")
                    {
                        if (kolvo >= 8)
                            kolvo = 0;
                        var _currentRange = _table.Cell(rowIndex, columnIndex).Range;
                        _currentRange.Text = typee;
                        _currentRange = _table.Cell(rowIndex, columnIndex + 1).Range;
                        _currentRange.Text = kolvo.ToString();
                    }
                }
                kolvo++;
                rowIndex = rowIndex + 1;
            }
            rowIndex = 4; columnIndex = 3;
            kolvo = 8;
            foreach (var net in blocksN)
            {
                if (net == "" || net == null)
                { }
                else
                {
                    if (kolvo >= 16)
                        kolvo = 8;
                    var _currentRange2 = _table.Cell(rowIndex, columnIndex + 2).Range;
                    _currentRange2.Text = net;
                    _currentRange2 = _table.Cell(rowIndex, columnIndex + 3).Range;
                    _currentRange2.Text = kolvo.ToString();
                }
                kolvo++;
                rowIndex = rowIndex + 1;
            }
            rowIndex = 4; columnIndex = 1;

            foreach (var par_ in par)
            {
                var _currentRange = _table.Cell(rowIndex, columnIndex).Range;
                _currentRange.Text = par_;
                _currentRange = _table.Cell(rowIndex, columnIndex + 1).Range;
                _currentRange.Text = par_;
                rowIndex = rowIndex + 8;
            }

            rowIndex = 2; columnIndex = 3;
            try
            {
                foreach (var y in BL_type)
                {
                    var _currentRange = _table.Cell(rowIndex, columnIndex).Range;
                    _currentRange.Text = y;
                    columnIndex = columnIndex + 1;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
            ////////////////////////////
            ///таблица 4
            ///////////////////////////
            string soxr = "", soxr_1 = ""; 
            int p = 0;
            tableNumber = 4;
            Word.Table _table6 = _document.Tables[tableNumber];
            columnIndex = 1; rowIndex = 2;
            foreach (var par_ in par)
            {
                var _currentRange = _table6.Cell(rowIndex, columnIndex).Range;
                _currentRange.Text = par_;
                for (int j = 3; j < 5; j++)
                {
                    var _currentRange_ = _table6.Cell(rowIndex + j, columnIndex).Range;
                    _currentRange_.Text = "";
                }
                var _currentRange1 = _table6.Cell(_table6.Rows.Count - 1, columnIndex).Range;
                if (_currentRange1.Text != "-\r\a")
                {
                    soxr = _currentRange1.Text;
                    soxr = soxr.Replace("\r\a", " ");
                }
                else
                {
                    _currentRange1.Text = soxr;
                    var _currentRange2 = _table6.Cell(_table6.Rows.Count, columnIndex).Range;
                    _currentRange2.Text = soxr;
                }
                for (int g = 5; g < 8; g++)
                {
                    var _currentRange3 = _table6.Cell(rowIndex + g, columnIndex).Range;
                    if (_currentRange3.Text != "-\r\a")
                    {
                        soxr_1 = _currentRange3.Text;
                        soxr_1 = soxr_1.Replace("\r\a", " ");
                    }
                    else
                    {
                        _currentRange3.Text = soxr_1;
                    }
                }
                columnIndex = columnIndex + 1;
            }
            columnIndex = 1;
            p = 0;
            foreach (var par_ in par)
            {
                rowIndex = 11;
                for (int j = 0; j < 8; j++)
                {
                    if (blocks[p, j] != "")
                    {
                        var _currentRange_2 = _table6.Cell(rowIndex, columnIndex).Range;
                        var _currentRange_3 = _table6.Cell(rowIndex + 9, columnIndex).Range;
                        var _currentRange_4 = _table6.Cell(rowIndex + 9 + 9, columnIndex).Range;
                        if (znach_SU50[p, j] == null || znach_SU50[p, j] == "")
                        { }
                        else
                            _currentRange_2.Text = znach_SU50[p, j];
                        if (znach_SCs50[p, j] == null || znach_SCs50[p, j] == "")
                        { }
                        else
                            _currentRange_3.Text = znach_SCs50[p, j];
                       
                        if (znach_SCfCm[p, j] == null || znach_SCfCm[p, j] == "")
                        { }
                        else
                            _currentRange_4.Text = znach_SCfCm[p, j];

                        rowIndex++;
                    }
                }
                p++;
                columnIndex = columnIndex + 1;
            }

        }

    }
}
