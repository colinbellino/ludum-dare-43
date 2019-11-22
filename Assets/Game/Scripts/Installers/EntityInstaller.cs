using System;
using UnityEngine;
using Zenject;

public class EntityInstaller : MonoInstaller
{
	[SerializeField] private EntitySettings _settings;

	public override void InstallBindings()
	{
		Container.BindInstance(_settings);

		var stats = new EntityStats
		{
			MoveSpeed = new Stat(Stats.MoveSpeed, _settings.MoveSpeed),
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
}
