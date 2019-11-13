using UnityEngine;
using Zenject;

[RequireComponent(typeof(Enemy))]
public class ShootAtPlayerAI : AI
{
	[SerializeField] private readonly float _cooldown = 1f;

	private GameManager _gameManager;
	private float _timestamp;

	[Inject]
	public void Construct(GameManager gameManager)
	{
		_gameManager = gameManager;
	}

	private void Update()
	{
		TryShoot();
	}

	private void TryShoot()
	{
		var isOnCooldown = Time.time < _timestamp;
		if (isOnCooldown) { return; }

		var target = _gameManager.player.transform;
		var fireVector = (target.position - transform.position).normalized;

		var fireInput = new Vector2(
			fireVector.x,
			fireVector.y
		);
		gameObject.PostNotification(OnSetFireInputNotification, fireInput);
		UnityEngine.Debug.Log("dooht");
		_timestamp = Time.time + _cooldown;
	}
}
