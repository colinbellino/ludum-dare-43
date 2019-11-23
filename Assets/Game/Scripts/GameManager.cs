using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Transform spawnPoint;
	[SerializeField] private GameObject exit;

	public const string OnStartSacrificeNotification = "GameManager.StartSacrificeNotification";
	public const string OnStartCombatNotification = "GameManager.StartCombatNotification";
	public const string OnLevelSpawn = "GameManager.LevelSpawn";

	public GameObject currentLevel { get; private set; }
	public AudioSource mainAudioSource;
	public bool IsNoUiMode = false;

	private PlayerFacade _player;
	private int currentLevelIndex = -1;
	private GameSettings _settings;
	private bool _isCombatPhase;

	[Inject]
	public void Construct(GameSettings settings, PlayerFacade player)
	{
		_settings = settings;
		_player = player;
	}

	private void OnEnable()
	{
		this.AddObserver(OnWin, WinCondition.OnWinNotification);
		this.AddObserver(OnShowExit, ExitCondition.OnShowExitNotification);
		this.AddObserver(OnDeath, Health.OnDeathNotification);
	}

	private void OnDisable()
	{
		this.RemoveObserver(OnWin, WinCondition.OnWinNotification);
		this.RemoveObserver(OnShowExit, ExitCondition.OnShowExitNotification);
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

		if (_isCombatPhase)
		{
			_isCombatPhase = false;
			NextLevel();
			StartSacrifice();
		}
		else
		{
			_isCombatPhase = true;
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

	private void OnDeath(object sender, object args)
	{
		var actor = ((MonoBehaviour) sender).GetComponent<PlayerFacade>();
		if (actor == _player)
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
		_player.transform.position = spawnPoint.position;
		currentLevelIndex++;
		SpawnLevel();
	}

	private static GameObject SpawnLevel(GameObject level)
	{
		var instance = Instantiate(level);
		return instance;
	}

	private void SpawnLevel()
	{
		var nextLevel = _settings.levels[currentLevelIndex];
		currentLevel = SpawnLevel(nextLevel);
		this.PostNotification(OnLevelSpawn, nextLevel.name);

		Debug.Log("Loading next level: " + nextLevel.name);
	}

	private bool IsLastLevel()
	{
		return currentLevelIndex >= _settings.levels.Count - 1;
	}

	private void DestroyCurrentLevel()
	{
		if (!currentLevel)
		{
			return;
		}

		Destroy(currentLevel);
	}
}
