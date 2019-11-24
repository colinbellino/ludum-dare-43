using System;
using UnityEngine;
using Zenject;

public class EntityInstaller : MonoInstaller
{
	[SerializeField] private EntitySettings _settings;

	public override void InstallBindings()
	{
		Container.BindInstance(_settings);

		var stats = new Stats();
		stats[StatTypes.MoveSpeed] = _settings.MoveSpeed;
		stats[StatTypes.Health] = _settings.Health;
		stats[StatTypes.MaxHealth] = _settings.Health;
		stats[StatTypes.FireRate] = _settings.FireRate;
		stats[StatTypes.Damage] = _settings.Damage;
		stats[StatTypes.ShotSpeed] = _settings.ShotSpeed;

		Container.BindInstance(stats);
	}
}

[Serializable]
public class EntitySettings
{
	public int MoveSpeed;
	public int FireRate;
	public int Health;
	public int Damage;
	public int ShotSpeed;
	public Alliances Alliance;
	public float InvincibilityFrameCoolDown;
}
