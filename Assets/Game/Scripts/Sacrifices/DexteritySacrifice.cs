using UnityEngine;

public class DexteritySacrifice : MonoBehaviour, ISacrifice
{
	public void OnApply()
	{
		// TODO: Inject the player instead ?
		var statsProvider = GameObject.Find("Player")?.GetComponent<PlayerFacade>().StatsProvider;

		if (statsProvider == null) return;

		var fireRate = statsProvider.GetStat(StatTypes.FireRate);
		statsProvider.SetStat(StatTypes.FireRate, fireRate / 2);
	}

	public void OnRemove()
	{
		throw new System.NotImplementedException();
	}
}
