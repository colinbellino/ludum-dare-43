using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
	[Header("Brain")]
	[SerializeField] private bool _moveTowardsPlayer;
	[SerializeField] private bool _shootAtPlayer;

	[Inject] private Vector3 _initialPosition;
	[Inject] private GameManager _gameManager;

	public override void InstallBindings()
	{
		Container.BindInterfacesAndSelfTo<Enemy>().FromComponentOnRoot();
		Container.Bind<Transform>().FromComponentOnRoot();
		Container.Bind<GameObject>().FromInstance(gameObject);
		Container.BindInterfacesTo<EnemySpawnHandler>().AsSingle();
		Container.BindInstance(_initialPosition).WhenInjectedInto<EnemySpawnHandler>();

		Container.Bind<IInputState>().To<InputState>().AsSingle();

		InstallBrain();
	}

	private void InstallBrain()
	{
		Container.Bind<ITarget>().FromInstance(_gameManager.Player);

		if (_moveTowardsPlayer)
		{
			Container.Bind<IBrainPart>().To<MoveTowardsPlayer>().AsSingle();
		}
		if (_shootAtPlayer)
		{
			Container.Bind<IBrainPart>().To<ShootAtPlayer>().AsSingle();
		}
	}
}
