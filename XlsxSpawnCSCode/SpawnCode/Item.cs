using System;
using System.Text;

namespace XlsxSpawnCSCode
{
    public class Item : ISpawn
    {

        public ItemType Type;
        public string Describe;
        public string Name;
        public void Spawn(StringBuilder s)
        {
            if(!string.IsNullOrWhiteSpace(Describe))
                s.Append("\n\t///").Append(Describe);
            s.Append("\n\tpublic ");
            switch (Type)
            {
                 case ItemType.Int32: s.Append("int"); break;
                 case ItemType.Int64: s.Append("long"); break;
                 case ItemType.Float: s.Append("float"); break;
                 case ItemType.Short: s.Append("short"); break;
                 case ItemType.String: s.Append("string"); break;
                 case ItemType.Int32Array: s.Append("int[]"); break;
                 case ItemType.Int64Array: s.Append("long[]"); break;
                 case ItemType.FloatArray: s.Append("float[]"); break;
                 case ItemType.ShortArray: s.Append("short[]"); break;
                 case ItemType.StringArray: s.Append("string[]"); break;
            }

            s.Append(" ").Append(Name).Append(";");
        }

        public void SetType(string typeName)
        {
            Type = ItemTool.GetItemType(typeName);
        }

        public static string TypeGetString(ItemType type)
        {
            switch (type)
            {
                case ItemType.Int32: return "int";
                case ItemType.Int64: return "long";
                case ItemType.Float: return "float";
                case ItemType.Short: return "short";
                case ItemType.String:return "string";
                case ItemType.Int32Array: return "int[]";
                case ItemType.Int64Array: return "long[]";
                case ItemType.FloatArray: return "float[]";
                case ItemType.ShortArray: return "short[]";
                case ItemType.StringArray:return "string[]";
            }

            return "string";
        }

    }
    
}