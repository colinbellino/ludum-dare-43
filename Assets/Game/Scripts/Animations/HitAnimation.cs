using UnityEngine;
using Zenject;

public class HitAnimation : MonoBehaviour
{
	private GameObject _owner;
	private Animator _animator;
	private SpriteRenderer _renderer;

	private float _hitAnimationTime;
	private float _iFrameDuration;
	private Material _hitMaterial;
	private Material _defaultMaterial;

	[Inject]
	public void Construct(PlayerFacade playerFacade, EntitySettings settings)
	{
		_owner = playerFacade.GameObject;
		_animator = playerFacade.Animator;
		_renderer = playerFacade.Renderer;

		_hitMaterial = settings.HitMaterial;
		_defaultMaterial = _renderer.material;
		_iFrameDuration = settings.InvincibilityFrameCoolDown;
	}

	private void OnEnable()
	{
		this.AddObserver(OnHit, Health.OnHitNotification, _owner);
	}

	private void OnDisable()
	{
		this.RemoveObserver(OnHit, Health.OnHitNotification, _owner);
	}

	protected void Update()
	{
		if (Time.time > _hitAnimationTime)
		{
			_renderer.material = _defaultMaterial;
		}
	}

	private void OnHit(object sender, object arg)
	{
		if (Time.time > _hitAnimationTime)
		{
			_renderer.material = _hitMaterial;
			_hitAnimationTime = Time.time + _iFrameDuration;
		}
	}

	public void OnHitDone()
	{
		_renderer.material = _defaultMaterial;
		_animator.SetTrigger("Finished");
	}
}
