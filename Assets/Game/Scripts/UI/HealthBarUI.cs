using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
	[SerializeField] private List<Image> images;
	[SerializeField] private Sprite emptySprite;
	[SerializeField] private Sprite fullSprite;

	private Stats _stats;

	private void Start()
	{
		_stats = GameObject.Find("Player")?.GetComponent<IEntity>()?.Stats;
		UpdateHealthStatus();
	}

	private void Update()
	{
		// FIXME: Subscribe to changes instead of updating EVERY SINGLE FRAME
		UpdateHealthStatus();
	}

	public void UpdateHealthStatus()
	{
		for (int i = 0; i < images.Count; i++)
		{
			images[i].enabled = _stats[StatTypes.MaxHealth] > i;
			images[i].sprite = _stats[StatTypes.Health] > i ? fullSprite : emptySprite;
		}
	}
}
