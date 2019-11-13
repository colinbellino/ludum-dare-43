using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Movement : MonoBehaviour
{
	[SerializeField][FormerlySerializedAs("rb")] private Rigidbody2D _rb;
	[SerializeField][FormerlySerializedAs("animator")] private Animator _animator;
	[SerializeField][FormerlySerializedAs("speed")] private float _speed = 2f;

	private IInputState _inputState;
	private float _currentSpeed;
	private bool _isIceMode = false;
	private bool _isSpeedMode = false;

	[Inject]
	public void Construct(IInputState inputState)
	{
		_inputState = inputState;
	}

	private void Start()
	{
		_currentSpeed = _speed;
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
		if (_isIceMode)
		{
			var iceSpeed = _currentSpeed;
			if (_isSpeedMode)
			{
				iceSpeed = _speed * 1.3f;
			}
			_rb.AddForce(moveInput * iceSpeed);
		}
		else
		{
			_rb.velocity = moveInput * _currentSpeed;
		}
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
		_isSpeedMode = true;
		MultiplySpeed(speed);
	}

	private void MultiplySpeed(float speed)
	{
		_currentSpeed *= speed;
	}

	public void ResetSpeed()
	{
		_isSpeedMode = false;
		_currentSpeed = _speed;
	}

	public void SetIceMode(bool value)
	{
		_isIceMode = value;
	}
}
