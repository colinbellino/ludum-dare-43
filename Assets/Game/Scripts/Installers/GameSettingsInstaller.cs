using UnityEngine;
using Zenject;

public class GameSettingsInstaller : MonoInstaller
{
	[SerializeField] private GameSettings _settings;
	[SerializeField] private SacrificeItemUI _sacrificeItemUIPrefab;

	public override void InstallBindings()
	{
		Container.BindInstance(_settings).AsSingle();
		Container.BindInstance(_sacrificeItemUIPrefab).AsSingle();
	}
}
