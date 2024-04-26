using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	[Header("Canvas")]
    public GameObject CanvasGame;
	public GameObject CanvasRestart;

	[Header("CanvasRestart")]
	public GameObject WinText;
	public GameObject LoseText;

	[Header("Other")]
	public AudioManager audioManager;
	
	public Score score;

	public PuckScript puckScript;
	public PlayerMovement playerMovement;
	public AI ai;

	public void ShowRestartCanvas(bool DidWin)
	{
		Time.timeScale = 0;
		CanvasGame.SetActive(false);
		CanvasRestart.SetActive(true);
		
		if (DidWin)
		{
			audioManager.PlayWonGame();
			LoseText.SetActive(false);
			WinText.SetActive(true);
		}
		else
		{
			audioManager.PlayLostGame();
			LoseText.SetActive(true);
			WinText.SetActive(false);
		}
	}

	public void RestartGame()
	{
		Time.timeScale = 1;

		CanvasGame.SetActive(true);
		CanvasRestart.SetActive(false);

		score.ResetScores();
		puckScript.CenterPuck();
		playerMovement.ResetPos();
		ai.ResetPos();
	}

	public void ShowMenu()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}
}
