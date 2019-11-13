using Zenject;

public class PlayerInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.BindInterfacesTo<InputState>().AsSingle();
		Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
	}
}
