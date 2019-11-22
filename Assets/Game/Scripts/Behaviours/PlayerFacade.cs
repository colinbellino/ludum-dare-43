using UnityEngine;
using Zenject;

public class PlayerFacade : MonoBehaviour, ITarget, IEntity
{
	public Transform Transform => transform;
	public Alliances Alliance { get; private set; }
	public EntityStats Stats { get; private set; }

	[Inject]
	public void Construct(EntitySettings settings, EntityStats stats)
	{
		Alliance = settings.Alliance;
		Stats = stats;
	}
}
