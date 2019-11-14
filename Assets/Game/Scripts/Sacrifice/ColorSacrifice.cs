public class ColorSacrifice : SacrificeBehaviour
{
	public const string OnEnableNotification = "ColorSacrifice.EnableNotification";

	protected void OnEnable()
	{
		this.PostNotification(OnEnableNotification);
	}
}