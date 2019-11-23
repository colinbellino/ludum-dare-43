using UnityEngine;

public class HealthSacrifice : MonoBehaviour, ISacrifice
{
	public void OnApply()
	{
		var stats = GameObject.Find("Player")?.GetComponent<PlayerFacade>()?.Stats;
		if (stats != null)
		{
			stats[StatTypes.Health] -= 2;
			stats[StatTypes.MaxHealth] -= 2;
		}
	}

	public void OnRemove() { }
}
