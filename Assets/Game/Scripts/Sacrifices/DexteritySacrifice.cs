using UnityEngine;

public class DexteritySacrifice : MonoBehaviour, ISacrifice
{
	private Shooter _playerShooter;

	public void OnApply()
	{
		var player = GameObject.Find("Player");

		if (player)
		{
			_playerShooter = player.GetComponent<Shooter>();
			_playerShooter.SetCooldownMode(true);
		}
	}

	public void OnRemove()
	{
		throw new System.NotImplementedException();
	}
}
