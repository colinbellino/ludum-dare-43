using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class LevelManager
{
	private readonly List<GameObject> _levels;
	private int _currentLevelIndex;
	private GameObject _loadedLevel;

	public Action<string> OnLevelChanged = delegate { };

	public LevelManager(GameSettings settings)
	{
		_levels = settings.levels;
	}

	public void NextLevel()
	{
		DestroyCurrentLevel();

		if (_currentLevelIndex >= _levels.Count)
		{
			throw new LastLevelReachedException();
		}

		var nextlevel = _levels[_currentLevelIndex];
		_loadedLevel = LoadLevel(nextlevel);

		var levelName = nextlevel.name == "SacrificeLevel" ? "To go deeper, you must sacrifice something." : nextlevel.name;
		OnLevelChanged(levelName);

		_currentLevelIndex++;
	}

	private static GameObject LoadLevel(GameObject level)
	{
		return UnityEngine.Object.Instantiate(level);
	}

	private void DestroyCurrentLevel()
	{
		if (_loadedLevel == null)
		{
			return;
		}

		UnityEngine.Object.Destroy(_loadedLevel);
	}
}

[Serializable]
public class LastLevelReachedException : Exception
{
	public LastLevelReachedException() { }
	public LastLevelReachedException(string message) : base(message) { }
	public LastLevelReachedException(string message, Exception innerException) : base(message, innerException) { }
	protected LastLevelReachedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
