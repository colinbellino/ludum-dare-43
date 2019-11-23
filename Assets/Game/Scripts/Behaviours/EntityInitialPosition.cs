using UnityEngine;
using Zenject;

public class EntityInitialPosition : IInitializable
{
	readonly Transform _transform;
	readonly Vector3 _initialPosition;

	public EntityInitialPosition(
		Transform transform,
		Vector3 initialPosition
	)
	{
		_transform = transform;
		_initialPosition = initialPosition;
	}

	public void Initialize()
	{
		_transform.position = _initialPosition;
	}
}
