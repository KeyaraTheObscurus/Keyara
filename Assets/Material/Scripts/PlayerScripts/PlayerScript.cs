using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //creating variables
    //[SerializeField] is private variables those we can see and edit in Unity

    //movement and physic variables
    private Rigidbody2D rb;
    [SerializeField] float Speed;
    [SerializeField] float JumpForce;
    [SerializeField] float HorizontalMove;
    [SerializeField] float JumpGrav;

    //this variable responsible for direction
    [SerializeField] bool facingRight = true;

    //ground checking variables  (Only Commenting this for now because we might use it later, but wanted the ability to "Mid-Air Jump")
    // [SerializeField] bool isGrounded = false;
    // [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundChecker;
    [SerializeField] float checkerRadius = 0.5f;

    //Number of jumps to start with. -Xeno
    [SerializeField] float HasJumps;

    //gravitation skill variables
    [SerializeField] float timeBeforeSkill;
    [SerializeField] float skillCD = 5f;

    //it`s animator
    private Animator animator;

    [SerializeField] bool isGrounded = false;
    [SerializeField] LayerMask groundLayer;



    void Start()
    {
        //getting Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        //getting animator
        animator = GetComponent<Animator>();

        timeBeforeSkill = skillCD;
    }

    void Update()
    {
        //Scene Reload with "R"
        if(Input.GetKeyDown("r")){
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }

        //very SIMPLE(like 3d AI) animation something that we need now(no)
        if (HorizontalMove > 0)
        {
            animator.SetFloat("Speed", HorizontalMove);
        } else
        {
            animator.SetFloat("Speed", HorizontalMove * -1);
        }

        //checking ground
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, checkerRadius, groundLayer);

        //Max Jumps -Xeno
        

        //getting direction with speed
        HorizontalMove = Input.GetAxisRaw("Horizontal") * Speed;

        //turning player
        if(HorizontalMove < 0 && facingRight)
        {
            FlipX();
        } else if(HorizontalMove > 0 && !facingRight)
        {
            FlipX();
        }

        if(HasJumps <= 0)
        {
            if(isGrounded)
            {
                HasJumps = 1f;
            }
        }


        if(Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        else
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        else
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        
        if (timeBeforeSkill >= skillCD)
        {
            if (Input.GetButton("q"))
            {
                rb.gravityScale *= -rb.gravityScale;
                timeBeforeSkill = 0;
                FlipY();
            }
        }
        else
        {
            rb.gravityScale *= -1;
            FlipY();
            timeBeforeSkill += Time.deltaTime;
        }
    }

    private void Jump()
    {  
        if (HasJumps >= 1f)
        {
            HasJumps -= 1f;
            rb.AddForce(transform.up * JumpForce * JumpGrav, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }    

    private void FixedUpdate()
    {
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
        if(collsion.tag == "ResetTrig")
        {
           Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
    }
}
