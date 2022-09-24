using System.Collections.Generic;
using System.Text;

namespace XlsxSpawnCSCode
{
    public class CellData
    {
        public bool IsArray;
        public string Data;
        public List<string> ArrayData=new List<string>(1);
        public int Start;
        public int End;

        public void Add(string s)
        {
            ArrayData.Add(s);
        }

        public void GetString(StringBuilder sb)
        {
            if (!IsArray)
            {
                if(!string.IsNullOrWhiteSpace(Data))
                    sb.Append(Data);
            }
            else
            {
                sb.Append("[");
                sb.Join(",", ArrayData);
                sb.Append("]");
            }
        }

        public static StringBuilder GetString(CellData cell, StringBuilder sb)
        {
            cell.GetString(sb);
            return sb;
        }

        public override string ToString()
        {
            return Data;
        }
    }
}