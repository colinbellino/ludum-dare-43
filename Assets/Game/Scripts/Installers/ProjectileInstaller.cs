using System;
using UnityEngine;
using Zenject;

public class ProjectileInstaller : MonoInstaller
{
	[Inject] private ProjectileSettings _projectileSettings;
	public override void InstallBindings()
	{
		Container.Bind<ProjectileFacade>().FromComponentOnRoot();
		Container.Bind<Transform>().FromComponentOnRoot();

		Container.BindInterfacesTo<EntityInitialPosition>().AsSingle();
		Container.BindInterfacesTo<ProjectileAlliance>().AsSingle();


		Container.BindInstance(_projectileSettings.InitialPositions).WhenInjectedInto<EntityInitialPosition>();
		Container.BindInstance(_projectileSettings);
	}
}
