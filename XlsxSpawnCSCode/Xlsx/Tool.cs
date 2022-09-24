using System;
using System.Collections.Generic;
using System.Text;

namespace XlsxSpawnCSCode
{
    public static class Tool
    {
        private static Dictionary<string, object> _defaultData = new Dictionary<string, object>()
        {
            {"int",0},
            {"int32",0},
            {"long",0},
            {"int64",0},
            {"float",0},
            {"short",0},
            {"string",string.Empty},
            {"int[]",Array.Empty<int>()},
            {"int32[]",Array.Empty<int>()},
            {"long[]",Array.Empty<long>()},
            {"int64[]",Array.Empty<long>()},
            {"float[]",Array.Empty<float>()},
            {"short[]",Array.Empty<short>()},
            {"string[]",Array.Empty<string>()},
        };
        public static StringBuilder Join(this StringBuilder sb, string seq, List<string> list)
        {
            if (list == null || list.Count <= 0)
                return sb;
            for (int i = 0; i < list.Count; i++)
            {
                if (i != 0)
                    sb.Append(seq);
                var s = list[i];
                if (!string.IsNullOrWhiteSpace(s))
                    sb.Append(s);
            }

            return sb;
        }
        
        public static StringBuilder Join<T>(this StringBuilder sb, string seq,
            List<T> list, Func<T,StringBuilder,StringBuilder> fun)
        {
            if (list == null || list.Count <= 0)
                return sb;
            for (int i = 0; i < list.Count; i++)
            {
                if (i != 0)
                    sb.Append(seq);
                var s = list[i];
                if (s != null)
                    fun.Invoke(s,sb);
                

            }

            return sb;
        }
        
        public static StringBuilder Join<T>(this StringBuilder sb, string seq, List<T> list)
        {
            if (list == null || list.Count <= 0)
                return sb;
            for (int i = 0; i < list.Count; i++)
            {
                if (i != 0)
                    sb.Append(seq);
                var s = list[i];
                var ss = string.Empty;
                if (s != null)
                {
                    ss = s.ToString();
                }

                if (!string.IsNullOrWhiteSpace(ss))
                    sb.Append(ss);


            }

            return sb;
        }
        
        
        public static object XlsxCellTypeGetData(string data, string type)
        {
            if (string.IsNullOrEmpty(data))
                return _defaultData[type];
            
            switch (ItemTool.GetItemType(type))
            {
                case ItemType.Float:
                    if (float.TryParse(data, out float f))
                        return f;
                    break;
                case ItemType.Int32:
                    if (int.TryParse(data, out int i))
                        return i;
                    break;
                case ItemType.Int64:
                    if (int.TryParse(data, out int l))
                        return l;
                    break;
                case ItemType.String:
                    return data;
                    break;
                case ItemType.Short:
                    if (short.TryParse(data, out short s))
                        return s;
                    break;
            }

            return _defaultData[type];
        }
        
        public static object XlsxCellTypeGetData(List<string> data, string type)
        {
            if (data.Count<=0)
                return _defaultData[type];
            List<object> res = new List<object>();
            switch (ItemTool.GetItemType(type))
            {
                case ItemType.FloatArray:
                    foreach (var s1 in data)
                    {
                        if (float.TryParse(s1, out float f))
                            res.Add(f);
                        else
                            res.Add(0);
                    }

                    return res;
                    break;
                case ItemType.Int32Array:
                    foreach (var s1 in data)
                    {
                        if (int.TryParse(s1, out int f))
                            res.Add(f);
                        else
                            res.Add(0);
                    }

                    return res;
                    break;
                case ItemType.Int64Array:
                    foreach (var s1 in data)
                    {
                        if (long.TryParse(s1, out long f))
                            res.Add(f);
                        else
                            res.Add(0);
                    }

                    return res;
                    break;

                case ItemType.ShortArray:
                    foreach (var s1 in data)
                    {
                        if (short.TryParse(s1, out short f))
                            res.Add(f);
                        else
                            res.Add(0);
                    }

                    return res;
                    break;
                case ItemType.StringArray:
                    return data;
                    break;
            }

            return data;
        }
        
    }
}