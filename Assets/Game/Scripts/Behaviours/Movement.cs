using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Movement : MonoBehaviour
{
	[SerializeField] [FormerlySerializedAs("rb")] private Rigidbody2D _rb;

	private Animator _animator;
	private IInputState _inputState;
	private float _hitAnimationTime;
	private float _iFrameDuration;
	private IStatsProvider _statsProvider;

	[Inject]
	public void Construct(IInputState inputState, IStatsProvider statsProvider, PlayerFacade playerFacade, EntitySettings settings)
	{
		_inputState = inputState;
		_statsProvider = statsProvider;
		_animator = playerFacade.Animator;
		_iFrameDuration = settings.InvincibilityFrameCoolDown;
	}

	private void OnEnable()
	{
		this.AddObserver(OnHit, Health.OnHitNotification, gameObject);
	}

	private void OnDisable()
	{
		this.RemoveObserver(OnHit, Health.OnHitNotification, gameObject);
	}

	private void Update()
	{
		UpdateVelocity();

		if (_animator)
		{
			UpdateAnimator();
		}
	}

	private void UpdateVelocity()
	{
		_rb.velocity = _inputState.Move * _statsProvider.GetStat(StatTypes.MoveSpeed);
	}

	private void UpdateAnimator()
	{
		if (_rb.velocity.magnitude > 0f)
		{
			_animator.SetTrigger("MoveStarted");
		}
		else
		{
			_animator.SetTrigger("MoveStopped");
		}

		if (_rb.velocity.magnitude > 0f)
		{
			_animator.SetFloat("MoveX", _rb.velocity.x);
			_animator.SetFloat("MoveY", _rb.velocity.y);
		}
	}

	private void OnHit(object sender, object arg)
	{
		if (Time.time > _hitAnimationTime)
		{
			_animator.SetTrigger("Hit");
			_animator.SetFloat("IFrameDuration", 1f / _iFrameDuration);
			_hitAnimationTime = Time.time + _iFrameDuration;
		}
	}
}
