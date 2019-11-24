using UnityEngine;

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
