using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExitCondition : MonoBehaviour
{
	public const string OnShowExitNotification = "ExitCondition.ShowExitNotification";

	void Update()
	{
		if (CheckForExitCondition())
		{
			this.PostNotification(OnShowExitNotification);
		}
	}

	protected abstract bool CheckForExitCondition();
}
