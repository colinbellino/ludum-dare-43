using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
	[Inject] private GameSettings _settings;

	public override void InstallBindings()
	{
		Container.Bind<GameManager>().FromComponentsInHierarchy().AsSingle();
		Container.Bind<SacrificesManager>().FromComponentsInHierarchy().AsSingle();
		Container.Bind<PlayerFacade>().FromComponentsInHierarchy().AsSingle();

		// TODO: Clean this up...
		Container.BindFactory<Vector3, EnemyFacade, EnemyFacade.Factory>()
			.FromSubContainerResolve()
			.ByNewContextPrefab<EnemyInstaller>(_settings.enemyPrefabs[0])
			.WhenInjectedInto<SkullSpawner>();

		Container.BindFactory<Vector3, EnemyFacade, EnemyFacade.Factory>()
			.FromSubContainerResolve()
			.ByNewContextPrefab<EnemyInstaller>(_settings.enemyPrefabs[1])
			.WhenInjectedInto<MuncherSpawner>();
	}
}
