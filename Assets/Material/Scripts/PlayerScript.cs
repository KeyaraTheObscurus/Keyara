using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //creating variables
    //[SerializeField] is private variables those we can see and edit in Unity

    //movement and physic variables
    private Rigidbody2D rb;
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpForce = 6f;
    [SerializeField] float HorizontalMove;

    //this variable responsible for direction
    [SerializeField] bool facingRight = true;

    //ground checking variables
    [SerializeField] bool isGrounded = false;
    [SerializeField] Transform groundChecker;
    [SerializeField] float checkerRadius = 0.5f;
    [SerializeField] LayerMask groundLayer;

    //gravitation skill variables
    [SerializeField] float timeBeforeSkill;
    [SerializeField] float skillCD = 5f;

    void Start()
    {
        //getting Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        timeBeforeSkill = skillCD;
    }

    void Update()
    {
        //checking ground
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, checkerRadius, groundLayer);

        //getting direction with speed
        HorizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        //turning player
        if(HorizontalMove < 0 && facingRight)
        {
            FlipX();
        } else if(HorizontalMove > 0 && !facingRight)
        {
            FlipX();
        }
        
        //jump
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
             rb.AddForce(transform.up * jumpForce * rb.gravityScale, ForceMode2D.Impulse);
        }
        if (timeBeforeSkill >= skillCD)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                rb.gravityScale *= -1;
                timeBeforeSkill = 0;
                FlipY();
            }
        }
        else 
        {
            timeBeforeSkill += Time.deltaTime;
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


    //if touching light
    private void OnTriggerEnter2D(Collider2D collsion)
    {
        if(collsion.tag == "Light")
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
