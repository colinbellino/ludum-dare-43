using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerFacade : MonoBehaviour, ITarget, IEntity
{
	[SerializeField] [FormerlySerializedAs("animator")] private Animator _animator;
	[SerializeField] private SpriteRenderer _renderer;

	public Transform Transform => transform;
	public Alliances Alliance { get; private set; }
	public IStatsProvider StatsProvider { get; private set; }
	public Animator Animator => _animator;
	public SpriteRenderer Renderer => _renderer;
	public GameObject GameObject => gameObject;

	[Inject]
	public void Construct(EntitySettings settings, IStatsProvider statsProvider)
	{
		Alliance = settings.Alliance;
		StatsProvider = statsProvider;
	}
}
