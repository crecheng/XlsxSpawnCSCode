using System.Collections.Generic;
using System.Text;

namespace XlsxSpawnCSCode
{
    public class SheetData
    {
        public string Name;
        public string IdKeyName;
        public string IdKeyType;
        public List<CellData> KeyName=new List<CellData>();
        public List<CellData> KeyType=new List<CellData>();
        public List<string> Des=new List<string>();
        public Dictionary<string, List<RowData>> Date=new Dictionary<string, List<RowData>>();
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            sb.Join(",\t", Des);

            sb.Append("\n");
            sb.Join(",\t", KeyName);

            sb.Append("\n");
            sb.Join(",\t", KeyType);
            sb.Append("\n");
            foreach (var rowList in Date)
            {
                foreach (var rowData in rowList.Value)
                {
                    rowData.GetString(sb);
                    sb.Append("\n");
                }
            }

            return sb.ToString();
        }

        public List<object> GetListData()
        {
            List<object> res = new List<object>();
            foreach (var d in Date)
            {
                foreach (var rowData in d.Value)
                {
                    res.Add(rowData.GetDicData(this));
                }
            }
            return res;
        }
    }
}