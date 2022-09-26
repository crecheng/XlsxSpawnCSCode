using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ConfigData;
using CustomLitJson;


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
            JsonMapper.DirectOutputChineseCharacters = true;
            var json = JsonMapper.Instance.ToJson(data.GetDicData());
            File.WriteAllText(path+"config.json",json);
            var obj= JsonMapper.Instance.ToObject<ConfigJsonData>(json);
            //生成代码
            var codes= XlsxSpawnCode.SpawnDataManageCode(data);
            File.WriteAllText(path+"ConfigData.cs",codes.ToString());
            Console.WriteLine("ok!");

        }
    }
}