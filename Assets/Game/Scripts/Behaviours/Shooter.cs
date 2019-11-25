using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Zenject;

public class Shooter : MonoBehaviour
{
	[SerializeField] [FormerlySerializedAs("projectileOrigin")]
	private Transform _projectileOrigin;

	[SerializeField] [FormerlySerializedAs("projectileOriginDistance")]
	private float _projectileOriginDistance = 0.8f;

	[SerializeField] [FormerlySerializedAs("onFire")]
	private UnityEvent _onFired;

	public const string OnFireNotification = "Shooter.OnFireNotification";

	private IInputState _inputState;
	private float _fireTimestamp;
	private ProjectileFacade.Factory _projectileFactory;
	private Alliances _alliances;
	private IStatsProvider _statsProvider;

	[Inject]
	public void Construct(IInputState inputState, IStatsProvider statsProvider, ProjectileFacade.Factory projectileFactory,
		EntitySettings settings)
	{
		_inputState = inputState;
		_projectileFactory = projectileFactory;
		_alliances = settings.Alliance;
		_statsProvider = statsProvider;
	}

	private void Update()
	{
		var fireInput = _inputState.Aim;

		if (!(fireInput.magnitude > 0f) || !(Time.time >= _fireTimestamp)) return;

		var shotAngles = GetShotAngles(_statsProvider.GetStat(StatTypes.ShotDirection));

		shotAngles.ForEach((shotAngle) =>
		{
			var shotDirection = GetProjectileDirection(fireInput, shotAngle);
			var projectilesAngles = GetProjectilesAngles(_statsProvider.GetStat(StatTypes.ShotCount));

			projectilesAngles.ForEach(angle =>
			{
				var projectileDirection = GetProjectileDirection(shotDirection, angle);
				var projectileSettings = new ProjectileSettings
				{
					InitialPositions = transform.position,
					Direction = projectileDirection,
					StatsProvider = _statsProvider,
					Alliances = _alliances,
				};
				_projectileFactory.Create(projectileSettings);
			});
		});

		_fireTimestamp = Time.time + _statsProvider.GetStat(StatTypes.FireRate) / 10f;
		_onFired.Invoke();
	}

	private List<float> GetShotAngles(int shotDirection)
	{
		var shotAngles = new List<float>();

		switch (shotDirection)
		{
			case 6:
				shotAngles.Add(90);
				shotAngles.Add(-90);
				shotAngles.Add(0);
				shotAngles.Add(180);
				break;
			case 5:
				shotAngles.Add(90);
				shotAngles.Add(-90);
				break;
			case 4:
				shotAngles.Add(90);
				break;
			case 3:
				shotAngles.Add(-90);
				break;
			case 2:
				shotAngles.Add(0);
				shotAngles.Add(180);
				break;
			case 1:
				shotAngles.Add(180);
				break;
			default:
				shotAngles.Add(0);
				break;
		}

		return shotAngles;
	}

	private List<float> GetProjectilesAngles(int shotCount)
	{
		var _projectilesAngles = new List<float>();

		switch (shotCount)
		{
			case 3:
				_projectilesAngles.Add(40);
				_projectilesAngles.Add(20);
				_projectilesAngles.Add(0);
				_projectilesAngles.Add(-20);
				_projectilesAngles.Add(-40);
				break;
			case 2:
				_projectilesAngles.Add(20);
				_projectilesAngles.Add(0);
				_projectilesAngles.Add(-20);
				break;
			case 1:
				_projectilesAngles.Add(20);
				_projectilesAngles.Add(-20);
				break;
			default:
				_projectilesAngles.Add(0);
				break;
		}

		return _projectilesAngles;
	}

	private static Vector2 GetProjectileDirection(Vector2 direction, float angle)
	{
		var _angle = angle * Mathf.Deg2Rad;
		var x = direction.x;
		var y = direction.y;
		var cos = Mathf.Cos(_angle);
		var sin = Mathf.Sin (_angle);

		var x2 = x * cos - y * sin;
		var y2 = x * sin + y * cos;

		return new Vector2(x2, y2);

	}
}

public class ProjectileSettings
{
	public IStatsProvider StatsProvider;
	public Alliances Alliances;
	public Vector3 InitialPositions;
	public Vector2 Direction;
}
