using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Movement : MonoBehaviour
{
	[SerializeField][FormerlySerializedAs("rb")] private Rigidbody2D _rb;
	[SerializeField][FormerlySerializedAs("animator")] private Animator _animator;

	private IInputState _inputState;
	private EntitySettings _settings;
	public Speed Speed;

	public static string SpeedWillChangeNotification = "SpeedWillChangeNotification";

	[Inject]
	public void Construct(IInputState inputState, EntitySettings settings)
	{
		_inputState = inputState;
		_settings = settings;

		Speed = new Speed(_settings.MoveSpeed);
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
		_rb.velocity = moveInput * Speed.Current;
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

public class Speed
{
	private float _current;

	public float Current
	{
		get => _current;
		set => SetValue(value, true);
	}

	public Speed(float speed)
	{
		_current = speed;
	}

	private void SetValue(float value, bool allowExceptions)
	{
		var oldValue = _current;
		var newValue = value;

		if (oldValue == value)
		{
			return;
		}

		if (allowExceptions)
		{
			var exc = new ValueChangeException(oldValue, value);
			this.PostNotification("SpeedWillChangeNotification", exc);

			newValue = Mathf.FloorToInt(exc.GetModifiedValue());

			if (!exc.Toggle || newValue == oldValue)
			{
				return;
			}
		}

		_current = newValue;
	}
}
