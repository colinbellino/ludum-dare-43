using UnityEngine;

public abstract class ExitCondition : MonoBehaviour
{
	public const string OnShowExitNotification = "ExitCondition.ShowExitNotification";

	protected SacrificesManager _manager;
	private bool _wasNotificationSent;

	private void Awake()
	{
		_manager = FindObjectOfType<SacrificesManager>();
	}

	private void Update()
	{
		if (CheckForExitCondition && _wasNotificationSent == false)
		{
			this.PostNotification(OnShowExitNotification);
			_wasNotificationSent = true;
		}
	}

	protected abstract bool CheckForExitCondition { get; }
}
