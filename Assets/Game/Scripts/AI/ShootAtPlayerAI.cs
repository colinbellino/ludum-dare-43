using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Enemy))]
public class ShootAtPlayerAI : AI
{
	private GameManager gameManager;

	[Inject]
	public void Construct(GameManager gameManager)
	{
		this.gameManager = gameManager;
	}

	private void Update()
	{
		var target = gameManager.player.transform;
		var fireVector = (target.position - transform.position).normalized;

		var fireInput = new Vector2(
			fireVector.x,
			fireVector.y
		);
		gameObject.PostNotification(AI.OnSetFireInputNotification, fireInput);
	}
}
