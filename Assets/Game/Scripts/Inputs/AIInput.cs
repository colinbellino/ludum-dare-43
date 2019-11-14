using System;
using UnityEngine;
using Zenject;

public class AIInput : IInitializable, IDisposable
{
	private readonly IInputState _inputState;
	private readonly GameObject _gameObject;

	public AIInput(IInputState inputState, GameObject gameObject)
	{
		_inputState = inputState;
		_gameObject = gameObject;
	}

	public void Initialize()
	{
		_gameObject.AddObserver(OnSetFireInput, AI.OnSetFireInputNotification, _gameObject);
		_gameObject.AddObserver(OnSetMoveInput, AI.OnSetMoveInputNotification, _gameObject);
	}

	public void Dispose()
	{
		_gameObject.RemoveObserver(OnSetFireInput, AI.OnSetFireInputNotification, _gameObject);
		_gameObject.RemoveObserver(OnSetMoveInput, AI.OnSetMoveInputNotification, _gameObject);
	}

	private void OnSetFireInput(object sender, object args)
	{
		_inputState.Aim = (Vector2) args;
	}

	private void OnSetMoveInput(object sender, object args)
	{
		_inputState.Move = (Vector2) args;
	}
}