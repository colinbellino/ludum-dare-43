using UnityEngine;
using Zenject;

public abstract class EnemySpawner : MonoBehaviour
{
	private GameManager gameManager;
	private EnemyFacade.Factory enemyFactory;

	[Inject]
	public void Construct(GameManager gameManager, EnemyFacade.Factory enemyFactory)
	{
		this.gameManager = gameManager;
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
		var spawnPoints = gameManager.currentLevel.transform.GetComponentsInChildren<EnemySpawnPoint>();
		foreach (var spawnPoint in spawnPoints)
		{
			if (spawnPoint.enemy == GetEnemyType())
			{
				enemyFactory.Create(spawnPoint.transform.position);
			}
		}
	}
}
