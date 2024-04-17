using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float MaxMoveSpeed;
    private Rigidbody2D RigBo;
    private Vector2 StartPos;

    public Rigidbody2D Puck;

    public Transform AIBoundsHolder;
    private Bounds AIBounds;
    
    public Transform PuckBoundsHolder;
    private Bounds PuckBounds;

    private Vector2 TargetPos;

    private bool FirstTimeInHalf = true;
    private float OffsetFromTarget;
    
    // Start is called before the first frame update
    private void Start()
    {
        RigBo = GetComponent<Rigidbody2D>();
        StartPos = RigBo.position;
        
        AIBounds = new Bounds(AIBoundsHolder.GetChild(0).position.y,
            AIBoundsHolder.GetChild(1).position.y,
            AIBoundsHolder.GetChild(2).position.x,
            AIBoundsHolder.GetChild(3).position.x);
        
        PuckBounds = new Bounds(PuckBoundsHolder.GetChild(0).position.y,
            PuckBoundsHolder.GetChild(1).position.y,
            PuckBoundsHolder.GetChild(2).position.x,
            PuckBoundsHolder.GetChild(3).position.x);
		GetComponent<AI>().enabled = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!PuckScript.WasGoal)
        {
            float moveSpeed;

            if (Puck.position.y < PuckBounds.Down)
            {
                if (FirstTimeInHalf)
                {
                    FirstTimeInHalf = false;
                    OffsetFromTarget = Random.Range(-1f, 1f);
                }

                moveSpeed = MaxMoveSpeed * Random.Range(0.1f, 0.3f);
                TargetPos = new Vector2(Mathf.Clamp(Puck.position.x + OffsetFromTarget, AIBounds.Left, AIBounds.Right),
                    StartPos.y);
            }
            else
            {
                FirstTimeInHalf = true;

                moveSpeed = Random.Range(MaxMoveSpeed * 0.4f, MaxMoveSpeed);
                TargetPos = new Vector2(Mathf.Clamp(Puck.position.x, AIBounds.Left, AIBounds.Right),
                    (Mathf.Clamp(Puck.position.y, AIBounds.Down, AIBounds.Up)));
            }

            RigBo.MovePosition(Vector2.MoveTowards(RigBo.position, TargetPos, moveSpeed * Time.fixedDeltaTime));
        }
    }
	
	public void ResetPos()
	{
		RigBo.position = StartPos;
	}	
}
