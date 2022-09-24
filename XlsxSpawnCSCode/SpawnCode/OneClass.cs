using System.Collections.Generic;
using System.Text;

namespace XlsxSpawnCSCode
{
    /// <summary>
    /// 类信息
    /// </summary>
    public class OneClass : ISpawn
    {
        public List<ISpawn> Items;
        public string IdKeyName;
        public ItemType IdKeyType;
        public string KeyType =>ItemHelper.TypeHelp(IdKeyType).TypeName;
        public string Name;

        public OneClass(string name)
        {
            Name = name;
            Items = new List<ISpawn>();
        }

        public void Add(ISpawn item)
        {
            Items.Add(item);
        }
        public void Spawn(StringBuilder s)
        {
            s.Append("\npublic partial class ").Append(Name);
            s.Append("\n{");
            if(Items!=null)
                Items.ForEach((i)=>i.Spawn(s));
            s.Append("\n}\n");
        }
    }
}