using UnityEngine;

public class DexteritySacrifice : MonoBehaviour, ISacrifice
{
	public void OnApply()
	{
		// TODO: Inject the player instead ?
		var stats = GameObject.Find("Player")?.GetComponent<IEntity>()?.Stats;
		if (stats != null)
		{
			UnityEngine.Debug.Log("stats[StatTypes.FireRate] " + stats[StatTypes.FireRate]);
			stats[StatTypes.FireRate] *= 2;
			UnityEngine.Debug.Log("stats[StatTypes.FireRate] " + stats[StatTypes.FireRate]);
		}
	}

	public void OnRemove()
	{
		throw new System.NotImplementedException();
	}
}
