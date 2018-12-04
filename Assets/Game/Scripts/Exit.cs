using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
	public const string OnTriggered = "Exit.Triggered";

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			this.PostNotification(OnTriggered, true);
		}
	}

	private void OnDisable()
	{
		this.PostNotification(OnTriggered, false);
	}
}
