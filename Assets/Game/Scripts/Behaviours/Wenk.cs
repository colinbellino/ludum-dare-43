using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Zenject;

public class Wenk : MonoBehaviour
{
	[SerializeField][FormerlySerializedAs("cooldown")] private float _cooldown = 1f;
	[SerializeField][FormerlySerializedAs("onWenk")] private UnityEvent _onWenked;

	private IInputState _inputState;
	private float _wenkTimestamp;

	[Inject]
	public void Construct(IInputState inputState)
	{
		_inputState = inputState;
	}

	private void Update()
	{
		if (_inputState.Act)
		{
			TryWenk();
		}
	}

	private void TryWenk()
	{
		var isCoolingDown = Time.time < _wenkTimestamp;
		if (isCoolingDown)
		{
			return;
		}

		_onWenked.Invoke();
		_wenkTimestamp = Time.time + _cooldown;
	}
}
