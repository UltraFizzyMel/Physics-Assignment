using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour
{
    public Score ScoreInstance;
    public AudioManager audioManager;
    public Score ScoreMinus;
    public string lastCollidedWithTag;
    
    private Rigidbody2D RigBo;
    private GameObject LastHit;

    private bool WasPlayerHit;
    private bool WasAIHit;
    //private bool canDecreaseAIScore = true;
    //private bool canDecreasePlayerScore = true;
    public static bool WasGoal { get; private set; }
    
    public float MaxSpeed;
    
    //private GameObject nPlayer;
    //private GameObject nAI;
    //private PlayerMovement nPlayer;
    //private AI nAI;
	//public int playerCount = 0;
	//public int aiCount = 0;


    // Start is called before the first frame update
    private void Start()
    {
        RigBo = GetComponent<Rigidbody2D>();
        WasGoal = false;
        WasPlayerHit = false;
        WasAIHit = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!WasGoal)
        {
            if (other.gameObject.CompareTag("AIGoal"))
            {
                ScoreInstance.Increment(Score.eScore.PlayerScore);
                WasGoal = true;
                audioManager.PlayGoal();
                StartCoroutine(ResetPuck(false));
            }
            else if (other.gameObject.CompareTag("PlayerGoal"))
            {
                ScoreInstance.Increment(Score.eScore.AIScore);
                WasGoal = true;
                audioManager.PlayGoal();
                StartCoroutine(ResetPuck(true));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioManager.PlayPuckCollision();

        if (collision.gameObject.CompareTag("AITag"))
        {
	        //playerCount = 0;
	        //++aiCount;
	        HandleCollision("AITag");
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
	        //++playerCount;
	        //aiCount = 0;
	        HandleCollision("Player");
        }
    }
    
    private void HandleCollision(string collidedWithTag)
    {
	    if (LastHit != null && lastCollidedWithTag == collidedWithTag)
	    {
		    if (collidedWithTag == "Player" && ScoreMinus.PlayerScore > 0 && WasPlayerHit == false)
		    {
			    ScoreMinus.Decrement(Score.eScore.PlayerScore);
			    WasPlayerHit = true;
			    //canDecreasePlayerScore = false;
			    //++playerCount;
			    //playerCount = 1;
			    //aiCount = 0;
		    }
		    else if (collidedWithTag == "AITag" && ScoreMinus.AIScore > 0 && WasAIHit == false)
		    {
			    ScoreMinus.Decrement(Score.eScore.AIScore);
			    WasAIHit = true;
			    //canDecreaseAIScore = false;
			    //++aiCount;
			    //playerCount = 0;
			    //aiCount = 1;
		    }
	    }
	    LastHit = gameObject;
	    lastCollidedWithTag = collidedWithTag;
		StartCoroutine(WaitHit());
    }

	private IEnumerator WaitHit()
	{
		yield return new WaitForSecondsRealtime(2f);
		WasPlayerHit = false;
		WasAIHit = false;
	}

    private IEnumerator ResetPuck(bool didAIScore)
    {
        yield return new WaitForSecondsRealtime(1f);
        WasGoal = false;
        RigBo.velocity = RigBo.position = new Vector2(0, 0);
		
        if (didAIScore)
            RigBo.position = new Vector2(0, -1);
        else
            RigBo.position = new Vector2(0, 1);

		LastHit = null;
		lastCollidedWithTag = null;
    }

	public void CenterPuck()
	{
		RigBo.position = new Vector2(0, 0);
	}

    private void FixedUpdate()
    {
        RigBo.velocity = Vector2.ClampMagnitude(RigBo.velocity, MaxSpeed);
    }

}
