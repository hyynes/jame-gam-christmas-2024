using UnityEngine;

public class MovementManager : MonoBehaviour
{
    // singleton declaration
    public static MovementManager instance { get; private set; }
    
    // horizontal movement
    [Header("Horizontal Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float sprintSpeed;
    
    // jumping
    [Header("Jumping")]
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float fallingGravityAcceleration;
    [SerializeField] private float maxFallingAcceleration;
    [SerializeField] private float velocityThresholdBeforeFalling;

    [Header("Key Codes")] 
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    
    // accessors
    private Rigidbody2D body;
    private float speedBeforeSprint;
    private float gravityScaleBeforeAcceleration;

    // bools
    private bool isGrounded;
    private bool isFacingRight = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // if i don't exist, create me
        if (instance == null)
        {
            instance = this;
        }
        
        // get values from values set in the inspector
        body = GetComponent<Rigidbody2D>();
        speedBeforeSprint = speed;
        gravityScaleBeforeAcceleration = body.gravityScale;
    }

    // fixed update is called so the falling velocity is not so extreme
    private void FixedUpdate()
    {
        // falling
        if (body.linearVelocity.y <= velocityThresholdBeforeFalling && !isGrounded)
        {
            body.gravityScale += fallingGravityAcceleration;
            body.gravityScale = Mathf.Clamp(body.gravityScale, 0, maxFallingAcceleration);
        }
        else
        {
            body.gravityScale = gravityScaleBeforeAcceleration;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // sprint check
        if (Input.GetKey(sprintKey))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = speedBeforeSprint;
        }
        
        // horizontal movement; multiply the horizontal direction with the speed set in the inspector
        float horizontalDirection = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalDirection * speed, body.linearVelocity.y);
        
        // jump action; if the player is grounded, then the player can jump - otherwise, they cannot
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            body.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse); 
        }
        
        // sprite flips (if horizontal direction changes)
        if (horizontalDirection < 0 && isFacingRight || horizontalDirection > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    // update whether the player is on the ground or not; if they are, then reset the jump 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    
    private void Flip()
    {
        // flip the direction first
        isFacingRight = !isFacingRight;
        
        // check direction character is facing, then rotate accordingly
        transform.eulerAngles = new Vector3(0, isFacingRight ? 0 : 180, 0);
    }
}
