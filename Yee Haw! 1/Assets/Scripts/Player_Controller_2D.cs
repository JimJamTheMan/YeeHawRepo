using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller_2D : MonoBehaviour 
{
    public Barrel Barrel;
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    bool isGrounded;
    bool isFliped;
    bool isCrouched;
    bool isJumping;
    [SerializeField]
    Transform groundCheck;

    private Barrel barrel;

    // Start is called before the first frame update
    void Start(){

       animator = GetComponent<Animator>();
       rb2d = GetComponent<Rigidbody2D>();
       spriteRenderer = GetComponent<SpriteRenderer>();
       isFliped = false;
       isJumping = false;
       barrel = GetComponentInChildren<Barrel>();
    }

    private void Update() {
        //barrel.enabled = !animator.GetCurrentAnimatorStateInfo(0).IsName("Dive");
        //barrel.GetComponent<Animator>().enabled = !animator.GetCurrentAnimatorStateInfo(0).IsName("Dive");
    }

    // Update is called once per frame
    void FixedUpdate()
   {   
        MovePlayer();
   }
     
     public void MovePlayer()
     {
        if(Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
		}
        else 
        {
            isGrounded = false;
		}
         
        
        
        if ((Input.GetKey("d")) && (isFliped == false) && isGrounded) //Move player Right Facing Right
        {
                if (Input.GetKey("space"))
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 10);
                    animator.Play("Falling");
                    rb2d.velocity = new Vector2(5, rb2d.velocity.y); 
				}

                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    animator.Play("Dive");
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 3);
                    rb2d.velocity = new Vector2(10, rb2d.velocity.y); 
                }
                
                else
                {
                MovePlayerRightFacingRight();
                }
        }
        else if((Input.GetKey("a")) && (isFliped == true) && isGrounded) //Player Moves Left Facing Right
        {    
                if (Input.GetKey("space"))
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 10);
                    animator.Play("Falling");
                    rb2d.velocity = new Vector2(-5, rb2d.velocity.y); 
				}

                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    animator.Play("Dive");
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 3);
                    rb2d.velocity = new Vector2(-10, rb2d.velocity.y); 
                }

                else
                    MovePlayerLeftFacingLeft();
        }
        else if((Input.GetKey("a")) && (isFliped == false) && isGrounded) //Player Moves Left Facing Backwards
        {
                  if (Input.GetKey("space"))
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 10);
                    animator.Play("Falling");
                    rb2d.velocity = new Vector2(-5, rb2d.velocity.y); 
				}

                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    animator.Play("DiveBackwards");
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 3);
                    rb2d.velocity = new Vector2(-10, rb2d.velocity.y); 
                }

                else

                MovePlayerLeftFacingRight();
        }
        else if((Input.GetKey("d")) && (isFliped == true)&& isGrounded) //Move player Right Facing Backwards
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.Play("DiveBackwards");
                rb2d.velocity = new Vector2(rb2d.velocity.x, 3);
                rb2d.velocity = new Vector2(10, rb2d.velocity.y); 
            }
            else
                MoveplayerRightFacingLeft();
        }
        else if (Input.GetKey("s") && isGrounded)
        {
            Crouch();
        }

        else if (Input.GetKey("space") && isGrounded)
        {
          StartCoroutine (Jumping()); 
        }

        else if (Input.GetKey("d"))
        {
          rb2d.velocity = new Vector2(5, rb2d.velocity.y);  
          //animator.Play("Falling");
        }

        else if (Input.GetKey("a"))
        {
          rb2d.velocity = new Vector2(-5, rb2d.velocity.y);  
          //animator.Play("Falling");
        }

        else
        {        
            Idel();
		}

        
	 }
     
     public void PlayerFlip()
     {
     spriteRenderer.flipX = true;
     isFliped = true;
     }
     
     public void UnPlayerFlip()
     {
     spriteRenderer.flipX = false;
     isFliped = false;
     }
     
     public void MovePlayerRightFacingRight()
     {
     if(isGrounded)
            spriteRenderer.flipX = false;
            if (Input.GetKey("s")&& isGrounded)
            {
            animator.Play("PlayerWalkingCrouch");
            rb2d.velocity = new Vector2(2, rb2d.velocity.y); 
            }
            else 
            {
            rb2d.velocity = new Vector2(5, rb2d.velocity.y); 
            animator.Play("PlayerRun");
            }
	 }

     public void MovePlayerLeftFacingRight()
     {
            if(isGrounded)
            spriteRenderer.flipX = true;
            
            if (Input.GetKey("s")&& isGrounded){

            animator.Play("PlayerWalkingCrouch");
            rb2d.velocity = new Vector2(-2, rb2d.velocity.y); 
            }
            else 
            {
            rb2d.velocity = new Vector2(-5, rb2d.velocity.y);  
             animator.Play("PlayerRunBackwards");
            }
	 }

     public void MovePlayerLeftFacingLeft()
     {
           if(isGrounded)
            spriteRenderer.flipX = false;
            if (Input.GetKey("s")&& isGrounded)
            {
            animator.Play("PlayerWalkingCrouch");
            rb2d.velocity = new Vector2(-2, rb2d.velocity.y); 
            }
            else 
            {
            rb2d.velocity = new Vector2(-5, rb2d.velocity.y);  
             animator.Play("PlayerRun");
            }
     }

    public void MoveplayerRightFacingLeft()
    {
            if(isGrounded)
            spriteRenderer.flipX = false;
            
            if (Input.GetKey("s")&& isGrounded)
            {
            animator.Play("PlayerWalkingCrouch");
            rb2d.velocity = new Vector2(2, rb2d.velocity.y); 
            }
            else 
            {
            rb2d.velocity = new Vector2(5, rb2d.velocity.y);  
             animator.Play("PlayerRunBackwards");
            }     
	}

    public void Crouch()
    {
          if(isGrounded)
          isCrouched = true;
          animator.Play("PlayerCrouched");
	}

    
     IEnumerator Jumping()
    {
      animator.Play("Player_Jump");
      rb2d.velocity = new Vector2(rb2d.velocity.x, 10);
      yield return new WaitForSeconds(0.3f);
      animator.Play("Falling");
      
      
      //animator.Play("Falling");
      
	}
    
    public void Idel()
    {
        if(isGrounded)
        {
            animator.Play("Player_Idel");
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);

        }
        else if (isJumping == false)
        {
            animator.Play("Falling");
		}
       
	}
   }