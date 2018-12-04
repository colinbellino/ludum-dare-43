using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
	[SerializeField]
	private GameManager gameManager;

	[SerializeField]
	private Settings settings;

	public override void InstallBindings()
	{
		Container.BindInstance(gameManager).AsSingle();
		Container.BindInstance(settings).AsSingle();

		Container.Bind<SacrificesManager>()
			.FromComponentInChildren()
			.WhenInjectedInto<GameManager>();

		Container.BindFactory<Vector3, Enemy, Enemy.Factory>()
			.FromComponentInNewPrefab(settings.enemyPrefabs[0])
			.WhenInjectedInto<SkullSpawner>();

		Container.BindFactory<Vector3, Enemy, Enemy.Factory>()
			.FromComponentInNewPrefab(settings.enemyPrefabs[1])
			.WhenInjectedInto<TargetDummySpawner>();

		Container.BindFactory<Vector3, Enemy, Enemy.Factory>()
			.FromComponentInNewPrefab(settings.enemyPrefabs[2])
			.WhenInjectedInto<MuncherSpawner>();
	}
}
