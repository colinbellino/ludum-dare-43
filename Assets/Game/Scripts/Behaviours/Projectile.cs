using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField]
	private Rigidbody2D rb;

	[SerializeField]
	private float speed = 2f;

	[SerializeField]
	private int damage = 1;

	private Vector2 direction;
	private bool wasShot;

	private void Update()
	{
		if (wasShot)
		{
			rb.velocity = direction * speed;
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		var meAlliance = GetComponent<Alliance>();
		var colliderAlliance = collider.GetComponent<Alliance>();

		if (collider.CompareTag("Projectile") || (colliderAlliance && colliderAlliance.current == meAlliance.current))
		{
			return;
		}

		var targetHealth = collider.GetComponent<Health>();
		if (targetHealth)
		{
			collider.gameObject.PostNotification(Health.OnHitNotification, damage);
		}

		GameObject.Destroy(gameObject);
	}

	public void Shoot(Vector2 direction)
	{
		this.wasShot = true;
		this.direction = direction;
	}
}
