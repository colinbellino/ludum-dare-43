using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
	[SerializeField] private List<Image> images;
	[SerializeField] private Sprite emptySprite;
	[SerializeField] private Sprite fullSprite;

	private Health playerHealth;

	private void Start()
	{
		var player = GameObject.Find("Player");
		playerHealth = player.GetComponent<Health>();

		UpdateHealthStatus();
	}

	public void UpdateHealthStatus()
	{
		for (int i = 0; i < images.Count; i++)
		{
			images[i].enabled = playerHealth.Max > i;
			images[i].sprite = playerHealth.Current > i ? fullSprite : emptySprite;
		}
	}

	private void Update()
	{
		UpdateHealthStatus();
	}
}
