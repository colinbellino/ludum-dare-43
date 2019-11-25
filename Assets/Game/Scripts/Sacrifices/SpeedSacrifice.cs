using UnityEngine;
using Zenject;

public class SpeedSacrifice : MonoBehaviour, ISacrifice
{
	public void OnApply()
	{
		var statsProvider = GameObject.Find("Player")?.GetComponent<PlayerFacade>().StatsProvider;

		if (statsProvider == null) return;

		var moveSpeed = statsProvider.GetStat(StatTypes.MoveSpeed);
		statsProvider.SetStat(StatTypes.MoveSpeed, moveSpeed / 2);
	}

	public void OnRemove()
	{
		throw new System.NotImplementedException();
	}
}
