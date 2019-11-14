using System;
using UnityEngine;
using Zenject;

public class Wenk : ITickable
{
	private readonly AudioPlayer _audioPlayer;
	private readonly IInputState _inputState;
	private readonly Settings _settings;

	private float _timestamp;

	public Wenk(IInputState inputState, AudioPlayer audioPlayer, Settings settings)
	{
		_inputState = inputState;
		_audioPlayer = audioPlayer;
		_settings = settings;
	}

	public void Tick()
	{
		if (_inputState.Act)
		{
			TryWenk();
		}
	}

	private void TryWenk()
	{
		var isCoolingDown = Time.time < _timestamp;
		if (isCoolingDown)
		{
			return;
		}

		_audioPlayer.PlayRandom(_settings.Sounds);
		_timestamp = Time.time + _settings.Cooldown;
	}

	[Serializable]
	public class Settings
	{
		public float Cooldown = 1f;
		public AudioClip[] Sounds;
	}
}
