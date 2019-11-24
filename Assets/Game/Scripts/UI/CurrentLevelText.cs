using TMPro;
using UnityEngine;
using Zenject;

public class CurrentLevelText : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _text;

	private LevelManager _levelManager;

	[Inject]
	public void Construct(LevelManager levelManager)
	{
		_levelManager = levelManager;
	}

	private void OnEnable()
	{
		_levelManager.OnLevelChanged += OnLevelChanged;
	}

	private void OnDisable()
	{
		_levelManager.OnLevelChanged -= OnLevelChanged;
	}

	private void OnLevelChanged(string levelName) => _text.text = levelName;
}
