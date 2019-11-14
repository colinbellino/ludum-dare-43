using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAllEnemiesExitCondition : ExitCondition
{
	private bool isConditionFullfilled;

	private void OnEnable()
	{
		this.AddObserver(OnDeath, Health.OnDeathNotification);
	}

	private void OnDisable()
	{
		this.RemoveObserver(OnDeath, Health.OnDeathNotification);
	}

	private void OnDeath(object sender, object args)
	{
		var enemies = GameObject.FindGameObjectsWithTag("Enemy");
		// This is set to 1 because we do the check before the last enemy has been deleted.
		isConditionFullfilled = enemies.Length <= 1;
	}

	protected override bool CheckForExitCondition()
	{
		return isConditionFullfilled;
	}
}
