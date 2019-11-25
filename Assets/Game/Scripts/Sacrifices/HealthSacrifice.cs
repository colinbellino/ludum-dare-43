using UnityEngine;

public class HealthSacrifice : MonoBehaviour, ISacrifice
{
	public void OnApply()
	{
		var statProvider = GameObject.Find("Player")?.GetComponent<IStatsProvider>();

		if (statProvider == null) return;

		var health = statProvider.GetStat(StatTypes.Health);
		var maxHeath = statProvider.GetStat(StatTypes.MaxHealth);

		statProvider.SetStat(StatTypes.Health, Mathf.Min(health, maxHeath - 2));
		statProvider.SetStat(StatTypes.MaxHealth, maxHeath);
	}

	public void OnRemove() { }
}
