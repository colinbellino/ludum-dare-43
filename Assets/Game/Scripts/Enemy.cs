using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
	private GameManager gameManager;
	[SerializeField] private GameObject HealthBar;

	private void Start()
	{
		this.AddObserver(ToggleHealthBarDisplay, KnowledgeSacrifice.OnUiDisable);
		if (this.gameManager.IsNoUiMode)
		{
			ToggleHealthBarDisplay(null, false);
		}
	}

	[Inject]
	public void Construct(Vector3 initialPosition, GameManager gameManager)
	{
		this.gameManager = gameManager;
		this.transform.position = initialPosition;
	}

	public class Factory : PlaceholderFactory<Vector3, Enemy> { }

	private void ToggleHealthBarDisplay(object sender, object args)
	{
		HealthBar.gameObject.SetActive((bool) args);
	}

	private void OnDisable()
	{
		this.RemoveObserver(ToggleHealthBarDisplay, KnowledgeSacrifice.OnUiDisable);
	}
}
