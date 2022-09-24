using System;
using System.Collections.Generic;

namespace XlsxSpawnCSCode
{
    public class ItemHelper
    {
        public ItemType Type => _type;
        public string TypeName => _typeName;
        private ItemType _type;
        private string _typeName;

        public static Dictionary<ItemType, ItemHelper> TypeToObj = new Dictionary<ItemType, ItemHelper>()
        {
            { ItemType.Float, new ItemHelper() { _type = ItemType.Float, _typeName = "float" } },
            { ItemType.Int32, new ItemHelper() { _type = ItemType.Int32, _typeName = "int" } },
            { ItemType.Int64, new ItemHelper() { _type = ItemType.Int64, _typeName = "long" } },
            { ItemType.String, new ItemHelper() { _type = ItemType.String, _typeName = "string" } },
            { ItemType.Short, new ItemHelper() { _type = ItemType.Short, _typeName = "short" } },
            { ItemType.FloatArray, new ItemHelper() { _type = ItemType.FloatArray, _typeName = "float[]" } },
            { ItemType.Int32Array, new ItemHelper() { _type = ItemType.Int32Array, _typeName = "int[]" } },
            { ItemType.Int64Array, new ItemHelper() { _type = ItemType.Int64Array, _typeName = "long[]" } },
            { ItemType.StringArray, new ItemHelper() { _type = ItemType.StringArray, _typeName = "string[]" } },
            { ItemType.ShortArray, new ItemHelper() { _type = ItemType.ShortArray, _typeName = "short[]" } },
        };

        public static Dictionary<string, ItemHelper> StringToObj = new Dictionary<string, ItemHelper>()
        {
            { "float", new ItemHelper() { _type = ItemType.Float, _typeName = "float" } },
            { "int", new ItemHelper() { _type = ItemType.Int32, _typeName = "int" } },
            { "int32", new ItemHelper() { _type = ItemType.Int32, _typeName = "int" } },
            { "long", new ItemHelper() { _type = ItemType.Int64, _typeName = "long" } },
            { "int64", new ItemHelper() { _type = ItemType.Int64, _typeName = "long" } },
            { "string", new ItemHelper() { _type = ItemType.String, _typeName = "string" } },
            { "short", new ItemHelper() { _type = ItemType.Short, _typeName = "short" } },
            { "float[]", new ItemHelper() { _type = ItemType.FloatArray, _typeName = "float[]" } },
            { "int[]", new ItemHelper() { _type = ItemType.Int32Array, _typeName = "int[]" } },
            { "int32[]", new ItemHelper() { _type = ItemType.Int32Array, _typeName = "int[]" } },
            { "long[]", new ItemHelper() { _type = ItemType.Int64Array, _typeName = "long[]" } },
            { "int64[]", new ItemHelper() { _type = ItemType.Int64Array, _typeName = "long[]" } },
            { "string[]", new ItemHelper() { _type = ItemType.StringArray, _typeName = "string[]" } },
            { "short[]", new ItemHelper() { _type = ItemType.ShortArray, _typeName = "short[]" } },
        };

        public static ItemHelper TypeHelp(ItemType type)
        {
            if (TypeToObj.ContainsKey(type))
                return TypeToObj[type];
            Console.WriteLine("类型未支持！");
            return TypeToObj[ItemType.String];
        }
        
        public static ItemHelper StringHelp(string s)
        {
            if (StringToObj.ContainsKey(s))
                return StringToObj[s];
            Console.WriteLine("类型未支持！");
            return StringToObj["string"];
        }
        

    }
}