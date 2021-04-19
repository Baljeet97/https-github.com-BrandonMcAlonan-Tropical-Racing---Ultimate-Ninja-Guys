using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float maxSpeed = 0.5f;
    public float speedMultiplier = 10f;
    Rigidbody2D playerRB;
    Animator playerAnim;
    Collider2D coll2D;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        coll2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");

        float verticalAxis = Input.GetAxis("Vertical");

        float jump = Input.GetAxis("Jump");
        bool jumping_mario = Input.GetButtonDown("Jump");
        
        if (jumping_mario == true && coll2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerRB.AddForce(new Vector2(0, 10 * speedMultiplier));
        }

        if (horizontalAxis < 0 && this.transform.localScale.x == 1f)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else if (horizontalAxis >0 && this.transform.localScale.x == -1f)
        {
            transform.localScale = new Vector2(1f, 1f);
        }

        float rbVelocity = playerRB.velocity.magnitude;

        Idle2Run(rbVelocity);

        if (rbVelocity < maxSpeed)
        {
            playerRB.AddForce(new Vector2(horizontalAxis * speedMultiplier, 0));
        }

        if(jumping_mario == true)
        {
            playerRB.AddForce(new Vector2(0, 10 * speedMultiplier));
        }


        if(coll2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            playerRB.gravityScale = 0f;
        }
       
        if(Mathf.Abs(verticalAxis) > 0.2 && coll2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            Climb(verticalAxis);
            playerAnim.SetBool("climbing", true);
        }
        else if (Mathf.Abs(verticalAxis) <0.2)
        {
            playerAnim.SetBool("climbing", false);
        }
    }

    void Climb(float yDirection)
    {
        float climbSpeed = 3f;

        if (yDirection > 0)
            playerRB.velocity = new Vector2(0f, climbSpeed);
        else 
            playerRB.velocity = new Vector2(0f, -climbSpeed);
    }

    void Idle2Run(float velocity)
    {
        
        if(velocity > 0.1f)
            playerAnim.SetBool("running", true);
        else
            playerAnim.SetBool("running", false);
    }
}
