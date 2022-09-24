using System.Collections.Generic;
using System.Text;

namespace XlsxSpawnCSCode
{
    public class RowData
    {
        public string ID;
        public List<CellData> Datas=new List<CellData>();

        /// <summary>
        /// 将格子数据添加到行数据
        /// </summary>
        /// <param name="cell"></param>
        public void Add(CellData cell)
        {
            Datas.Add(cell);
        }

        public void GetString(StringBuilder sb)
        {
            sb.Join(",\t", Datas,CellData.GetString);
        }
        
        public static StringBuilder GetString(RowData row, StringBuilder sb)
        {
             row.GetString(sb);
             return sb;
        }

        public Dictionary<string, object> GetDicData(SheetData sheetData)
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            for (var i = 0; i < Datas.Count; i++)
            {
                var cell = Datas[i];
                var type = sheetData.KeyType[i];
                var name = sheetData.KeyName[i];
                if (cell.IsArray)
                    res.Add(name.Data,Tool.XlsxCellTypeGetData(cell.ArrayData,type.Data));
                else
                    res.Add(name.Data,Tool.XlsxCellTypeGetData(cell.Data,type.Data));
                
            }

            return res;
        }

    }
}