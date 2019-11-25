using UnityEngine;
using Zenject;

public class SettingsToStats: IStatsProvider
{

	private Stats _stats = new Stats();

	[Inject]
	public void Construct(EntitySettings settings)
	{
		SetStatsFromSettings(settings);
	}
	public int GetStat(StatTypes name)
	{
		return _stats[name];
	}

	public void SetStat(StatTypes name, int value)
	{
		_stats[name] = value;
	}

	private void SetStatsFromSettings(EntitySettings settings)
	{
		_stats[StatTypes.MoveSpeed] = settings.MoveSpeed;
		_stats[StatTypes.Health] = settings.Health;
		_stats[StatTypes.MaxHealth] = settings.Health;
		_stats[StatTypes.FireRate] = settings.FireRate;
		_stats[StatTypes.Damage] = settings.Damage;
		_stats[StatTypes.ShotSpeed] = settings.ShotSpeed;
		_stats[StatTypes.ShotDirection] = settings.ShotDirection;
		_stats[StatTypes.ShotCount] = settings.ShotCount;
	}
}
