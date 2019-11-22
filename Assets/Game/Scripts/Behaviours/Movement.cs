using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Movement : MonoBehaviour
{
	[SerializeField][FormerlySerializedAs("rb")] private Rigidbody2D _rb;
	[SerializeField][FormerlySerializedAs("animator")] private Animator _animator;

	private IInputState _inputState;
	private EntityStats _stats;

	[Inject]
	public void Construct(IInputState inputState, EntityStats stats)
	{
		_inputState = inputState;
		_stats = stats;
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
		_rb.velocity = moveInput * _stats.MoveSpeed.Current;
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
}
