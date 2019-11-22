using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Health : MonoBehaviour
{
	[SerializeField] private UnityEvent onDeathEvent;
	[SerializeField] private UnityEvent onHit;
	[SerializeField] private UnityEvent onStartup;
	[SerializeField] private float invulnerabilityFrameCoolDown = 0.3f;

	public const string OnDeathNotification = "Health.OnDeathNotification";
	public const string OnHitNotification = "Health.OnHitNotification";

	// public int Current => _stats.Health.Current;
	public int Max { get; private set; }

	private const int min = 0;
	private float iFrameStart;
	private EntityStats _stats;

	[Inject]
	public void Construct(EntityStats stats)
	{
		_stats = stats;
	}

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
		Max = _stats.MaxHealth.Current;
		onStartup.Invoke();
	}

	private void Damage(int damage)
	{
		if (!IsIFrame())
		{
			_stats.Health.Current -= damage;

			if (_stats.Health.Current <= 0)
			{
				onDeathEvent.Invoke();
				this.PostNotification(OnDeathNotification);
			}
		}
	}

	public void SetMaxHealth(int value)
	{
		Max = value;
		_stats.Health.Current = _stats.Health.Current;
	}

	public void DestroyItself()
	{
		Destroy(gameObject);
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
