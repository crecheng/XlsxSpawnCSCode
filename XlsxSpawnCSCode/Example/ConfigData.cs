
using System.Collections.Generic;
using System;
using LitJson;
using UnityEngine;

namespace ConfigData
{
public partial class ItemData
{
	///id
	public int Id;
	///数量
	public int Count;
	///品质
	public short Quality;
	///描述
	public string Des;
}



public partial class Monster
{
	///id
	public int ID;
	///等级
	public int Level;
	///血量
	public int HP;
	///攻击
	public int ATK;
	///攻速
	public float ATKSpeed;
	///词条
	public string[] Buffs;
}



	class ConfigJsonData
	{
		public ItemData[] ItemData;
		public Monster[] Monster;
	}
	public class ConfigDataManage
	{
		Dictionary<int, List<ItemData>> _ItemDataDictionary = new Dictionary<int, List<ItemData>>();
		Dictionary<int, List<Monster>> _MonsterDictionary = new Dictionary<int, List<Monster>>();

		public void InitJson(string json)
		{
			var data = JsonMapper.ToObject<ConfigJsonData>(json);
			if (data.ItemData != null)
			{
				foreach (var i in data.ItemData)
				{
					if (i != null)
					{
						if(!_ItemDataDictionary.ContainsKey(i.Id))
							_ItemDataDictionary.Add(i.Id, new List<ItemData>(){i}); 
						else
							_ItemDataDictionary[i.Id].Add(i);
					}
				}
			}
			else
			{
				Debug.Log("ItemData not found in json data");
			}

			if (data.Monster != null)
			{
				foreach (var i in data.Monster)
				{
					if (i != null)
					{
						if(!_MonsterDictionary.ContainsKey(i.ID))
							_MonsterDictionary.Add(i.ID, new List<Monster>(){i}); 
						else
							_MonsterDictionary[i.ID].Add(i);
					}
				}
			}
			else
			{
				Debug.Log("Monster not found in json data");
			}

		}

		public bool HasKeyItemData(int id) => _ItemDataDictionary.ContainsKey(id);

		public Dictionary<int, List<ItemData>>.KeyCollection GetItemDataKeyCollection() => _ItemDataDictionary.Keys;

		public List<ItemData> GetItemDataData(int id)
		{
			if (_ItemDataDictionary.TryGetValue(id, out var list)) 
				return list;
			throw new Exception($"Config Data Error : No {id} in ItemDataConfig");
		}

		public ItemData GetItemDataDataFirst(int id) => GetItemDataData(id)[0];


		public bool HasKeyMonster(int id) => _MonsterDictionary.ContainsKey(id);

		public Dictionary<int, List<Monster>>.KeyCollection GetMonsterKeyCollection() => _MonsterDictionary.Keys;

		public List<Monster> GetMonsterData(int id)
		{
			if (_MonsterDictionary.TryGetValue(id, out var list)) 
				return list;
			throw new Exception($"Config Data Error : No {id} in MonsterConfig");
		}

		public Monster GetMonsterDataFirst(int id) => GetMonsterData(id)[0];

	}
}