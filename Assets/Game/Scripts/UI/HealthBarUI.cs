using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
	[SerializeField] private List<Image> images;
	[SerializeField] private Sprite emptySprite;
	[SerializeField] private Sprite fullSprite;
	private IStatsProvider _statsProvider;


	private void Start()
	{
		var statProvider = GameObject.Find("Player")?.GetComponent<PlayerFacade>().StatsProvider;
		UpdateHealthStatus();

		if (statProvider == null) return;

		_statsProvider = statProvider;
	}

	private void Update()
	{
		// FIXME: Subscribe to changes instead of updating EVERY SINGLE FRAME
		UpdateHealthStatus();
	}

	public void UpdateHealthStatus()
	{
		if (_statsProvider == null) return;

		var _health = _statsProvider.GetStat(StatTypes.Health);
		var _maxHealth = _statsProvider.GetStat(StatTypes.MaxHealth);

		for (int i = 0; i < images.Count; i++)
		{
			images[i].enabled = _maxHealth > i;
			images[i].sprite = _health > i ? fullSprite : emptySprite;
		}
	}
}
