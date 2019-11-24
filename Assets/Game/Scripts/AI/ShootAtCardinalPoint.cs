using UnityEngine;

public class ShootAtCardinalPoint : IBrainPart
{
	private readonly IInputState _inputState;
	private readonly Transform _transform;
	private readonly ITarget _target;
	private string _cardinalDirection = "North";
	private float _timestamp;
	private float _fireCooldownRate = 0.3f;

	public ShootAtCardinalPoint(IInputState inputState, Transform transform, ITarget target)
	{
		_inputState = inputState;
		_transform = transform;
		_target = target;
		_timestamp = Time.time;
	}

	public void Tick()
	{
		_inputState.Aim = Vector2.zero;

		if (!(Time.time >= _timestamp + _fireCooldownRate)) return;

		_timestamp = Time.time + _fireCooldownRate;

		Vector2 aimVector;
		switch (_cardinalDirection)
		{
			case "North":
				aimVector = Vector2.up;
				_cardinalDirection = "East";
				break;
			case "East":
				aimVector = Vector2.right;
				_cardinalDirection = "South";
				break;
			case "South":
				aimVector = Vector2.down;
				_cardinalDirection = "West";
				break;
			default:
			case "West":
				aimVector = Vector2.left;
				_cardinalDirection = "North";
				break;

		}

		_inputState.Aim = aimVector;
	}
}
