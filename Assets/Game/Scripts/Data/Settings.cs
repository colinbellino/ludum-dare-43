using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "LD43/Settings", order = 1)]
public class Settings : ScriptableObject
{
	public List<GameObject> levels = new List<GameObject>();
	public List<Sacrifice> sacrifices = new List<Sacrifice>();
	public List<GameObject> enemyPrefabs = new List<GameObject>();
}

[System.Serializable]
public class SacrificeList
{
	public List<string> sacrifices;
}
