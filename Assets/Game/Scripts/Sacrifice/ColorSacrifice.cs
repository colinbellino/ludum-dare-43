using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSacrifice : SacrificeBehaviour
{
	public const string OnEnableNotification = "ColorSacrifice.EnableNotification";

	protected void OnEnable()
	{
		this.PostNotification(OnEnableNotification);
	}
}
