using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour, INotifier
{
	[SerializeField] private GameObject HealthBar;

	private GameManager _gameManager;
	private List<IBrainPart> _brain;

	[Inject]
	public void Construct(GameManager gameManager, List<IBrainPart> brain)
	{
		_gameManager = gameManager;
		_brain = brain;
	}

	private void OnEnable()
	{
		this.AddObserver(ToggleHealthBarDisplay, KnowledgeSacrifice.OnUiDisable);
	}

	private void Start()
	{
		if (_gameManager.IsNoUiMode)
		{
			ToggleHealthBarDisplay(null, false);
		}
	}

	private void Update()
	{
		foreach (var brainPart in _brain)
		{
			brainPart.Tick();
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

	public void NotifySelf(string name, object obj)
	{
		gameObject.PostNotification(name, obj);
	}

	public class Factory : PlaceholderFactory<Vector3, Enemy> { }
}
