using UnityEngine;
using Zenject;

public class PlayerInput : ITickable, IInitializable
{
	private readonly IInputState _inputState;
	private readonly SimpleControls _controls;

	public PlayerInput(IInputState inputState)
	{
		_inputState = inputState;
		_controls = new SimpleControls();
	}

	public void Initialize()
	{
		_controls.Gameplay.Enable();
	}

	public void Tick()
	{
		_inputState.Move = _controls.Gameplay.Move.ReadValue<Vector2>();
		_inputState.Aim = _controls.Gameplay.Aim.ReadValue<Vector2>();
		_inputState.Act = _controls.Gameplay.Act.ReadValue<float>() > 0;
	}
}
