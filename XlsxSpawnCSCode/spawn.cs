
using System.Collections.Generic;
using System;
using LitJson;
using UnityEngine;

namespace ConfigData
{
public partial class Monster
{
	public int Id;
	///血量
	public float HP;
	///防御
	public float DEF;
	///攻击
	public float ATK;
	///buff
	public int[] Buff;
}



public partial class Player
{
	public string PId;
	public string Type;
	public long Level;
	public int Atk;
	public string[] Fun;
	public float View;
}



	class ConfigJsonData
	{
		public Monster[] Monster;
		public Player[] Player;
	}
	public class ConfigDataManage
	{
		Dictionary<int, List<Monster>> _MonsterDictionary = new Dictionary<int, List<Monster>>();
		Dictionary<string, List<Player>> _PlayerDictionary = new Dictionary<string, List<Player>>();

		public void InitJson(string json)
		{
			var data = JsonMapper.ToObject<ConfigJsonData>(json);
			if (data.Monster != null)
			{
				foreach (var i in data.Monster)
				{
					if (i != null)
					{
						if(!_MonsterDictionary.ContainsKey(i.Id))
							_MonsterDictionary.Add(i.Id, new List<Monster>(){i}); 
						else
							_MonsterDictionary[i.Id].Add(i);
					}
				}
			}
			else
			{
				Debug.Log("Monster not found in json data");
			}

			if (data.Player != null)
			{
				foreach (var i in data.Player)
				{
					if (i != null)
					{
						if(!_PlayerDictionary.ContainsKey(i.PId))
							_PlayerDictionary.Add(i.PId, new List<Player>(){i}); 
						else
							_PlayerDictionary[i.PId].Add(i);
					}
				}
			}
			else
			{
				Debug.Log("Player not found in json data");
			}

		}

		public bool HasKeyMonster(int id) => _MonsterDictionary.ContainsKey(id);

		public Dictionary<int, List<Monster>>.KeyCollection GetMonsterKeyCollection() => _MonsterDictionary.Keys;

		public List<Monster> GetMonsterData(int id)
		{
			if (_MonsterDictionary.TryGetValue(id, out var list)) 
				return list;
			throw new Exception($"Config Data Error : No {id} in MonsterConfig");
		}


		public bool HasKeyPlayer(string pid) => _PlayerDictionary.ContainsKey(pid);

		public Dictionary<string, List<Player>>.KeyCollection GetPlayerKeyCollection() => _PlayerDictionary.Keys;

		public List<Player> GetPlayerData(string pid)
		{
			if (_PlayerDictionary.TryGetValue(pid, out var list)) 
				return list;
			throw new Exception($"Config Data Error : No {pid} in PlayerConfig");
		}

	}
}