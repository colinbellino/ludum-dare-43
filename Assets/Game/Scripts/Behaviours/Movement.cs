using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Movement : MonoBehaviour
{
	[SerializeField][FormerlySerializedAs("rb")] private Rigidbody2D _rb;
	[SerializeField][FormerlySerializedAs("animator")] private Animator _animator;

	private IInputState _inputState;
	private EntitySettings _settings;
	private float _currentSpeed;

	[Inject]
	public void Construct(IInputState inputState, EntitySettings settings)
	{
		_inputState = inputState;
		_settings = settings;

		_currentSpeed = _settings.MoveSpeed;
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
		_rb.velocity = moveInput * _currentSpeed;
	}

	private void UpdateAnimator(Vector2 moveInput)
	{
		if (moveInput.magnitude > 0f)
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

	public void SetSpeedMode(float speed)
	{
		MultiplySpeed(speed);
	}

	private void MultiplySpeed(float speed)
	{
		_currentSpeed *= speed;
	}

	public void ResetSpeed()
	{
		_currentSpeed = _settings.MoveSpeed;
	}
}
