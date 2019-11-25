using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Movement : MonoBehaviour
{
	[SerializeField][FormerlySerializedAs("rb")] private Rigidbody2D _rb;

	private Animator _animator;
	private IInputState _inputState;
	private float _hitAnimationTime;
	private float _invulnerabilityFrameCoolDown;
	private IStatsProvider _statsProvider;

	[Inject]
	public void Construct(IInputState inputState, IStatsProvider statsProvider, PlayerFacade playerFacade, EntitySettings settings)
	{
		_inputState = inputState;
		_statsProvider = statsProvider;
		_animator = playerFacade.Animator;
		_invulnerabilityFrameCoolDown = settings.InvincibilityFrameCoolDown;
		// Start a -_invulnerabilityFrameCoolDown to avoid trigger instantly
		_hitAnimationTime = -_invulnerabilityFrameCoolDown;
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
		var moveInput = _inputState.Move;
		UpdateVelocity(moveInput);

		if (_animator)
		{
			UpdateAnimator(moveInput);
		}
	}

	private void UpdateVelocity(Vector2 moveInput)
	{
		_rb.velocity = moveInput * _statsProvider.GetStat(StatTypes.MoveSpeed);
	}

	private void UpdateAnimator(Vector2 moveInput)
	{
		if (_hitAnimationTime + _invulnerabilityFrameCoolDown > Time.time )
		{
			_animator.Play("Hit");
		}
		else if (moveInput.magnitude > 0f)
		{
			_animator.Play("Walk");
		}
		else
		{
			_animator.Play("Idle");
		}

		if (moveInput.x != 0)
		{
			_animator.SetFloat("MoveX", moveInput.x);
			_animator.SetFloat("MoveY", 0f);
		}

		if (moveInput.y != 0)
		{
			_animator.SetFloat("MoveX", 0f);
			_animator.SetFloat("MoveY", moveInput.y);
		}
	}

	private void OnHit(object sender, object arg)
	{
		var isPlayer = ((GameObject) sender).CompareTag("Player");

		if (isPlayer)
		{
			_hitAnimationTime = Time.time;
		}
	}
}
