using System.Collections;
using System.Collections.Generic;

namespace XlsxSpawnCSCode
{
    public class XlsxData : ICollection<SheetData>
    {
        public List<SheetData> Datas=new List<SheetData>();

        public Dictionary<string, object> GetDicData()
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            foreach (var sheetData in Datas)
            {
                res.Add(sheetData.Name,sheetData.GetListData());
            }

            return res;
        }

        public IEnumerator<SheetData> GetEnumerator()
        {
            return Datas.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(SheetData item)
        {
            Datas.Add(item);
        }

        public void Clear()
        {
            Datas.Clear();
        }

        public bool Contains(SheetData item)
        {
            return Datas.Contains(item);
        }

        public void CopyTo(SheetData[] array, int arrayIndex)
        {
            Datas.CopyTo(array,arrayIndex);
        }

        public bool Remove(SheetData item)
        {
            return Datas.Remove(item);
        }

        public int Count => Datas.Count;

        public bool IsReadOnly => false;

        public override string ToString()
        {
            string s = string.Empty;
            foreach (var sheetData in Datas)
            {
                s += sheetData.ToString();
                s += "\n\n";
            }

            return s;
        }
    }
}