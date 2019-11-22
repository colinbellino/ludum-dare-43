using UnityEngine;

public class HealthSacrifice : MonoBehaviour, ISacrifice
{
	private EntityStats _stats;

	public void OnApply()
	{
		_stats = GameObject.Find("Player")?.GetComponent<PlayerFacade>()?.Stats;
		if (_stats != null)
		{
			_stats.Health.Current -= 2;
			_stats.MaxHealth.Current -= 2;
		}
	}

	public void OnRemove() { }
}
