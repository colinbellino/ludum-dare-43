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
	private Stats _stats;
	private float _fireTimestamp;

	[Inject]
	public void Construct(IInputState inputState, Stats stats)
	{
		_inputState = inputState;
		_stats = stats;
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
				_fireTimestamp = Time.time + _stats[StatTypes.FireRate] / 10f;
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
}
