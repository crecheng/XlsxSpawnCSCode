using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ConfigData;
using LitJson;

namespace XlsxSpawnCSCode
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            OneClass oneClass = new OneClass("GameData1");
            oneClass.Add(new Item()
            {
                Type = ItemType.Int32,
                Name = "O1",
                Describe = "hhhhh"
            });
            oneClass.Add(new Item()
            {
                Type = ItemType.Int32Array,
                Name = "O2",
                Describe = "hhhhh"
            });
            oneClass.Add(new Item()
            {
                Type = ItemType.String,
                Name = "O3",
                Describe = "hhhhh"
            });
            StringBuilder a = new StringBuilder();
            oneClass.Spawn(a);
            Console.WriteLine(a);


            var file = @"C:\Users\Crecheng\Desktop\2\Entity.xlsx";
            var data= ReadXlsx.Read(file);
            Console.WriteLine(data);
            /*var code= XlsxSpawnCode.SpawnDataManageCode(data);
            File.WriteAllText(@"D:\code\codeNet\projects\XlsxSpawnCSCode\XlsxSpawnCSCode\spawn.cs",code.ToString());*/
            var json = JsonMapper.ToJson(data.GetDicData());
            var c = GetConfig(json);
            Console.WriteLine("ok");

        }
        
        
        public static ConfigDataManage GetConfig(string json)
        {
            var c = new ConfigDataManage();
            c.InitJson(json);
            return c;
        }
        
    }
}