using UnityEngine;

public class ColorSacrifice : MonoBehaviour, ISacrifice
{
	public const string OnEnableNotification = "ColorSacrifice.EnableNotification";

	public void OnApply()
	{
		this.PostNotification(OnEnableNotification);
	}

	public void OnRemove()
	{
		throw new System.NotImplementedException();
	}
}
