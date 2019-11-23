using UnityEngine;
using Zenject;

public class ProjectileFacade : MonoBehaviour
{
	[SerializeField] private Rigidbody2D _rigidbody;
	[SerializeField] private SpriteRenderer _spriteRenderer;

	private Vector2 _direction;
	private int _shotSpeed;
	private int _damage;
	private Alliances _alliances;

	public SpriteRenderer SpriteRenderer => _spriteRenderer;

	[Inject]
	public void Construct(ProjectileSettings projectileSettings)
	{
		_damage = projectileSettings.Stats[StatTypes.Damage];
		_shotSpeed = projectileSettings.Stats[StatTypes.ShotSpeed];
		_direction = projectileSettings.Direction;
		_alliances = projectileSettings.Alliances;
	}

	private void Update()
	{
		_rigidbody.velocity = _shotSpeed * _direction;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		var colliderEntity = collider.GetComponent<IEntity>();

		if (collider.CompareTag("Projectile") || (colliderEntity != null && colliderEntity.Alliance == _alliances))
		{
			return;
		}

		var targetHealth = collider.GetComponent<Health>();
		if (targetHealth)
		{
			collider.gameObject.PostNotification(Health.OnHitNotification, _damage);
		}

		GameObject.Destroy(gameObject);
	}

	public class Factory : PlaceholderFactory<ProjectileSettings, ProjectileFacade> {}
}
