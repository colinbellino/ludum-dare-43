using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSacrifice : SacrificeBehaviour
{
	private Movement playerMovement;

	protected void OnEnable()
	{
		var player = GameObject.Find("Player");

		if (player)
		{
			playerMovement = player.GetComponent<Movement>();
			playerMovement.SetSpeedMode(0.5f);
		}
	}

	// protected void OnDisable()
	// {
	// 	playerMovement.ResetSpeed();
	// }
}
