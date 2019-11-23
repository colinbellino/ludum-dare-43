using UnityEngine;

public class MoveDumblyAtPlayer : IBrainPart
{
	private readonly IInputState _inputState;
	private readonly Transform _transform;
	private readonly ITarget _target;
	private float _cooldown = 2f;
	private float _timestamp = Time.time;
	private Vector2 _moveVector;

	public MoveDumblyAtPlayer(IInputState inputState, Transform transform, ITarget target)
	{
		_inputState = inputState;
		_transform = transform;
		_target = target;
	}

	public void Tick()
	{
		if (Time.time - _timestamp > _cooldown)
		{
			_moveVector = (_target.Transform.position - _transform.position).normalized;
			_timestamp = Time.time;
		}

		_inputState.Move = new Vector2(_moveVector.x, _moveVector.y);
	}
}
