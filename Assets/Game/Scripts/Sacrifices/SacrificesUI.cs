using UnityEngine;
using Zenject;

public class SacrificesUI : MonoBehaviour
{
	private SacrificesManager _manager;
	private SacrificeItemUI _prefab;

	[Inject]
	public void Construct(SacrificesManager manager, SacrificeItemUI prefab)
	{
		_manager = manager;
		_prefab = prefab;
	}

	private void OnEnable()
	{
		_manager.OnSacrificeActivated += OnSacrificeActivated;
	}

	private void OnDisable()
	{
		_manager.OnSacrificeActivated -= OnSacrificeActivated;
	}

	private void OnSacrificeActivated(SacrificeData data)
	{
		SpawnSacrificeIconUIItem(data);
	}

	private void SpawnSacrificeIconUIItem(SacrificeData data)
	{
		var instance = Instantiate(_prefab, transform);
		instance.name = data.id;

		var ui = instance.GetComponent<SacrificeItemUI>();
		ui.SetSacrifice(data);
	}
}
