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
	private Stats _stats;
	private float _fireTimestamp;
	private ProjectileFacade.Factory _projectileFactory;
	private Alliances _alliances;

	[Inject]
	public void Construct(IInputState inputState, Stats stats, ProjectileFacade.Factory projectileFactory,
		EntitySettings settings)
	{
		_inputState = inputState;
		_stats = stats;
		_projectileFactory = projectileFactory;
		_alliances = settings.Alliance;
	}

	private void Update()
	{
		var fireInput = _inputState.Aim;

		if (!(fireInput.magnitude > 0f) || !(Time.time >= _fireTimestamp)) return;

		var directions = GetShotDirections(_stats[StatTypes.ShotDirection]);

		directions.ForEach((direction) =>
		{
			var projectileSettings = new ProjectileSettings
			{
				InitialPositions = transform.position,
				Direction = direction,
				Stats = _stats,
				Alliances = _alliances,
			};

			_projectileFactory.Create(projectileSettings);
		});

		_fireTimestamp = Time.time + _stats[StatTypes.FireRate] / 10f;
		_onFired.Invoke();
	}

	private List<Vector2> GetShotDirections(int shotDirection)
	{
		var shotDirectionList = new List<Vector2>();
		var fireInput = _inputState.Aim;

		/*
		 * -90deg : shotDirectionList.Add(new Vector2 {x = -fireInput.y, y = fireInput.x});
		 * +90deg : shotDirectionList.Add(new Vector2 {x = fireInput.y, y = -fireInput.x});
		 * +-180deg : shotDirectionList.Add(-fireInput)
		 */

		switch (shotDirection)
		{
			case 6:
				shotDirectionList.Add(new Vector2 {x = fireInput.y, y = -fireInput.x});
				shotDirectionList.Add(new Vector2 {x = -fireInput.y, y = fireInput.x});
				shotDirectionList.Add(fireInput);
				shotDirectionList.Add(-fireInput);
				break;
			case 5:
				shotDirectionList.Add(new Vector2 {x = fireInput.y, y = -fireInput.x});
				shotDirectionList.Add(new Vector2 {x = -fireInput.y, y = fireInput.x});
				break;
			case 4:
				shotDirectionList.Add(new Vector2 {x = fireInput.y, y = -fireInput.x});
				break;
			case 3:
				shotDirectionList.Add(new Vector2 {x = -fireInput.y, y = fireInput.x});
				break;
			case 2:
				shotDirectionList.Add(fireInput);
				shotDirectionList.Add(-fireInput);
				break;
			case 1:
				shotDirectionList.Add(-fireInput);
				break;
			default:
				shotDirectionList.Add(fireInput);
				break;
		}

		return shotDirectionList;
	}

	private ShotDirectionSettings ConstructShotSettings(Vector2 input, Vector2 direction)
	{
		return new ShotDirectionSettings
		{
			InitialPositions = input,
			Direction = direction,
		};
	}
}

public class ShotDirectionSettings
{
	public Vector3 InitialPositions;
	public Vector2 Direction;
}

public class ProjectileSettings: ShotDirectionSettings
{
	public Stats Stats;
	public Alliances Alliances;
}
