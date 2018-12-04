using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
	[SerializeField] private GameObject _healthBarForeground;
	private Health _health;

	// Start is called before the first frame update
	private void Start()
	{
		_health = GetComponentInParent<Health>();
		UpdateLifePercent();
	}

	private float CalculateHealthPercent()
	{
		return (float) _health.current / _health.max;
	}

	public void UpdateLifePercent()
	{
		if (_healthBarForeground.active)
		{
			_healthBarForeground.GetComponent<Image>().fillAmount = CalculateHealthPercent();
		}
	}
}