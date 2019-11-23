using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
	[Header("Brain")]
	[SerializeField] private bool _moveTowardsPlayer;
	[SerializeField] private bool _shootAtPlayer;
	[SerializeField] private bool _moveDumblyAtPlayer;

	[Inject] private Vector3 _initialPosition;
	[Inject] private PlayerFacade _player;

	public override void InstallBindings()
	{
		Container.BindInterfacesAndSelfTo<EnemyFacade>().FromComponentOnRoot();
		Container.Bind<Transform>().FromComponentOnRoot();
		Container.Bind<GameObject>().FromInstance(gameObject);
		Container.BindInterfacesTo<EntityInitialPosition>().AsSingle();
		Container.BindInstance(_initialPosition).WhenInjectedInto<EntityInitialPosition>();

		InstallBrain();
	}

	private void InstallBrain()
	{
		Container.Bind<ITarget>().FromInstance(_player);
		Container.Bind<IInputState>().To<InputState>().AsSingle();

		if (_moveTowardsPlayer)
		{
			Container.Bind<IBrainPart>().To<MoveTowardsPlayer>().AsSingle();
		}
		if (_shootAtPlayer)
		{
			Container.Bind<IBrainPart>().To<ShootAtPlayer>().AsSingle();
		}
		if (_moveDumblyAtPlayer)
		{
			Container.Bind<IBrainPart>().To<MoveDumblyAtPlayer>().AsSingle();
		}
	}
}
