using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Score BonusScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        BonusScore.Increment(Score.eScore.PlayerScore);
    }
    //public void AddScore(int score)
    //{
      //  playerScore += score;
        //UpdateScoreUI();
    //}

    //private void UpdateScoreUI()
    //{
    //    scoreText.text = "Score: " + playerScore.ToString();
    //}
}
