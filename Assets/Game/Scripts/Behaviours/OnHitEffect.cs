using UnityEngine;
using Zenject;

public class OnHitEffect: MonoBehaviour
{
	private GameObject _onHitParticulePrefab;
	private Transform _transform;

	[Inject]
	public void Construct(PlayerFacade playerFacade)
	{
		_onHitParticulePrefab = playerFacade.OnHitParticulePrefab;
		_transform = playerFacade.Transform;
	}
	private void OnEnable()
	{
		this.AddObserver(OnHit, Health.OnHitNotification);
	}

	private void OnDisable()
	{
		this.RemoveObserver(OnHit, Health.OnHitNotification);
	}

	private void OnHit(object sender, object args)
	{
		var isPlayer = ((GameObject) sender).CompareTag("Player");

		if (isPlayer)
		{
			var instance = Instantiate(_onHitParticulePrefab);
			var offsetY = new Vector3 {x = 0, y = 1, z = 0};
			instance.transform.position = _transform.position + offsetY;
		}

	}
}
