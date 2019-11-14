using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
	[SerializeField] private GameManager gameManager;
	[SerializeField] private GameSettings settings;

	public override void InstallBindings()
	{
		Container.BindInstance(gameManager).AsSingle();
		Container.BindInstance(settings).AsSingle();

		Container.Bind<SacrificesManager>()
			.FromComponentInChildren()
			.WhenInjectedInto<GameManager>();

		// TODO: Clean this up...
		Container.BindFactory<Vector3, EnemyFacade, EnemyFacade.Factory>()
			.FromSubContainerResolve()
			.ByNewContextPrefab<EnemyInstaller>(settings.enemyPrefabs[0])
			.WhenInjectedInto<SkullSpawner>();

		Container.BindFactory<Vector3, EnemyFacade, EnemyFacade.Factory>()
			.FromSubContainerResolve()
			.ByNewContextPrefab<EnemyInstaller>(settings.enemyPrefabs[1])
			.WhenInjectedInto<MuncherSpawner>();
	}
}
