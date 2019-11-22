using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Zenject;

public class Shooter : MonoBehaviour
{
	[SerializeField][FormerlySerializedAs("projectilePrefab")] private GameObject _projectilePrefab;
	[SerializeField][FormerlySerializedAs("projectileOrigin")] private Transform _projectileOrigin;
	[SerializeField][FormerlySerializedAs("projectileOriginDistance")] private float _projectileOriginDistance = 0.8f;
	[SerializeField][FormerlySerializedAs("onFire")] private UnityEvent _onFired;

	private IInputState _inputState;
	private float _defaultCooldown;
	private float _fireTimestamp;
	private float _fireRate;

	[Inject]
	public void Construct(IInputState inputState, EntitySettings settings)
	{
		_inputState = inputState;
		_fireRate = settings.FireRate;
	}

	private void Start()
	{
		_defaultCooldown = _fireRate;
	}

	private void Update()
	{
		var fireInput = _inputState.Aim;
		if (fireInput.magnitude > 0f)
		{
			_projectileOrigin.localPosition = fireInput * _projectileOriginDistance;

			if (Time.time > _fireTimestamp)
			{
				SpawnProjectile(fireInput);
				_fireTimestamp = Time.time + _fireRate;
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
			SetCooldownValue(_fireRate * 2);
		}
		else
		{
			SetCooldownValue(_defaultCooldown);
		}
	}

	private void SetCooldownValue(float value)
	{
		_fireRate = value;
	}
}
