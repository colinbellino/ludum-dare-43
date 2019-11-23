public class SelectSacrificeExitCondition : ExitCondition
{
	private bool winStatus = false;

	private void OnEnable()
	{
		_manager.OnSacrificeActivated += OnSacrificeActivated;
	}

	private void OnDisable()
	{
		_manager.OnSacrificeActivated -= OnSacrificeActivated;
	}

	private void OnSacrificeActivated(SacrificeData data)
	{
		winStatus = true;
	}

	protected override bool CheckForExitCondition => winStatus;
}
