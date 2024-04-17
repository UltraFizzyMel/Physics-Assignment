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

	public int scoreValue = 1;


    private int aiScore, playerScore;
	public int PlayerScore
	{
		get {return playerScore;}
		set
		{
			playerScore = value;
			if (value == MaxScore)
				uiManager.ShowRestartCanvas(true);
		}
	}

	public int AIScore
	{
		get {return aiScore;}
		set
		{
			aiScore = value;
			if (value == MaxScore)
				uiManager.ShowRestartCanvas(false);
		}
	}


    public void Increment(eScore whoScore)
    {
        if (whoScore == eScore.PlayerScore)
            PlayerScoreText.text = (++PlayerScore).ToString();
        else 
            AIScoreText.text = (++AIScore).ToString();
    }

	public void Decrement(eScore MinusScore)
	{
		if (MinusScore == eScore.PlayerScore)
            PlayerScoreText.text = (--PlayerScore).ToString();
        else 
            AIScoreText.text = (--AIScore).ToString();
	}

	public void IncreasePlayerScore(int scoreToAdd)
    {
        PlayerScore += scoreToAdd;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Puck"))
        {
            //GameManager.Instance.IncreasePlayerScore(scoreValue);
			IncreasePlayerScore(scoreValue);
        }
    }

	public void ResetScores()
	{
		AIScore = PlayerScore = 0;
		AIScoreText.text = PlayerScoreText.text = "0";
	}
}
