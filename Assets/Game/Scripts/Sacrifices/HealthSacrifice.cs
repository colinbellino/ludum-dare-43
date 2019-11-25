using UnityEngine;

public class HealthSacrifice : MonoBehaviour, ISacrifice
{
	public void OnApply()
	{
		var statProvider = GameObject.Find("Player")?.GetComponent<PlayerFacade>().StatsProvider;

		if (statProvider == null) return;

		var health = statProvider.GetStat(StatTypes.Health);
		var maxHeath = statProvider.GetStat(StatTypes.MaxHealth);

		statProvider.SetStat(StatTypes.Health, Mathf.Min(health, maxHeath - 2));
		statProvider.SetStat(StatTypes.MaxHealth, maxHeath - 2);
	}

	public void OnRemove() { }
}
