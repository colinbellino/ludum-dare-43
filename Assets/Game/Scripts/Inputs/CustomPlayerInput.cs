using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayerInput : InputBehaviour
{
	private void Update()
	{
		move = new Vector2(
			Input.GetAxis("Move Horizontal"),
			Input.GetAxis("Move Vertical")
		);

		fire = Vector3.Normalize(new Vector2(
			Input.GetAxis("Fire Horizontal"),
			Input.GetAxis("Fire Vertical")
		));

		submit = Input.GetButton("Submit");
	}
}
