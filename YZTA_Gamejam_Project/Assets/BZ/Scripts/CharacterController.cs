using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float speed = 3f;
    private bool jump = false;
    [SerializeField] private float jumpForce = 5f;
    private int moveDirection;
    private float horizontalInput;

    [Header("Ladder Climbing")]
    [SerializeField] public float climbSpeed = 4f;
    private float verticalInput;
    public bool isClimbing = false;
    [SerializeField] public float jumpAfterClimbing = 0.2f;

    [Header("Ground Check")]
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool ground = true;

    [Header("Components")]
    public Rigidbody2D rb;
    private SpriteRenderer _spriteRenderer;
    public Animator animator;
    private AudioSource _audio;

    private float footstepTimer;
    [SerializeField] float footstepDelay = 0.2f;

    private float jumpTimer;
    [SerializeField] float jumpDelay = 0.2f;


    void Awake(){
        animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
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

            // Jump sound
            PlayJumpSound();
        }
        else if (Input.GetButtonDown("Jump") && !ground){
            ground = false;
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

        if (ground && Mathf.Abs(horizontalInput) > 0.1f && !isClimbing)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                PlayFootstepSound();
                footstepTimer = footstepDelay;
            }
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

    void PlayFootstepSound()
    {
        int randomIndex = Random.Range(1, 3); // Walk1, Walk2, Walk3
        SoundType walkSound = (SoundType)System.Enum.Parse(typeof(SoundType), "Walk" + randomIndex);
        SFXManager.PlaySound(walkSound);
    }

    void PlayJumpSound()
    {   
        SFXManager.PlaySound(SoundType.Jump1);
    }
}
