using System;
using UnityEngine;
using Zenject;

public class EntityInstaller : MonoInstaller
{
	[SerializeField] private EntitySettings _settings;

	public override void InstallBindings()
	{
		Container.BindInstance(_settings);

		// TODO: Just fucking do this already: https://github.com/cbellino/unity-tactical-rpg-battle-system/blob/4e836db0920945cb0cfc71eb83bf9dd47ac28caf/Assets/Scripts/Tactical/Actor/Component/Stats.cs
		var stats = new EntityStats
		{
			MoveSpeed = new Stat(Stats.MoveSpeed, _settings.MoveSpeed),
				Health = new Stat(Stats.Health, _settings.Health),
				MaxHealth = new Stat(Stats.MaxHealth, _settings.Health),
		};
		Container.BindInstance(stats);
	}
}

[Serializable]
public class EntitySettings
{
	public int MoveSpeed;
	public float FireRate;
	public int Health;
	public Alliances Alliance;
}

public class EntityStats
{
	public Stat MoveSpeed;
	public Stat Health;
	public Stat MaxHealth;
}
