using UnityEngine;
using Zenject;

public abstract class ExitCondition : MonoBehaviour
{
	public const string OnShowExitNotification = "ExitCondition.ShowExitNotification";

	protected SacrificesManager _manager;

	private void Awake()
	{
		_manager = FindObjectOfType<SacrificesManager>();
	}

	private void Update()
	{
		if (CheckForExitCondition)
		{
			this.PostNotification(OnShowExitNotification);
		}
	}

	protected abstract bool CheckForExitCondition { get; }
}
