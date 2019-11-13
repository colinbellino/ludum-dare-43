using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
	[Inject] Vector3 _initialPosition;

	public override void InstallBindings()
	{
		Container.Bind<Enemy>().FromComponentOnRoot();
		Container.Bind<Transform>().FromComponentOnRoot();
		Container.Bind<GameObject>().FromInstance(gameObject);
		Container.BindInterfacesTo<EnemySpawnHandler>().AsSingle();
		Container.BindInstance(_initialPosition).WhenInjectedInto<EnemySpawnHandler>();

		Container.Bind<IInputState>().To<InputState>().AsSingle();
		Container.BindInterfacesAndSelfTo<AIInput>().AsSingle();
	}
}
