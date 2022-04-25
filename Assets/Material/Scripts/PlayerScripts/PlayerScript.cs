using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //creating variables
    //[SerializeField] is private variables those we can see and edit in Unity

    //movement and physic variables
    private Rigidbody2D rb;
    [SerializeField] float Speed;
    [SerializeField] float negGravity;
    [SerializeField] float JumpForce;
    [SerializeField] float HorizontalMove;
//    [SerializeField] float JumpGrav;
    [SerializeField] AudioSource audioSource01;

    //this variable responsible for direction
    [SerializeField] bool facingRight = true;

    //ground checking variables
    [SerializeField] bool isGrounded = true;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundChecker;
    [SerializeField] float checkerRadius = 0.5f;

    //Number of jumps to start with. -Xeno
    [SerializeField] float HasJumps;

    //gravitation skill variables
    [SerializeField] float timeBeforeSkill;
    [SerializeField] float skillCD = 5f;

    //it`s animator
    private Animator animator;

    [SerializeField] bool InCave = false;




    void Start()
    {

        audioSource01 = GetComponent<AudioSource>();

        //getting Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        //getting animator
        animator = GetComponent<Animator>();

        timeBeforeSkill = skillCD;

        // transform.position = new Vector3(PlayerPrefs.GetFloat("position.x"), PlayerPrefs.GetFloat("position.y"), PlayerPrefs.GetFloat("position.z"));
        // transform.position = new Vector3(0.42f, -0.98f, 1.13f);
    }

    void LateUpdate()
    {
        //Scene Reload with "R"
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }

        //very SIMPLE(like 3d AI) animation something that we need now(no)
        if (HorizontalMove > 0)
        {
            animator.SetFloat("Speed", HorizontalMove);
        }
        else
        {
            animator.SetFloat("Speed", HorizontalMove * -1);
        }

        //checking ground
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, checkerRadius, groundLayer);

        //getting direction with speed
        HorizontalMove = Input.GetAxisRaw("Horizontal") * Speed;

        //turning player
        if (HorizontalMove < 0 && facingRight)
        {
            FlipX();
        }
        else if (HorizontalMove > 0 && !facingRight)
        {
            FlipX();
        }

        if (HasJumps <= 0)
        {
            if (isGrounded)
            {
                HasJumps = 1f;
            }
        }


        if ((Input.GetKeyDown(KeyCode.W) && !(Input.GetKeyDown(KeyCode.UpArrow) && !(Input.GetKeyDown(KeyCode.Space)))))
        {
            if (HasJumps >= 1f)
            {
                Jump();
            }
        }
        else
        if ((Input.GetKeyDown(KeyCode.UpArrow) && !(Input.GetKeyDown(KeyCode.W) && !(Input.GetKeyDown(KeyCode.Space)))))
        {
            if (HasJumps >= 1f)
            {
                Jump();
            }
        }
        else
        if ((Input.GetKeyDown(KeyCode.Space) && !(Input.GetKeyDown(KeyCode.W) && !(Input.GetKeyDown(KeyCode.UpArrow)))))
        {
            if (HasJumps >= 1f)
            {
                Jump();
            }
        }

        // if (transform.position.y < 0 && !InCave)
        // {
        //     FlipY();
        //     rb.gravityScale *= -1;
        // }

        // if (timeBeforeSkill >= skillCD)
        // {
        //     if (Input.GetKeyDown(KeyCode.Q))
        //     {
        //         rb.gravityScale *= negGravity;
        //         timeBeforeSkill = 0;
        //         FlipY();
        //     }
        // }
        // else
        // {
        //     FlipY();
        //     timeBeforeSkill += Time.deltaTime;
        // }
    }

    private void Jump()
    {
            // if (!InCave)
            {
                HasJumps -= 1f;
                rb.AddForce(transform.up * (JumpForce + rb.gravityScale), ForceMode2D.Impulse);
                isGrounded = false;
            }
            // else
            // {
            //     rb.gravityScale *= -1;
            //     FlipY();
            // }
    }

    private void FixedUpdate()
    {
        if ((Input.GetAxis("Horizontal") != 0) && isGrounded && !audioSource01.isPlaying) audioSource01.Play();
        //moveing character
        Vector2 targetVelocity = new Vector2(HorizontalMove * 5, rb.velocity.y);
        rb.velocity = targetVelocity;
    }

    //I think you can understand what that 2 methods doing
    private void FlipX()
    {
        facingRight = !facingRight;

        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void FlipY()
    {
        Vector2 theScale = transform.localScale;
        theScale.y *= -1;
        transform.localScale = theScale;
    }

    /* 
       this function will draw groundChecker circle 
       it's not it's not necessary,
       but it will be more convenient.
    */

    private void OnDrawGizmosSelected()
    {
        //set color for circle
        Gizmos.color = Color.yellow;

        //drawing circle
        Gizmos.DrawWireSphere(groundChecker.position, checkerRadius);
    }


    //if touching light (Added generic trigger name for other reset triggers -Xeno)
    private void OnTriggerEnter2D(Collider2D collsion)
    {
        if (collsion.tag == "ResetTrig")
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
        if (collsion.tag == "Cave")
        {
            InCave = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Cave")
        {
            InCave = false;
        }
    }

    void Update()
    {
        if ((Input.GetAxis("Horizontal") == 0f))
            {
                audioSource01.Stop();
            }
            if (!isGrounded) audioSource01.Stop();

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}