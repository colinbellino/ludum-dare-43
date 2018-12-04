using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentLevelText : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI text;

	private void OnEnable()
	{
		this.AddObserver(ChangeLevelName, GameManager.OnLevelSpawn);
	}

	private void OnDisable()
	{
		this.RemoveObserver(ChangeLevelName, GameManager.OnLevelSpawn);
	}

	private void ChangeLevelName(object sender, object args)
	{
		var levelName = (string) args;
		text.text = levelName != "Sacrifice" ? levelName : "To go deeper, you must sacrifice something.";
	}
}
