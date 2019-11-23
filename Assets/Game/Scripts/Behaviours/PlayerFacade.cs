using UnityEngine;
using Zenject;

public class PlayerFacade : MonoBehaviour, ITarget, IEntity
{
	public Transform Transform => transform;
	public Alliances Alliance { get; private set; }
	public Stats Stats { get; private set; }

	[Inject]
	public void Construct(EntitySettings settings, Stats stats)
	{
		Alliance = settings.Alliance;
		Stats = stats;
	}
}
