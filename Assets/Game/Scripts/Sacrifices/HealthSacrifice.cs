using UnityEngine;

public class HealthSacrifice : MonoBehaviour, ISacrifice
{
	private readonly int _healthToSacrifice = 1;

	public void OnApply()
	{
		var player = GameObject.Find("Player");
		if (player)
		{
			player.PostNotification(Health.OnHitNotification, _healthToSacrifice);
		}
	}

	public void OnRemove() { }
}
