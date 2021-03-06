using UnityEngine;
using Zenject;

public abstract class EnemySpawner : MonoBehaviour
{
	private EnemyFacade.Factory enemyFactory;

	[Inject]
	public void Construct(EnemyFacade.Factory enemyFactory)
	{
		this.enemyFactory = enemyFactory;
	}

	protected abstract Enemies GetEnemyType();

	private void OnEnable()
	{
		this.AddObserver(OnStartCombat, GameManager.OnStartCombatNotification);
	}

	private void OnDisable()
	{
		this.RemoveObserver(OnStartCombat, GameManager.OnStartCombatNotification);
	}

	private void OnStartCombat(object sender, object args)
	{
		SpawnEnemies();
	}

	private void SpawnEnemies()
	{
		var spawnPoints = FindObjectsOfType<EnemySpawnPoint>();
		foreach (var spawnPoint in spawnPoints)
		{
			if (spawnPoint.enemy == GetEnemyType())
			{
				enemyFactory.Create(spawnPoint.transform.position);
			}
		}
	}
}
