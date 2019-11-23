using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
	[SerializeField] private GameObject _healthBarForeground;

	private Stats _stats;

	private void Start()
	{
		_stats = GetComponentInParent<IEntity>()?.Stats;
		UpdateLifePercent();
	}

	// FIXME: Subscribe to changes to Health / MaxHealth instead of calling this with UnityEvents
	public void UpdateLifePercent()
	{
		if (_healthBarForeground.activeInHierarchy)
		{
			_healthBarForeground.GetComponent<Image>().fillAmount = CalculateHealthPercent();
		}
	}

	private float CalculateHealthPercent()
	{
		return (float) _stats[StatTypes.Health] / (float) _stats[StatTypes.MaxHealth];
	}
}
