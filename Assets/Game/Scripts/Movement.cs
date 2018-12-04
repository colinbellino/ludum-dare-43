using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	[SerializeField]
	private InputBehaviour input;

	[SerializeField]
	private Rigidbody2D rb;

	[SerializeField]
	private Animator animator;

	[SerializeField]
	private float speed = 2f;
	private float currentSpeed;
	private bool isIceMode = false;
	private bool isSpeedMode = false;

	private void Start()
	{
		currentSpeed = speed;
	}

	private void Update()
	{
		var moveInput = input.GetMove();
		UpdateVelocity(moveInput);

		if (animator)
		{
			UpdateAnimator(moveInput);
		}
	}

	private void UpdateVelocity(Vector2 moveInput)
	{
		if (isIceMode)
		{
			var iceSpeed = currentSpeed;
			if (isSpeedMode)
			{
				iceSpeed = speed * 1.3f;
			}
			rb.AddForce(moveInput * iceSpeed);
		}
		else
		{
			rb.velocity = moveInput * currentSpeed;
		}
	}

	private void UpdateAnimator(Vector2 moveInput)
	{
		if (moveInput.magnitude > 0f)
		{
			animator.Play("Walk");
		}
		else
		{
			animator.Play("Idle");
		}

		if (moveInput.x != 0)
		{
			animator.SetFloat("MoveX", moveInput.x);
			animator.SetFloat("MoveY", 0f);
		}

		if (moveInput.y != 0)
		{
			animator.SetFloat("MoveX", 0f);
			animator.SetFloat("MoveY", moveInput.y);
		}
	}

	public void SetSpeedMode(float speed)
	{
		isSpeedMode = true;
		MultiplySpeed(speed);
	}

	private void MultiplySpeed(float speed)
	{
		currentSpeed *= speed;
	}

	public void ResetSpeed()
	{
		isSpeedMode = false;
		currentSpeed = speed;
	}

	public void SetIceMode(bool value)
	{
		isIceMode = value;
	}
}
