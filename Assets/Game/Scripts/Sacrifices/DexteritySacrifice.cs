using UnityEngine;

public class DexteritySacrifice : MonoBehaviour, ISacrifice
{
	public void OnApply()
	{
		// TODO: Inject the player instead ?
		var stats = GameObject.Find("Player")?.GetComponent<IEntity>()?.Stats;
		if (stats != null)
		{
			stats[StatTypes.FireRate] /= 2;
		}
	}

	public void OnRemove()
	{
		throw new System.NotImplementedException();
	}
}
