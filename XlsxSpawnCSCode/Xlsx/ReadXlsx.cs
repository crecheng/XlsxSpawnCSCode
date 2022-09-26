using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

namespace XlsxSpawnCSCode
{
    public static class ReadXlsx
    {

        /// <summary>
        /// 读取一个表格文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static XlsxData Read(string fileName)
        {
            if (!File.Exists(fileName))
                return null;
            //复制一份文件进行读取
            var fileTmp = GetRandomTempFile(fileName);
            while (File.Exists(fileTmp))
            {
                fileTmp = GetRandomTempFile(fileName);
            }
            File.Copy(fileName,fileTmp);
            FileStream fs = new FileStream(fileTmp, FileMode.Open);
            ExcelPackage package = new ExcelPackage(fs);
            XlsxData data = null;
            try
            {
                data = Read(package);
            }
            finally
            {
                package.Dispose();
                fs.Close();
                File.Delete(fileTmp);
            }

            return data;
        }
        
        /// <summary>
        /// 开始读取当个文件
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public static XlsxData Read(ExcelPackage package)
        {
            if (package == null || package.Workbook==null)
                return null;

            XlsxData data = new XlsxData();
            foreach (var sheet in package.Workbook.Worksheets)
            {
                //读取每个表
                data.Datas.Add(ReadSheet(sheet));
            }

            return data;
        }
        
        /// <summary>
        /// 读取单个表格sheet
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public static SheetData ReadSheet(ExcelWorksheet sheet)
        {
            if (sheet == null)
                return null;
            SheetData data = new SheetData();
            List<int> num = new List<int>();
            data.Name = sheet.GetValue<string>(1,1);
            var rowEnd = sheet.Dimension.End.Row;
            var cellEnd = sheet.Dimension.End.Column;
            var keyCell = new CellData();
            var keyType = new CellData();
            //读取表的格式定义
            for (int j = 1; j <= cellEnd; j++)
            {
                var des=sheet.GetValue<string>(2, j);
                var key=sheet.GetValue<string>(3, j);
                var type=sheet.GetValue<string>(4, j);
                if (j == 1)
                {
                    data.IdKeyName = key;
                    data.IdKeyType = type.ToLower();
                }
                if (!string.IsNullOrWhiteSpace(type))
                {
                    if (type.EndsWith("["))
                    {
                        keyCell.IsArray = true;
                        keyCell.Data = key;
                        keyCell.Start = j;
                        keyType.Data = (type+"]").ToLower();
                        data.Des.Add(des);
                    }else 
                    {
                        if (!type.Equals("]"))
                        {
                            keyCell.Data = key;
                            keyType.Data = type.ToLower();
                            keyCell.Start = j;
                            data.Des.Add(des);
                           
                        }
                        keyCell.End = j;
                        data.KeyName.Add(keyCell);
                        data.KeyType.Add(keyType);
                        keyCell = new CellData();
                        keyType = new CellData();
                    }
                }
            }

            for (int i = 5; i <= rowEnd; i++)
            {
                RowData row = new RowData();
                int cellIndex = 0;
                //读取每个格子
                CellData cell = new CellData();
                for (int j = 1; j <= cellEnd; j++)
                {
                    var key = data.KeyName[cellIndex];
                    var s = sheet.GetValue<string>(i, j);
                    if (s == null)
                        s = string.Empty;
                    
                    if (j == 1)
                    {
                        //首格为空，这行不读
                        if (!string.IsNullOrWhiteSpace(s))
                        {
                            if (!data.Date.ContainsKey(s))
                                data.Date.Add(s,new List<RowData>());
                            data.Date[s].Add(row);
                            cell.Data = s;
                            row.ID = s;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (key.IsArray)
                        {
                            cell.IsArray = true;
                            cell.Add(s);
                        }
                        else
                        {
                            cell.Data = s;
                        }
                    }
                    if (key.End == j)
                    {
                        row.Add(cell);
                        cell = new CellData();
                        cellIndex++;
                    }
                    
                }
                
                if(cell.IsArray)
                    row.Add(cell);
            }

            return data;
        }


        public static string GetRandomTempFile(string file)
        {
            return  $"{Path.GetDirectoryName(file)}\\" +
                    $"~{Path.GetFileNameWithoutExtension(file)}-{RandomHelper.Next(0, 1000)}.xlsx";
        }
    }
}