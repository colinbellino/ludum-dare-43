using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
	[SerializeField]
	private InputBehaviour input;

	[SerializeField]
	private GameObject projectilePrefab;

	[SerializeField]
	private Transform projectileOrigin;

	[SerializeField]
	private float projectileOriginDistance = 0.8f;

	[SerializeField]
	private float cooldown = 0.3f;

	[SerializeField]
	private UnityEvent onFire;

	private float defaultCooldown;
	private float fireTimestamp;

	private void Start()
	{
		defaultCooldown = cooldown;
	}

	private void Update()
	{
		var fireInput = input.GetFire();
		if (fireInput.magnitude > 0f)
		{
			projectileOrigin.localPosition = fireInput * projectileOriginDistance;

			if (Time.time > fireTimestamp)
			{
				SpawnProjectile(fireInput);
				fireTimestamp = Time.time + cooldown;
				onFire.Invoke();
			}
		}
	}

	private void SpawnProjectile(Vector2 fireDirection)
	{
		var instance = GameObject.Instantiate(projectilePrefab);
		instance.transform.position = projectileOrigin.position;

		var projectile = instance.GetComponent<Projectile>();
		if (projectile)
		{
			projectile.Shoot(fireDirection);
		}
		else
		{
			Debug.LogWarning($"Missing Projectile component on {projectilePrefab.name}");
		}
	}

	public void SetCooldownMode(bool value)
	{
		if (value)
		{
			SetCooldownValue(cooldown * 2);
		}
		else
		{
			SetCooldownValue(defaultCooldown);
		}
	}

	private void SetCooldownValue(float value)
	{
		cooldown = value;
	}
}
