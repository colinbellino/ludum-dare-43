using UnityEngine;

public class MoveTowardsPlayer : IBrainPart
{
	private readonly INotifier _notifier;
	private readonly Transform _transform;
	private readonly ITarget _target;

	public MoveTowardsPlayer(INotifier notifier, Transform transform, ITarget target)
	{
		_notifier = notifier;
		_transform = transform;
		_target = target;
	}

	public void Tick()
	{
		var moveVector = (_target.Transform.position - _transform.position).normalized;
		var moveInput = new Vector2(moveVector.x, moveVector.y);

		_notifier.NotifySelf(Notifications.OnSetMoveInputNotification, moveInput);
	}
}
