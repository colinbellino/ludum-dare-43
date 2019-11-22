using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{
	public const string OnStartSacrificeNotification = "GameManager.StartSacrificeNotification";
	public const string OnStartCombatNotification = "GameManager.StartCombatNotification";
	public const string OnLevelSpawn = "GameManager.LevelSpawn";

	public PlayerFacade Player { get; private set; }

	[SerializeField]
	private Transform spawnPoint;

	[SerializeField]
	private GameObject exit;

	public AudioSource mainAudioSource;

	private int currentLevelIndex = -1;
	public GameObject currentLevel { get; private set; }
	private GameSettings settings;
	private bool isCombatPhase;
	public bool IsNoUiMode = false;

	[Inject]
	public void Construct(GameSettings settings)
	{
		this.settings = settings;
		// TODO: Clean this
		// I think this is a bad practice to use GameObject methods in Construct but we need the player to
		// be present when GameManager is injected. Also this is a game jam so, if it works...
		Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFacade>();
	}

	private void OnEnable()
	{
		this.AddObserver(OnWin, WinCondition.OnWinNotification);
		this.AddObserver(OnShowExit, ExitCondition.OnShowExitNotification);
		this.AddObserver(OnChooseSacrifice, SacrificesManager.OnChooseSacrificeNotification);
		this.AddObserver(OnDeath, Health.OnDeathNotification);
	}

	private void OnDisable()
	{
		this.RemoveObserver(OnWin, WinCondition.OnWinNotification);
		this.RemoveObserver(OnShowExit, ExitCondition.OnShowExitNotification);
		this.RemoveObserver(OnChooseSacrifice, SacrificesManager.OnChooseSacrificeNotification);
		this.RemoveObserver(OnDeath, Health.OnDeathNotification);
		this.RemoveObserver(SetNoUIMode, KnowledgeSacrifice.OnUiDisable);
	}

	private void Start()
	{
		var isDebugMode = spawnPoint == null;
		if (isDebugMode)
		{
			Invoke("StartCombat", 0.1f);
		}
		else
		{
			Invoke("StartNextGameplayPhase", 0.1f);
		}

		this.AddObserver(SetNoUIMode, KnowledgeSacrifice.OnUiDisable);
	}

	private void Update()
	{
		if (Input.GetButton("Cancel"))
		{
			SceneManager.LoadScene("MainMenu");
			return;
		}
	}

	private void SetNoUIMode(object sender, object args)
	{
		IsNoUiMode = true;
	}

	private void OnWin(object sender, object args)
	{
		if (IsLastLevel())
		{
			SceneManager.LoadScene("Finish");
			return;
		}

		DestroyCurrentLevel();
		StartNextGameplayPhase();
	}

	private void OnShowExit(object sender, object args)
	{
		exit.transform.position = spawnPoint.position;
		exit.SetActive(true);
	}

	private void StartNextGameplayPhase()
	{
		if (exit != null)
		{
			exit.SetActive(false);
		}

		if (isCombatPhase)
		{
			isCombatPhase = false;
			NextLevel();
			StartSacrifice();
		}
		else
		{
			isCombatPhase = true;
			NextLevel();
			StartCombat();
		}
	}

	private void StartSacrifice()
	{
		this.PostNotification(OnStartSacrificeNotification);
	}

	private void StartCombat()
	{
		this.PostNotification(OnStartCombatNotification);
	}

	private void OnChooseSacrifice(object sender, object args) => StartCombat();

	private void OnDeath(object sender, object args)
	{
		var actor = ((MonoBehaviour) sender).GetComponent<PlayerFacade>();
		if (actor == Player)
		{
			SceneManager.LoadScene("GameOver");
		}
	}

	public void DebuggerNextLevel()
	{
		if (IsLastLevel())
		{
			SceneManager.LoadScene("Finish");
			return;
		}

		DestroyCurrentLevel();
		StartNextGameplayPhase();
	}

	private void NextLevel()
	{
		Player.transform.position = spawnPoint.position;
		currentLevelIndex++;
		SpawnLevel();
	}

	private static GameObject SpawnLevel(GameObject level)
	{
		var instance = GameObject.Instantiate(level);
		return instance;
	}

	private void SpawnLevel()
	{
		var nextLevel = settings.levels[currentLevelIndex];
		currentLevel = SpawnLevel(nextLevel);
		this.PostNotification(OnLevelSpawn, nextLevel.name);

		Debug.Log("Loading next level: " + nextLevel.name);
	}

	private bool IsLastLevel()
	{
		return currentLevelIndex >= settings.levels.Count - 1;
	}

	private void DestroyCurrentLevel()
	{
		if (!currentLevel)
		{
			return;
		}

		GameObject.Destroy(currentLevel);
	}
}
