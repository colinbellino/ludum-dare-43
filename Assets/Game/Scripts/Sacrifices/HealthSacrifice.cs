using UnityEngine;

public class HealthSacrifice : MonoBehaviour, ISacrifice
{
	private PlayerFacade _player;

	public void OnApply()
	{
		_player = GameObject.Find("Player")?.GetComponent<PlayerFacade>();
		if (_player)
		{
			_player.Stats.Health.Current -= 2;
			_player.Stats.MaxHealth.Current -= 2;
		}
	}

	public void OnRemove() { }
}
