using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Zenject;

public class Shooter : MonoBehaviour
{
	public const string OnFireNotification = "Shooter.OnFireNotification";

	[SerializeField][FormerlySerializedAs("projectileOrigin")] private Transform _projectileOrigin;
	[SerializeField][FormerlySerializedAs("projectileOriginDistance")] private float _projectileOriginDistance = 0.8f;
	[SerializeField][FormerlySerializedAs("onFire")] private UnityEvent _onFired;

	private IInputState _inputState;
	private Stats _stats;
	private float _fireTimestamp;
	private ProjectileFacade.Factory _projectileFactory;
	private Alliances _alliances;

	[Inject]
	public void Construct(IInputState inputState, Stats stats, ProjectileFacade.Factory projectileFactory, EntitySettings settings)
	{
		_inputState = inputState;
		_stats = stats;
		_projectileFactory = projectileFactory;
		_alliances = settings.Alliance;
	}

	private void Update()
	{
		var fireInput = _inputState.Aim;

		if (!(fireInput.magnitude > 0f)) return;

		_projectileOrigin.localPosition = fireInput * _projectileOriginDistance;

		if (!(Time.time >= _fireTimestamp)) return;

		var projectileSetting = new ProjectileSettings
		{
			InitialPositions = _projectileOrigin.position,
			Direction = fireInput,
			Stats = _stats,
			Alliances = _alliances,
		};
		_fireTimestamp = Time.time + _stats[StatTypes.FireRate] / 10f;
		_onFired.Invoke();
		_projectileFactory.Create(projectileSetting);
	}
}

public class ProjectileSettings
{
	public Vector3 InitialPositions;
	public Vector2 Direction;
	public Stats Stats;
	public Alliances Alliances;
}
