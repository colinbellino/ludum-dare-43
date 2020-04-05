using System;
using UnityEngine;
using Zenject;

public class EntityInstaller : MonoInstaller
{
	[SerializeField] private EntitySettings _settings;

	public override void InstallBindings()
	{
		Container.BindInstance(_settings);

		Container.Bind<IStatsProvider>().To<SettingsToStats>().AsSingle();

		Container.BindInstance(_settings).WhenInjectedInto<SettingsToStats>();
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
	public int ShotDirection;
	public int ShotCount;
	public Alliances Alliance;

	public float InvincibilityFrameCoolDown;
	public Material HitMaterial;
	public GameObject OnHitParticulePrefab;

	public GameObject OnDeathParticulePrefab;
}
