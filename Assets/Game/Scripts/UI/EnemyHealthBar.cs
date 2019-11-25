using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EnemyHealthBar : MonoBehaviour
{
	[SerializeField] private GameObject _healthBarForeground;

	private IStatsProvider _statsProvider;

	[Inject]
	public void Construct(IStatsProvider statsProvider)
	{
		_statsProvider = statsProvider;
	}

	private void OnEnable()
	{
		this.AddObserver(OnHealthChange, Stats.DidChangeNotification(StatTypes.Health));
	}

	private void OnDisable()
	{
		this.RemoveObserver(OnHealthChange, Stats.DidChangeNotification(StatTypes.Health));
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

	private float CalculateHealthPercent =>
		(float) _statsProvider.GetStat(StatTypes.Health) / _statsProvider.GetStat(StatTypes.MaxHealth);
}
