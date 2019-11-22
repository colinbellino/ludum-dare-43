using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Health : MonoBehaviour
{
	public const string OnDeathNotification = "Health.OnDeathNotification";
	public const string OnHitNotification = "Health.OnHitNotification";

	public int Current { get; private set; }
	public int Max { get; private set; }

	private const int min = 0;
	private int defaultMax;
	private float iFrameStart;

	[SerializeField] private UnityEvent onDeathEvent;
	[SerializeField] private UnityEvent onHit;
	[SerializeField] private UnityEvent onStartup;
	[SerializeField] private float invulnerabilityFrameCoolDown = 0.3f;

	[Inject]
	public void Construct(EntitySettings settings)
	{
		Current = settings.Health;
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
		Max = Current;
		defaultMax = Current;
		onStartup.Invoke();
	}

	private void Damage(int damage)
	{
		if (!IsIFrame())
		{
			ClampHealth(Current - damage);

			if (Current <= 0)
			{
				onDeathEvent.Invoke();
				this.PostNotification(OnDeathNotification);
			}
		}
	}

	public void SetMaxHealth(int value)
	{
		Max = value;
		ClampHealth(Current);
	}

	public void ResetToMaxDefaultHealth()
	{
		Max = defaultMax;
	}

	public void DestroyItself()
	{
		Destroy(gameObject);
	}

	private void ClampHealth(int value)
	{
		Current = Mathf.Clamp(value, min, Max);
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
