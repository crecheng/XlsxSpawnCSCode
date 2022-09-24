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
            s.Append(ItemHelper.TypeHelp(Type).TypeName);
            s.Append(" ").Append(Name).Append(";");
        }
        
    }
    
}