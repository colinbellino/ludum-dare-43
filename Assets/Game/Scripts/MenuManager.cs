using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	[SerializeField]
	private GameObject OptionsMenuContainer;

	public void QuitApplication()
	{
		Application.Quit();
	}

	public void StartGame()
	{
		SceneManager.LoadScene("Game");
	}

	public void ShowOptions()
	{
		OptionsMenuContainer.gameObject.SetActive(true);
	}

	public void HideOptions()
	{
		OptionsMenuContainer.gameObject.SetActive(false);
	}
}
