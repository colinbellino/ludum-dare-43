using System;
using UnityEngine;
using Zenject;

public class EntityInstaller : MonoInstaller
{
	[SerializeField] private EntitySettings _settings;

	public override void InstallBindings()
	{
		Container.BindInstance(_settings);
	}
}

[Serializable]
public class EntitySettings
{
	public float MoveSpeed;
	public float FireRate;
	public int Health;
	public Alliances Alliance;
}
