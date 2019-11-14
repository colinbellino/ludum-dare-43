using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Enemy))]
public class MoveTowardsPlayerAI : AI
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
		var moveVetor = (target.position - transform.position).normalized;

		var moveInput = new Vector2(
			moveVetor.x,
			moveVetor.y
		);
		gameObject.PostNotification(AI.OnSetMoveInputNotification, moveInput);
	}
}
