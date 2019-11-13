using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
	[SerializeField] private GameObject HealthBar;

	private GameManager _gameManager;

	[Inject]
	public void Construct(GameManager gameManager)
	{
		_gameManager = gameManager;
	}

	private void Start()
	{
		this.AddObserver(ToggleHealthBarDisplay, KnowledgeSacrifice.OnUiDisable);
		if (_gameManager.IsNoUiMode)
		{
			ToggleHealthBarDisplay(null, false);
		}
	}

	private void OnDisable()
	{
		this.RemoveObserver(ToggleHealthBarDisplay, KnowledgeSacrifice.OnUiDisable);
	}

	private void ToggleHealthBarDisplay(object sender, object args)
	{
		HealthBar.gameObject.SetActive((bool) args);
	}

	public class Factory : PlaceholderFactory<Vector3, Enemy> { }
}
