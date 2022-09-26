using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LitJson;

namespace XlsxSpawnCSCode
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var path = "..\\..\\Example\\";
            var filePath = path+"Example.xlsx";
            //读取表格数据
            var data= ReadXlsx.Read(filePath);
            Console.WriteLine(data);
            //生成json
            var json = JsonMapper.ToJson(data.GetDicData());
            File.WriteAllText(path+"config.json",json);
            //生成代码
            var codes= XlsxSpawnCode.SpawnDataManageCode(data);
            File.WriteAllText(path+"ConfigData.cs",codes.ToString());
            Console.WriteLine("ok!");

        }
    }
}