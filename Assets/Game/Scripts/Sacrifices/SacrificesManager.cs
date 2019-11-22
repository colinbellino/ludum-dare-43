using System.Collections.Generic;
using System.Linq;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class SacrificesManager : MonoBehaviour
{
	// TODO: inject this
	[SerializeField] private GameObject sacrificeIconUiItemPrefab;
	[SerializeField] private GameObject selectionUiItemPrefab;
	[SerializeField] private GameObject sacrificeItemPrefab;

	[SerializeField] private Transform sacrificeIconsUiContainer;
	[SerializeField] private Transform selectionUiContainer;
	[SerializeField] private List<Transform> sacrificeSpawnPoints;
	[SerializeField] private UnityEvent onChooseSacrifice;

	private List<Sacrifice> _choices = new List<Sacrifice>();
	private GameSettings _settings;
	private SacrificeDictionary _sacrificesDictionary = new SacrificeDictionary();

	public const string OnChooseSacrificeNotification = "SacrificesManager.ChooseSacrificeNotification";

	[Inject]
	public void Construct(GameSettings settings)
	{
		_settings = settings;
	}

	private void OnEnable()
	{
		this.AddObserver(OnStartSacrifice, GameManager.OnStartSacrificeNotification);
		this.AddObserver(OnStartCombat, GameManager.OnStartCombatNotification);
		this.AddObserver(OnChooseSacrifice, OnChooseSacrificeNotification);
	}

	private void OnDisable()
	{
		this.RemoveObserver(OnStartSacrifice, GameManager.OnStartSacrificeNotification);
		this.RemoveObserver(OnStartCombat, GameManager.OnStartCombatNotification);
		this.RemoveObserver(OnChooseSacrifice, OnChooseSacrificeNotification);
	}

	private void Start()
	{
		foreach (var sacrifice in _settings.sacrifices)
		{
			_sacrificesDictionary.Add(sacrifice.id, false);
		}

		RefreshActiveSacrificesUI();
		SetSacrificeUIVisibility(false);
	}

	private void OnChooseSacrifice(object sender, object args)
	{
		var sacrifice = (Sacrifice) args;
		ToggleSacrifice(sacrifice.id, true);
		onChooseSacrifice.Invoke();
	}

	private void OnStartSacrifice(object sender, object args)
	{
		_choices = _settings.sacrifices
			.Where(FilterAvailableSacrifice)
			.Shuffle(new System.Random())
			.Take(2)
			.ToList();

		RefreshActiveSacrificesUI();
		SetSacrificeUIVisibility(true);
		ShowSacrificeChoices();
	}

	private void ShowSacrificeChoices()
	{
		for (int i = 0; i < _choices.Count; i++)
		{
			var sacrifice = _choices[i];

			var instance = Instantiate(sacrificeItemPrefab);
			instance.name = $"Sacrifice (choice): {sacrifice.label}";
			instance.transform.position = sacrificeSpawnPoints[i].transform.position;

			var pedestal = instance.GetComponent<SacrificePedestal>();
			if (pedestal)
			{
				pedestal.SetSacrifice(sacrifice);
			}
		}
	}

	private bool FilterAvailableSacrifice(Sacrifice sacrifice)
	{
		if (_sacrificesDictionary.TryGetValue(sacrifice.id, out bool isActive))
		{
			return sacrifice.prefab && !isActive;
		}

		return false;
	}

	private void OnStartCombat(object sender, object args)
	{
		SetSacrificeUIVisibility(false);
	}

	private void SetSacrificeUIVisibility(bool value)
	{
		selectionUiContainer.gameObject.SetActive(value);
	}

	private void RefreshActiveSacrificesUI()
	{
		foreach (Transform child in selectionUiContainer)
		{
			Destroy(child.gameObject);
		}

		foreach (var sacrifice in _choices)
		{
			SpawnActiveSacrificeUI(sacrifice);
		}
	}

	private void ToggleSacrifice(string key, bool value)
	{
		_sacrificesDictionary[key] = value;

		if (value)
		{
			SpawnSacrificeEffect(key);
		}
	}

	private void SpawnSacrificeEffect(string key)
	{
		var existingSacrifice = _settings.sacrifices.Find(s => s.id == key);
		if (!existingSacrifice)
		{
			Debug.LogWarning("Couln't find sacrifice: " + key);
			return;
		}

		var instance = Instantiate(existingSacrifice.prefab);
		instance.name = $"Sacrifice: {existingSacrifice.label}";
		var sacrifice = instance.GetComponent<ISacrifice>();
		sacrifice.OnApply();

		SpawnSacrificeIconUIItem(existingSacrifice);
	}

	private void SpawnSacrificeIconUIItem(Sacrifice sacrifice)
	{
		var instance = Instantiate(sacrificeIconUiItemPrefab, sacrificeIconsUiContainer);
		instance.name = sacrifice.id;

		var ui = instance.GetComponent<SacrificeItemUI>();
		if (ui)
		{
			ui.image.sprite = sacrifice.image;
			ui.image.sprite = sacrifice.image;
		}
	}

	private void SpawnActiveSacrificeUI(Sacrifice sacrifice)
	{
		var instance = Instantiate(selectionUiItemPrefab, selectionUiContainer);
		instance.name = sacrifice.id;

		var ui = instance.GetComponent<SacrificeItemUI>();
		if (ui)
		{
			ui.image.sprite = sacrifice.image;
		}
	}
}

[System.Serializable]
public class SacrificeDictionary : SerializableDictionaryBase<string, bool> { }
