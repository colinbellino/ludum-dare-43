using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class LevelManager
{
	private readonly List<AssetReference> _levels;
	private int _currentLevelIndex;
	private SceneInstance _loadedLevel;

	public Action<string> OnLevelChanged = delegate { };

	public LevelManager(GameSettings settings)
	{
		_levels = settings.Levels;
	}

	public async Task NextLevel()
	{
		Addressables.UnloadSceneAsync(_loadedLevel);

		if (_currentLevelIndex >= _levels.Count)
		{
			throw new LastLevelReachedException();
		}

		var nextlevel = _levels[_currentLevelIndex];
		_loadedLevel = await Addressables.LoadSceneAsync(nextlevel, LoadSceneMode.Additive).Task;

		var levelName = _loadedLevel.Scene.name;
		OnLevelChanged(levelName);

		_currentLevelIndex++;
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
