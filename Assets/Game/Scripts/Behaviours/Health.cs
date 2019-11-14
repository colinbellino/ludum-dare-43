using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
	public const string OnDeathNotification = "Health.OnDeathNotification";
	public const string OnHitNotification = "Health.OnHitNotification";

	public int current = 3;
	public int max { get; private set; }
	private int defaultMax;
	private const int min = 0;

	[SerializeField]
	private UnityEvent onDeathEvent;

	[SerializeField]
	private UnityEvent onHit;

	[SerializeField]
	private UnityEvent onStartup;

	[SerializeField] private float invulnerabilityFrameCoolDown = 0.3f;
	private float iFrameStart;

	private void OnEnable()
	{
		this.AddObserver(OnHit, OnHitNotification, gameObject);
	}

	private void OnDisable()
	{
		this.RemoveObserver(OnHit, OnHitNotification, gameObject);
	}

	private void OnHit(object sender, object args)
	{
		var damage = (int) args;
		Damage(damage);
		onHit.Invoke();
	}

	private void Start()
	{
		iFrameStart = Time.time;
		max = current;
		defaultMax = current;
		onStartup.Invoke();
	}

	private void Damage(int damage)
	{
		if (!IsIFrame())
		{
			ClampHealth(current - damage);

			if (current <= 0)
			{
				onDeathEvent.Invoke();
				this.PostNotification(OnDeathNotification);
			}	
		}
	}

	public void SetMaxHealth(int value)
	{
		max = value;
		ClampHealth(current);
	}

	public void ResetToMaxDefaultHealth()
	{
		max = defaultMax;
	}

	public void DestroyItself()
	{
		GameObject.Destroy(gameObject);
	}

	private void ClampHealth(int value)
	{
		current = Mathf.Clamp(value, min, max);
	}

	private bool IsIFrame()
	{
		return Time.time <= (iFrameStart + invulnerabilityFrameCoolDown);
	}

	public void SetInvulnerabilityFrame()
	{
		iFrameStart = Time.time;
	}
}
