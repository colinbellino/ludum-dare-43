using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Zenject;

public class Shooter : MonoBehaviour
{
	[SerializeField][FormerlySerializedAs("projectilePrefab")] private GameObject _projectilePrefab;
	[SerializeField][FormerlySerializedAs("projectileOrigin")] private Transform _projectileOrigin;
	[SerializeField][FormerlySerializedAs("projectileOriginDistance")] private float _projectileOriginDistance = 0.8f;
	[SerializeField][FormerlySerializedAs("cooldown")] private float _cooldown = 0.3f;
	[SerializeField][FormerlySerializedAs("onFire")] private UnityEvent _onFired;

	private IInputState _inputState;
	private float defaultCooldown;
	private float fireTimestamp;

	[Inject]
	public void Construct(IInputState inputState)
	{
		_inputState = inputState;
	}

	private void Start()
	{
		defaultCooldown = _cooldown;
	}

	private void Update()
	{
		var fireInput = _inputState.Aim;
		if (fireInput.magnitude > 0f)
		{
			_projectileOrigin.localPosition = fireInput * _projectileOriginDistance;

			if (Time.time > fireTimestamp)
			{
				SpawnProjectile(fireInput);
				fireTimestamp = Time.time + _cooldown;
				_onFired.Invoke();
			}
		}
	}

	private void SpawnProjectile(Vector2 fireDirection)
	{
		var instance = Instantiate(_projectilePrefab);
		instance.transform.position = _projectileOrigin.position;

		var projectile = instance.GetComponent<Projectile>();
		if (projectile)
		{
			projectile.Shoot(fireDirection);
		}
		else
		{
			Debug.LogWarning($"Missing Projectile component on {_projectilePrefab.name}");
		}
	}

	public void SetCooldownMode(bool value)
	{
		if (value)
		{
			SetCooldownValue(_cooldown * 2);
		}
		else
		{
			SetCooldownValue(defaultCooldown);
		}
	}

	private void SetCooldownValue(float value)
	{
		_cooldown = value;
	}
}
