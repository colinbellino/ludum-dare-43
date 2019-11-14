using UnityEngine;

[CreateAssetMenu(fileName = "Sacrifice", menuName = "LD43/Sacrifice", order = 1)]
public class Sacrifice : ScriptableObject
{
	public string id;
	public string label;
	public Sprite image;
	public GameObject prefab;
}
