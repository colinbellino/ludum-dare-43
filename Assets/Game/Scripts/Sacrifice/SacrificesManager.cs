using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class SacrificesManager : MonoBehaviour
{
	public const string OnChooseSacrificeNotification = "SacrificesManager.ChooseSacrificeNotification";

	[SerializeField][HideInInspector]
	private SacrificeDictionary sacrificesDictionary = new SacrificeDictionary();

	[HideInInspector]
	public List<Sacrifice> choices = new List<Sacrifice>();

	[Header("Sacrifice Icons UI")]
	[SerializeField]
	private Transform sacrificeIconsUiContainer;

	[SerializeField]
	private GameObject sacrificeIconUiItemPrefab;

	[Header("Selection UI")]
	[SerializeField]
	private Transform selectionUiContainer;

	[SerializeField]
	private GameObject selectionUiItemPrefab;

	[Header("Debug UI")]
	[SerializeField]
	private Transform debugUiContainer;

	[SerializeField]
	private GameObject debugUiItemPrefab;

	[Header("Sacrifice Item")]
	[SerializeField]
	private GameObject sacrificeItemPrefab;

	[SerializeField]
	private List<Transform> sacrificeSpawnPoints;

	[SerializeField]
	private UnityEvent onChooseSacrifice;

	private Settings settings;

	[Inject]
	public void Construct(Settings settings)
	{
		this.settings = settings;
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
		foreach (var sacrifice in settings.sacrifices)
		{
			sacrificesDictionary.Add(sacrifice.id, false);
		}

		RefreshSelectionUI();
		RefreshDebugUI();

		ToggleSacrificeUI(false);
	}

	private void OnChooseSacrifice(object sender, object args)
	{
		var sacrifice = (Sacrifice) args;
		ToggleSacrifice(sacrifice.id, true);
		onChooseSacrifice.Invoke();
	}

	private void OnStartSacrifice(object sender, object args)
	{
		this.choices = settings.sacrifices
			.Where(FilterAvailableSacrifice)
			.Shuffle(new System.Random())
			.Take(2)
			.ToList();

		RefreshSelectionUI();
		ToggleSacrificeUI(true);
		ToggleSacrificeItem(true);
	}

	private void ToggleSacrificeItem(bool toggle)
	{
		for (int i = 0; i < choices.Count; i++)
		{
			var sacrifice = choices[i];

			var instance = GameObject.Instantiate(sacrificeItemPrefab);
			instance.name = sacrifice.id;
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
		bool isActive;
		if (sacrificesDictionary.TryGetValue(sacrifice.id, out isActive))
		{
			return sacrifice.prefab && !isActive;
		}

		return false;
	}

	private void OnStartCombat(object sender, object args)
	{
		ToggleSacrificeUI(false);
	}

	private void ToggleSacrificeUI(bool value)
	{
		selectionUiContainer.gameObject.SetActive(value);
	}

	private void RefreshSelectionUI()
	{
		foreach (Transform child in selectionUiContainer)
		{
			GameObject.Destroy(child.gameObject);
		}

		foreach (var sacrifice in choices)
		{
			SpawnSelectionUIItem(sacrifice);
		}
	}

	private void RefreshDebugUI()
	{
		foreach (Transform child in debugUiContainer)
		{
			GameObject.Destroy(child.gameObject);
		}

		foreach (var keyValue in sacrificesDictionary)
		{
			SpawnDebugUIItem(keyValue);
		}
	}

	private void ToggleSacrifice(string key, bool value)
	{
		sacrificesDictionary[key] = value;
		RefreshDebugUI();

		if (value)
		{
			SpawnSacrificeActivator(key);
		}
	}

	private void SpawnSacrificeActivator(string key)
	{
		var sacrifice = settings.sacrifices.Find(s => s.id == key);
		if (!sacrifice)
		{
			Debug.LogWarning("Couln't find sacrifice: " + key);
			return;
		}

		var instance = GameObject.Instantiate(sacrifice.prefab);
		instance.name = $"Sacrifice activator: {sacrifice.label}";
		SpawnSacrificeIconUIItem(sacrifice);
	}

	private void SpawnSacrificeIconUIItem(Sacrifice sacrifice)
	{
		var instance = GameObject.Instantiate(sacrificeIconUiItemPrefab, sacrificeIconsUiContainer);
		instance.name = sacrifice.id;

		var ui = instance.GetComponent<SacrificeItemUI>();
		if (ui)
		{
			ui.image.sprite = sacrifice.image;
			ui.image.sprite = sacrifice.image;
		}
	}

	private void SpawnSelectionUIItem(Sacrifice sacrifice)
	{
		var instance = GameObject.Instantiate(selectionUiItemPrefab, selectionUiContainer);
		instance.name = sacrifice.id;

		var ui = instance.GetComponent<SacrificeItemUI>();
		if (ui)
		{
			ui.image.sprite = sacrifice.image;
			ui.label.text = sacrifice.label;
			// Finish the game loop
			ui.button.onClick.AddListener(() =>
			{
				this.PostNotification(OnChooseSacrificeNotification, sacrifice);
			});
		}
	}

	private void SpawnDebugUIItem(KeyValuePair<string, bool> keyValue)
	{
		var instance = GameObject.Instantiate(debugUiItemPrefab, debugUiContainer);
		instance.name = keyValue.Key;

		var ui = instance.GetComponent<SacrificeDebugItemUI>();
		if (ui)
		{
			ui.toggle.isOn = keyValue.Value;
			ui.toggle.onValueChanged.AddListener((value) => { ToggleSacrifice(keyValue.Key, value); });
			ui.label.text = keyValue.Key;
		}
	}
}

[System.Serializable]
public class SacrificeDictionary : SerializableDictionaryBase<string, bool> { }
