namespace XlsxSpawnCSCode
{
    public enum ItemType
    {
        Int32,
        Int64,
        String,
        Float,
        Short,
        Int32Array,
        Int64Array,
        StringArray,
        FloatArray,
        ShortArray
    }

    public static class ItemTool
    {
        public static ItemType GetItemType(string typeName)
        {
            typeName = typeName.ToLower();
            switch (typeName)
            {
                case "int":
                case "int32":
                    return ItemType.Int32;
                    break;
                case "long":
                case "int64":
                    return ItemType.Int64;
                    break;
                case "float":
                    return ItemType.Float;
                    break;
                case "short":
                    return ItemType.Short;
                    break;
                case "string":
                    return ItemType.String;
                    break;
                case "int[]":
                case "int32[]":
                    return ItemType.Int32Array;
                    break;
                case "long[]":
                case "int64[]":
                    return ItemType.Int64Array;
                    break;
                case "float[]":
                    return ItemType.FloatArray;
                    break;
                case "short[]":
                    return ItemType.ShortArray;
                    break;
                case "string[]":
                    return ItemType.StringArray;
                    break;
                default:
                    return ItemType.String;
                    break;
            }
        }
    }
}