using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSacrifice : SacrificeBehaviour
{
	private Health playerHealth;

	protected void OnEnable()
	{
		var player = GameObject.Find("Player");

		if (player)
		{
			playerHealth = player.GetComponent<Health>();
			playerHealth.SetMaxHealth(2);
		}
	}

	// protected void OnDisable()
	// {
	// 	playerHealth.ResetToMaxDefaultHealth();
	// }
}
