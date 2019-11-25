using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class SacrificesManager : MonoBehaviour
{
	[SerializeField] private List<SacrificePedestal> _pedestals;
	[SerializeField] private UnityEvent _onChooseSacrifice;

	private List<SacrificeData> _availableSacrifices;

	public Action<SacrificeData> OnSacrificeActivated = delegate { };

	[Inject]
	public void Construct(GameSettings settings)
	{
		_availableSacrifices = settings.sacrifices;
	}

	private void OnEnable()
	{
		this.AddObserver(OnStartSacrificePhase, GameManager.OnStartSacrificeNotification);
	}

	private void OnDisable()
	{
		this.RemoveObserver(OnStartSacrificePhase, GameManager.OnStartSacrificeNotification);
	}

	private void Start()
	{
		SetPedestalsVisibility(false);
	}

	private void OnStartSacrificePhase(object sender, object args)
	{
		var selectableSacrifices = _availableSacrifices
			.Shuffle(new System.Random())
			.Take(2)
			.ToList();

		ShowSelectableSacrifices(selectableSacrifices);
	}

	private void SetPedestalsVisibility(bool value)
	{
		foreach (var pedestal in _pedestals)
		{
			pedestal.gameObject.SetActive(value);
		}
	}

	private void ShowSelectableSacrifices(List<SacrificeData> sacrifices)
	{
		for (int i = 0; i < sacrifices.Count; i++)
		{
			var sacrifice = sacrifices[i];

			var pedestal = _pedestals[i];
			pedestal.gameObject.SetActive(true);
			pedestal.transform.name = $"Sacrifice (choice): {sacrifice.label}";
			pedestal.SetSacrifice(sacrifice);
		}
	}

	public void ActivateSacrifice(SacrificeData data)
	{
		var instance = Instantiate(data.prefab);
		instance.name = $"Sacrifice: {data.label}";

		var sacrifice = instance.GetComponent<ISacrifice>();
		sacrifice.OnApply();

		OnSacrificeActivated(data);
		_onChooseSacrifice.Invoke();

		SetPedestalsVisibility(false);
		_availableSacrifices.Remove(data);
	}
}
