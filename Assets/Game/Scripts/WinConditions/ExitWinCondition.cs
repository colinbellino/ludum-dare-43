using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitWinCondition : WinCondition
{
	private bool winStatus = false;

	private void OnEnable()
	{
		this.AddObserver(UpdateExitStatus, Exit.OnTriggered);
	}

	private void OnDisable()
	{
		this.RemoveObserver(UpdateExitStatus, Exit.OnTriggered);
	}

	private void UpdateExitStatus(object sender, object args)
	{
		winStatus = (bool) args;
	}

	protected override bool CheckForWinCondition()
	{
		return winStatus;
	}
}
