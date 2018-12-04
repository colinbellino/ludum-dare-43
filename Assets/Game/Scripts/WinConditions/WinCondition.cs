using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class WinCondition : MonoBehaviour
{
	public const string OnWinNotification = "WinCondition.WinNotification";

	protected void Update()
	{
		if (CheckForWinCondition())
		{
			this.PostNotification(OnWinNotification);
		}
	}

	protected abstract bool CheckForWinCondition();
}