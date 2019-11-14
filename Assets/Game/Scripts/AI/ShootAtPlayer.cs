using UnityEngine;

public class ShootAtPlayer : IBrainPart
{
	private readonly IInputState _inputState;
	private readonly Transform _transform;
	private readonly ITarget _target;

	public ShootAtPlayer(IInputState inputState, Transform transform, ITarget target)
	{
		_inputState = inputState;
		_transform = transform;
		_target = target;
	}

	public void Tick()
	{
		var aimVector = (_target.Transform.position - _transform.position).normalized;
		_inputState.Aim = new Vector2(aimVector.x, aimVector.y);
	}
}
