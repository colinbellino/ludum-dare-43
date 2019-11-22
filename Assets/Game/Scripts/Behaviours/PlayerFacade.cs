using UnityEngine;
using Zenject;

public class PlayerFacade : MonoBehaviour, ITarget, IEntity
{
	public Transform Transform => transform;
	public Alliances Alliance { get; private set; }

	[Inject]
	public void Construct(EntitySettings settings)
	{
		Alliance = settings.Alliance;
	}
}
