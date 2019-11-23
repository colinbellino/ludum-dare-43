using System;
using UnityEngine;
using Zenject;

public class ProjectileInstaller : MonoInstaller
{
	[Inject] private ProjectileSettings _projectileSettings;
	public override void InstallBindings()
	{
		Container.Bind<ProjectileFacade>().FromComponentOnRoot();
		Container.BindInterfacesTo<EntityInitialPosition>().AsSingle();

		Container.Bind<Transform>().FromComponentOnRoot();

		Container.BindInstance(_projectileSettings.InitialPositions).WhenInjectedInto<EntityInitialPosition>();
		Container.BindInstance(_projectileSettings);
	}
}
