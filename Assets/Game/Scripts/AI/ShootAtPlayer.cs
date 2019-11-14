using UnityEngine;

public class ShootAtPlayer : IBrainPart
{
	private readonly INotifier _notifier;
	private readonly Transform _transform;
	private readonly ITarget _target;

	public ShootAtPlayer(INotifier notifier, Transform transform, ITarget target)
	{
		_notifier = notifier;
		_transform = transform;
		_target = target;
	}

	public void Tick()
	{
		var aimVector = (_target.Transform.position - _transform.position).normalized;
		var fireInput = new Vector2(aimVector.x, aimVector.y);

		_notifier.NotifySelf(Notifications.OnSetFireInputNotification, fireInput);
	}
}
