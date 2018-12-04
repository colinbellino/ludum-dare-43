using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIInput : InputBehaviour
{
	private float firstShootCooldown;
	private float startTime;

	private void OnEnable()
	{
		this.AddObserver(OnSetFireInput, AI.OnSetFireInputNotification, gameObject);
		this.AddObserver(OnSetMoveInput, AI.OnSetMoveInputNotification, gameObject);
	}

	private void OnDisable()
	{
		this.RemoveObserver(OnSetFireInput, AI.OnSetFireInputNotification, gameObject);
		this.RemoveObserver(OnSetMoveInput, AI.OnSetMoveInputNotification, gameObject);
	}

	private void Start()
	{
		startTime = Time.time;
		firstShootCooldown = Random.Range(0.3f, 1.3f);
	}

	private void OnSetFireInput(object sender, object args)
	{
		if (Time.time > startTime + firstShootCooldown)
		{
			var fireInput = (Vector2) args;
			fire = fireInput;
		}
	}

	private void OnSetMoveInput(object sender, object args)
	{
		var moveInput = (Vector2) args;
		move = moveInput;
	}
}
