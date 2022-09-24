using System.Collections.Generic;
using System.Text;
using ConfigData;
using LitJson;

namespace XlsxSpawnCSCode
{
    public static class XlsxSpawnCode
    {
        public static List<OneClass> SpawnFormXlsx(XlsxData xlsx)
        {
            List<OneClass> list = new List<OneClass>();
            foreach (var x in xlsx)
            {
                var one = new OneClass(x.Name);
                one.IdKeyName = x.IdKeyName;
                one.IdKeyType = ItemTool.GetItemType(x.IdKeyType);
                for (int i = 0; i < x.KeyType.Count; i++)
                {
                    var key = x.KeyName[i];
                    var des = x.Des[i];
                    var type = x.KeyType[i];
                    one.Items.Add(new Item()
                    {
                        Describe = des,
                        Name = key.Data,
                        Type = ItemTool.GetItemType(type.Data),
                    });
                }
                list.Add(one);
            }

            return list;
        }

        public static StringBuilder SpawnDataManageCode(XlsxData xlsx)
        {
            return SpawnDataManageCode(SpawnFormXlsx(xlsx));
        }

        public static StringBuilder SpawnDataManageCode(List<OneClass> oneClasses)
        {
            StringBuilder s = new StringBuilder();
            s.Append("\nusing System.Collections.Generic;");
            s.Append("\nusing System;");
            s.Append("\nusing LitJson;");
            s.Append("\nusing UnityEngine;");
            s.Append("\n");
            s.Append("\nnamespace ConfigData");

            s.Append("\n{");
            foreach (var one in oneClasses)
            {
                one.Spawn(s);
                s.Append("\n\n");
            }

            
            s.Append("\n\tclass ConfigJsonData");

            s.Append("\n\t{");
            foreach (var one in oneClasses)
            {
                s.Append("\n\t\tpublic ").Append(one.Name).Append("[] ").Append(one.Name).Append(";");
            }

            s.Append("\n\t}");
            
            s.Append("\n\tpublic class ConfigDataManage");
            s.Append("\n\t{");

            foreach (var one in oneClasses)
            {
                s.Append($"\n\t\tDictionary<{one.KeyType}, List<{one.Name}>> _{one.Name}Dictionary ")
                    .Append($"= new Dictionary<{one.KeyType}, List<{one.Name}>>();");
            }

            s.Append("\n");
            s.Append("\n\t\tpublic void InitJson(string json)");
            s.Append("\n\t\t{");
            s.Append("\n\t\t\tvar data = JsonMapper.ToObject<ConfigJsonData>(json);");
            
            foreach (var one in oneClasses)
            {
                s.Append($"\n\t\t\tif (data.{one.Name} != null)");
                s.Append( "\n\t\t\t{");
                s.Append($"\n\t\t\t\tforeach (var i in data.{one.Name})");
                s.Append( "\n\t\t\t\t{");
                s.Append( "\n\t\t\t\t\tif (i != null)");
                s.Append( "\n\t\t\t\t\t{");
                s.Append($"\n\t\t\t\t\t\tif(!_{one.Name}Dictionary.ContainsKey(i.{one.IdKeyName}))");
                s.Append($"\n\t\t\t\t\t\t\t_{one.Name}Dictionary.Add(i.{one.IdKeyName}, new List<{one.Name}>(){{i}});");
                s.Append(" \n\t\t\t\t\t\telse");
                s.Append($"\n\t\t\t\t\t\t\t_{one.Name}Dictionary[i.{one.IdKeyName}].Add(i);");
                s.Append( "\n\t\t\t\t\t}");
                s.Append( "\n\t\t\t\t}");
                s.Append( "\n\t\t\t}");
                s.Append( "\n\t\t\telse");
                s.Append( "\n\t\t\t{");
                s.Append($"\n\t\t\t\tDebug.Log(\"{one.Name} not found in json data\");");
                s.Append( "\n\t\t\t}");
                s.Append( "\n");
            }
            s.Append("\n\t\t}");
            
            foreach (var one in oneClasses)
            {
                var cName = one.Name;
                s.Append("\n");
                s.Append($"\n\t\tpublic bool HasKey{cName}({one.KeyType} {one.IdKeyName.ToLower()}) =>")
                    .Append($" _{cName}Dictionary.ContainsKey({one.IdKeyName.ToLower()});");
                s.Append("\n");
                s.Append($"\n\t\tpublic Dictionary<{one.KeyType}, List<{cName}>>.KeyCollection ")
                    .Append($"Get{cName}KeyCollection() => _{cName}Dictionary.Keys;");
                s.Append("\n");
                s.Append($"\n\t\tpublic List<{cName}> Get{cName}Data({one.KeyType} {one.IdKeyName.ToLower()})");
                s.Append( "\n\t\t{");
                s.Append($"\n\t\t\tif (_{cName}Dictionary.TryGetValue({one.IdKeyName.ToLower()}, out var list))");
                s.Append(" \n\t\t\t\treturn list;");
                s.Append($"\n\t\t\tthrow new Exception($\"Config Data Error : No {{{one.IdKeyName.ToLower()}}} in {cName}Config\");");
                s.Append( "\n\t\t}");
                s.Append("\n");
            }
            
            s.Append("\n\t}");
            
            s.Append("\n}");
            
            
            return s;
        }
    }
}