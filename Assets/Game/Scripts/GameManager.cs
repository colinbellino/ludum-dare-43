using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Transform _spawnPoint;
	[SerializeField] private GameObject _exit;

	public const string OnStartSacrificeNotification = "GameManager.StartSacrificeNotification";
	public const string OnStartCombatNotification = "GameManager.StartCombatNotification";
	public bool IsNoUiMode { get; private set; }

	private LevelManager _levelManager;
	private PlayerFacade _player;
	private bool _isCombatPhase;

	[Inject]
	public void Construct(LevelManager levelManager, PlayerFacade player)
	{
		_levelManager = levelManager;
		_player = player;
	}

	private void OnEnable()
	{
		this.AddObserver(OnWin, WinCondition.OnWinNotification);
		this.AddObserver(OnShowExit, ExitCondition.OnShowExitNotification);
		this.AddObserver(OnDeath, Health.OnDeathNotification);

		_levelManager.OnLevelChanged += OnLevelChanged;
	}

	private void OnDisable()
	{
		this.RemoveObserver(OnWin, WinCondition.OnWinNotification);
		this.RemoveObserver(OnShowExit, ExitCondition.OnShowExitNotification);
		this.RemoveObserver(OnDeath, Health.OnDeathNotification);
		this.RemoveObserver(SetNoUIMode, KnowledgeSacrifice.OnUiDisable);

		_levelManager.OnLevelChanged -= OnLevelChanged;
	}

	private void Start()
	{
		this.AddObserver(SetNoUIMode, KnowledgeSacrifice.OnUiDisable);

		if (SceneManager.sceneCount > 1)
		{
			OnLevelChanged(SceneManager.GetSceneAt(1).name);
			return;
		}

		StartNextPhase();
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
		StartNextPhase();
	}

	private void OnShowExit(object sender, object args)
	{
		_exit.transform.position = _spawnPoint.position;
		_exit.SetActive(true);
	}

	private async void StartNextPhase()
	{
		if (_exit != null)
		{
			_exit.SetActive(false);
		}

		try
		{
			await _levelManager.NextLevel();
		}
		catch
		{
			SceneManager.LoadScene("Finish");
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

	private void OnLevelChanged(string levelName)
	{
		_player.transform.position = _spawnPoint.position;
		_isCombatPhase = levelName == "Sacrifice";

		if (_isCombatPhase)
		{
			StartSacrifice();
		}
		else
		{
			StartCombat();
		}
	}
}
