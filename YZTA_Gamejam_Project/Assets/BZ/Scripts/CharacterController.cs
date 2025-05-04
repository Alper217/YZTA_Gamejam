using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;
    private bool jump = false;
    private float jumpForce = 5f;
    private int moveDirection;
    private float horizontalInput;

    [Header("Ladder Climbing")]
    public float climbSpeed = 4f;
    private float verticalInput;
    public bool isClimbing = false;
    private float jumpAfterClimbing = 1.2f;

    [Header("Ground Check")]
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool ground = true;

    [Header("Components")]
    public Rigidbody2D rb;
    private SpriteRenderer _spriteRenderer;
    public Animator animator;

    void Awake(){
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (ground == true){
            moveDirection = 0;
            animator.SetFloat("speed", 0.0f);
        }


        if (horizontalInput > 0.01f){
            _spriteRenderer.flipX = false;
            animator.SetFloat("speed", speed);
        }
        else if (horizontalInput < -0.01f){
            _spriteRenderer.flipX = true;
            animator.SetFloat("speed", speed);
        }

        // Jump
        if (Input.GetButtonDown("Jump") && ground && !isClimbing)
        {            
            jump = true;
            ground = false;
        //    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("jump");
            animator.SetBool("ground", false);
        }
        else if (Input.GetButtonDown("Jump") && !ground){
            ground = false;
            Debug.Log("zıplama artık");
        }

        // Ladder Climbing
        if (isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * climbSpeed);
            rb.gravityScale = 0f;
        }
        else
        {
            rb.gravityScale = 1f;
        }

        // Animation
        if (animator != null)
        {
            animator.SetFloat("speed", Mathf.Abs(horizontalInput));
            animator.SetBool("ground", ground);
            animator.SetFloat("ClimbSpeed", isClimbing ? Mathf.Abs(verticalInput) : 0f);
        }
    }

    private void FixedUpdate()
    {
        // Horizontal movement (done in FixedUpdate for physics consistency)
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        if (jump == true){
        //    rb.velocity = new Vector3(rb.velocity.x, 0, 0);
           // _rigidbody2D.AddForce(transform.up * jumpForce);
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            jump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Floor")){
            ground = true;
            Debug.Log("Floor");
            animator.SetBool("ground", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = true;
            animator.SetBool("isClimbing", true);
        }
        else
            return;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = false;
            animator.SetBool("isClimbing", false);
            rb.gravityScale = 1f;

            // Optional: nudge player a bit away from ladder so they can move
            Vector3 pos = transform.position;
        //    pos.x += horizontalInput > 0 ? 0.2f : -0.2f;
            pos.y += verticalInput > 0 ? jumpAfterClimbing : 0;
            transform.position = pos;
        }
    }
}
