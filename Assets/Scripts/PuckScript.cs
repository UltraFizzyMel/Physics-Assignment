using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour
{
    public Score ScoreInstance;
    public static bool WasGoal { get; private set; }
    public static bool WasHit { get; private set; }
    private Rigidbody2D RigBo;

    public float MaxSpeed;

    public AudioManager audioManager;

	public Score ScoreMinus;
	int Count = 0;

    // Start is called before the first frame update
    void Start()
    {
        RigBo = GetComponent<Rigidbody2D>();
        WasGoal = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!WasGoal)
        {
            if (other.tag == "AIGoal")
            {
                ScoreInstance.Increment(Score.eScore.PlayerScore);
                WasGoal = true;
                audioManager.PlayGoal();
                StartCoroutine(ResetPuck(false));
            }
            else if (other.tag == "PlayerGoal")
            {
                ScoreInstance.Increment(Score.eScore.AIScore);
                WasGoal = true;
                audioManager.PlayGoal();
                StartCoroutine(ResetPuck(true));
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D Hit)
    //{
	  //  if (!WasHit)
	    //{
		  //  if (other.collider == GetComponent)
		    //{
			    
		    //}
		    //else if (other.tag == "Player")
		    //{
			  //  ScoreInstance.Increment(Score.eScore.AIScore);
			    //WasGoal = true;
			    //audioManager.PlayGoal();
			    //StartCoroutine(ResetPuck(true));
		    //}
	    //}
    //}
    
	private void Update()
	{
		//if (other.gameObject.name == "Player")
		{
		//	++Count;
		//	if (Count == 2)
			{
		//		ScoreMinus.Decrement(Score.eScore.PlayerScore);
		//		Count = 0;
			}
		}
		//else if (other.gameObject.name == "AI")
		{
		//	++Count;
		//	if (Count == 2)
			{
		//		ScoreMinus.Decrement(Score.eScore.AIScore);
		//		Count = 0;
			}
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioManager.PlayPuckCollision();
    }

    private IEnumerator ResetPuck(bool didAIScore)
    {
        yield return new WaitForSecondsRealtime(1);
        WasGoal = false;
        RigBo.velocity = RigBo.position = new Vector2(0, 0);
		
        if (didAIScore)
            RigBo.position = new Vector2(0, -1);
        else
            RigBo.position = new Vector2(0, 1);
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
