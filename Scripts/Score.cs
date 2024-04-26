using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public enum eScore
    {
        AIScore, PlayerScore
    }

    public Text AIScoreText, PlayerScoreText;
	public UIManager uiManager;
	public int MaxScore;

	//public int scoreValue = 1;


    private int aiScore, playerScore;
	public int PlayerScore
	{
		get {return playerScore;}
		set
		{
			playerScore = value;
			PlayerScoreText.text = playerScore.ToString();
			CheckWinCondition();
			//if (playerScore >= MaxScore)
			//	uiManager.ShowRestartCanvas(true);
		}
	}

	public int AIScore
	{
		get {return aiScore;}
		set
		{
			aiScore = value;
			AIScoreText.text = aiScore.ToString();
			CheckWinCondition();
			//if (value >= MaxScore)
			//	uiManager.ShowRestartCanvas(false);
		}
	}


    public void Increment(eScore whoScore)
    {
	    if (whoScore == eScore.PlayerScore)
		    ++PlayerScore;
	    //PlayerScoreText.text = (++PlayerScore).ToString();
	    else
		    ++AIScore;
		    //AIScoreText.text = (++AIScore).ToString();
		    Debug.Log(playerScore.ToString());
    }

	public void Decrement(eScore minusScore)
	{
		if (minusScore == eScore.PlayerScore && PlayerScore > 0)
			--PlayerScore;
		//PlayerScoreText.text = (--PlayerScore).ToString();
		else if (minusScore == eScore.AIScore && AIScore > 0)
			--AIScore;
		//AIScoreText.text = (--AIScore).ToString();
	}

	public void IncreasePlayerScore(eScore bonusScore)
	{
		if (bonusScore == eScore.PlayerScore)
			PlayerScore += 2;
		//PlayerScoreText.text = (PlayerScore + 2).ToString();
		else
			AIScore += 2;
			//AIScoreText.text = (AIScore + 2).ToString();
	}
	
	private void CheckWinCondition()
	{
		if (playerScore >= MaxScore)
			uiManager.ShowRestartCanvas(true);
		else if (aiScore >= MaxScore)
			uiManager.ShowRestartCanvas(false);
	}

	public void ResetScores()
	{
		AIScore = PlayerScore = 0;
		AIScoreText.text = PlayerScoreText.text = "0";
	}
}
