using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerFacade : MonoBehaviour, ITarget, IEntity
{
	[SerializeField] private GameObject _onHitParticulePrefab;
	[SerializeField][FormerlySerializedAs("animator")] private Animator _animator;

	public Transform Transform => transform;
	public Alliances Alliance { get; private set; }
	public Stats Stats { get; private set; }

	public GameObject OnHitParticulePrefab => _onHitParticulePrefab;
	public Animator Animator => _animator;

	[Inject]
	public void Construct(EntitySettings settings, Stats stats)
	{
		Alliance = settings.Alliance;
		Stats = stats;
	}
}
