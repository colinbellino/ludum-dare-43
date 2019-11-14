using UnityEngine;

public class Damager : MonoBehaviour
{
	[SerializeField] private int _damage = 1;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		collider.gameObject.PostNotification(Health.OnHitNotification, _damage);
	}
}
