using UnityEngine;
using Zenject;

public class HitAnimation : MonoBehaviour
{
	private Animator _animator;

	[Inject]
	public void Construct(PlayerFacade playerFacade)
	{
		_animator = playerFacade.Animator;
	}

	public void OnHit()
	{
		_animator.SetTrigger("Finished");
	}
}
