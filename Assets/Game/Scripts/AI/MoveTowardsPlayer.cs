using UnityEngine;

public class MoveTowardsPlayer : IBrainPart
{
	private readonly IInputState _inputState;
	private readonly Transform _transform;
	private readonly ITarget _target;

	public MoveTowardsPlayer(IInputState inputState, Transform transform, ITarget target)
	{
		_inputState = inputState;
		_transform = transform;
		_target = target;
	}

	public void Tick()
	{
		var moveVector = (_target.Transform.position - _transform.position).normalized;
		_inputState.Move = new Vector2(moveVector.x, moveVector.y);
	}
}
