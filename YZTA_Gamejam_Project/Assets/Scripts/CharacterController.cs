using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private float jumpForce = 5f;
    private float horizontalInput;

    [Header("Ladder Climbing")]
    public float climbSpeed = 4f;
    private float verticalInput;
    private bool isClimbing = false;
    private float jumpAfterClimbing = 1.2f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        Debug.Log("isGrounded:" + isGrounded);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded && !isClimbing)
        {
            Debug.Log("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
            animator.SetBool("IsGrounded", isGrounded);
            animator.SetFloat("ClimbSpeed", isClimbing ? Mathf.Abs(verticalInput) : 0f);
        }
    }

    private void FixedUpdate()
    {
        // Horizontal movement (done in FixedUpdate for physics consistency)
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = false;
            rb.gravityScale = 1f;

            // Optional: nudge player a bit away from ladder so they can move
            Vector3 pos = transform.position;
        //    pos.x += horizontalInput > 0 ? 0.2f : -0.2f;
            pos.y += verticalInput > 0 ? jumpAfterClimbing : 0;
            transform.position = pos;
        }
    }
}
