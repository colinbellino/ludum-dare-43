using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
	[SerializeField]
	private List<Image> images;

	[SerializeField]
	private Sprite emptySprite;

	[SerializeField]
	private Sprite fullSprite;

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
			images[i].enabled = playerHealth.max > i;
			images[i].sprite = playerHealth.current > i ? fullSprite : emptySprite;
		}
	}

	private void Update()
	{
		UpdateHealthStatus();
	}
}
