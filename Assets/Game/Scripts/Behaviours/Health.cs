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

	private float _iFrameStart;
	private Stats _stats;

	[Inject]
	public void Construct(Stats stats)
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
		_iFrameStart = Time.time;
		onStartup.Invoke();
	}

	private void Damage(int damage)
	{
		if (!IsIFrame())
		{
			_stats[StatTypes.Health] -= damage;

			if (_stats[StatTypes.Health] <= 0)
			{
				onDeathEvent.Invoke();
				this.PostNotification(OnDeathNotification);
			}
		}
	}

	public void SetMaxHealth(int value)
	{
		UnityEngine.Debug.Log("FIXME:");
		// Max = value;
		// _stats[StatTypes.Health] = _stats[StatTypes.Health];
	}

	public void DestroyItself()
	{
		Destroy(gameObject);
	}

	private bool IsIFrame()
	{
		return Time.time <= (_iFrameStart + invulnerabilityFrameCoolDown);
	}

	public void SetInvulnerabilityFrame()
	{
		_iFrameStart = Time.time;
	}
}
