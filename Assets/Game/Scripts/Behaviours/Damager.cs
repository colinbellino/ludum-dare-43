using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Damager : MonoBehaviour
{
	[SerializeField]
	private int damage = 1;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		collider.gameObject.PostNotification(Health.OnHitNotification, damage);
	}
}
