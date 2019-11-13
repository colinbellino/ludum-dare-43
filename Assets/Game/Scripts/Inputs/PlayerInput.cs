using UnityEngine;
using Zenject;

public class PlayerInput : ITickable
{
	private readonly IInputState _inputState;

	public PlayerInput(IInputState inputState)
	{
		_inputState = inputState;
	}

	public void Tick()
	{
		_inputState.Move = new Vector2(
			Input.GetAxis("Move Horizontal"),
			Input.GetAxis("Move Vertical")
		);

		_inputState.Aim = Vector3.Normalize(new Vector2(
			Input.GetAxis("Fire Horizontal"),
			Input.GetAxis("Fire Vertical")
		));

		_inputState.Act = Input.GetButton("Submit");
	}
}
