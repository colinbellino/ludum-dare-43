using UnityEngine;
using Zenject;

public class EntityEffect:  MonoBehaviour
{
	private GameObject _onHitParticulePrefab;
	private GameObject _onDeathParticulePrefab;

	[Inject]
	public void Construct(EntitySettings settings)
	{
		_onHitParticulePrefab = settings.OnHitParticulePrefab;
		_onDeathParticulePrefab = settings.OnDeathParticulePrefab;
	}
	private void OnEnable()
	{
		this.AddObserver(OnHit, Health.OnHitNotification, gameObject);
	}

	private void OnDisable()
	{
		this.RemoveObserver(OnHit, Health.OnHitNotification, gameObject);
	}

	private void OnHit(object sender, object args)
	{
		if (_onHitParticulePrefab == null)
		{
			return;
		}

		var instance = Instantiate(_onHitParticulePrefab);
		instance.transform.position = transform.position;
	}

	// Public method because it's use on a UnityEvent
	// PostNotification come too late and GameObject is already destroy :(
	public void OnDeath()
	{
		if (!_onDeathParticulePrefab) return;

		var instance = Instantiate(_onDeathParticulePrefab);
		instance.transform.position = transform.position;
	}
}
