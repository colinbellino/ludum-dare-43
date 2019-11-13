using UnityEngine;

public class TargetDummyAI : AI
{
	private void Update()
	{
		var aimInput = new Vector2(
			Random.Range(-1f, 1f),
			Random.Range(-1f, 1f)
		).normalized;

		this.PostNotification(OnSetFireInputNotification, aimInput);
	}
}
