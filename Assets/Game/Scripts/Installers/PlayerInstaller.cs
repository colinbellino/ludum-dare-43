using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
	[Inject] private GameSettings _settings;

	public override void InstallBindings()
	{
		Container.BindInterfacesTo<InputState>().AsSingle();
		Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
		Container.BindInterfacesAndSelfTo<AudioPlayer>().AsSingle();
		Container.Bind<AudioSource>().FromComponentOnRoot();
		Container.BindInterfacesTo<Wenk>().AsSingle();
		Container.BindInstance(_settings.PlayerSettings.Wenk);
	}
}
