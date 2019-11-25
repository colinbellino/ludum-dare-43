using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerFacade : MonoBehaviour, ITarget, IEntity
{
	[SerializeField][FormerlySerializedAs("animator")] private Animator _animator;

	public Transform Transform => transform;
	public Alliances Alliance { get; private set; }
	public IStatsProvider StatsProvider { get; private set; }
	public Animator Animator => _animator;

	[Inject]
	public void Construct(EntitySettings settings, IStatsProvider statsProvider)
	{
		Alliance = settings.Alliance;
		StatsProvider = statsProvider;
	}
}
