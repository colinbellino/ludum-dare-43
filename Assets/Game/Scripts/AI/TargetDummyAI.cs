using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummyAI : AI
{
	private void Update()
	{
		var fireInput = new Vector2(
			Random.Range(-1f, 1f),
			Random.Range(-1f, 1f)
		).normalized;

		this.PostNotification(AI.OnSetFireInputNotification, fireInput);
	}
}
