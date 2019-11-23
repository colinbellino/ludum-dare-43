using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EnemyHealthBar : MonoBehaviour
{
	[SerializeField] private GameObject _healthBarForeground;

	private Stats _stats;

	[Inject]
	public void Construct(Stats stats)
	{
		_stats = stats;
	}

	private void OnEnable()
	{
		this.AddObserver(OnHealthChange, Stats.DidChangeNotification(StatTypes.Health), _stats);
	}

	private void OnDisable()
	{
		this.RemoveObserver(OnHealthChange, Stats.DidChangeNotification(StatTypes.Health), _stats);
	}

	private void Start() => UpdateLifePercent();

	private void OnHealthChange(object arg1, object arg2) => UpdateLifePercent();

	public void UpdateLifePercent()
	{
		if (_healthBarForeground.activeInHierarchy)
		{
			_healthBarForeground.GetComponent<Image>().fillAmount = CalculateHealthPercent;
		}
	}

	private float CalculateHealthPercent => (float) _stats[StatTypes.Health] / _stats[StatTypes.MaxHealth];
}
