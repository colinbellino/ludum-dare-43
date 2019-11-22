using UnityEngine;

public class HealthSacrifice : MonoBehaviour, ISacrifice
{
	private Health _playerHealth;

	public void OnApply()
	{
		var player = GameObject.Find("Player");
		if (player)
		{
			_playerHealth = player.GetComponent<Health>();
			_playerHealth.SetMaxHealth(2);
		}
	}

	public void OnRemove()
	{
		_playerHealth.ResetToMaxDefaultHealth();
	}
}
