using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool bClicked = true;
    public bool bMove;
    
    private Rigidbody2D RigBo;
	Vector2 StartPos;

    public Transform BoundsHolder;
    Bounds playerBounds;

    Collider2D playerCollider;
    
    // Use this for initialization
    void Start () {
        RigBo = GetComponent<Rigidbody2D>();
		StartPos = RigBo.position;
        playerCollider = GetComponent<Collider2D>();

        playerBounds = new Bounds(BoundsHolder.GetChild(0).position.y,
                            BoundsHolder.GetChild(1).position.y,
                            BoundsHolder.GetChild(2).position.x,
                            BoundsHolder.GetChild(3).position.x);
		GetComponent<PlayerMovement>().enabled = false;
    }
	
    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (bClicked)
            {
                bClicked = false;

                if (playerCollider.OverlapPoint(mousePos))
                {
                    bMove = true;
                }
                else
                {
                    bMove = false;
                }
            }

            if (bMove)
            {
                Vector2 clampMousePos = new Vector2(Mathf.Clamp(mousePos.x, playerBounds.Left, playerBounds.Right),
                                                    Mathf.Clamp(mousePos.y, playerBounds.Down, playerBounds.Up));
                RigBo.MovePosition(clampMousePos);
            }
        }
        else
        {
            bClicked = true;
        }
    }

	public void ResetPos()
	{
		RigBo.position = StartPos;
	}	
}
