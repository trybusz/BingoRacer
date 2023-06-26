using UnityEngine;

public class Jump : MonoBehaviour
{
    // objects

    [SerializeField] private InputController input = null;
    Rigidbody2D body;
    Ground ground;
    
    // physics characteristics
    public int jumpsAllowed = 2;
    public float jumpHeight = 3.33f;
    public float risingGravity = 3f;
    public float stopRisingGravity = 8f;
    public float fallingGravity = 4f;
    public float restingGravity = 1f;
    public float dashingGravity = 0f;
    public float jumpBufferTime = .2f;
    public float coyoteTime = .08f;

    // state
    public float endJumpBuffer = -1f;
    public float endGroundTime = -1f;
    public bool jumpPressed = false;
    public bool jumpDesired = false;
    public int jumpsUsed = 0;
    public bool justJumped = false;
    public bool isDashing = false;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        HandleJump();
        HandleGravity();
    }

    void GetInput() 
    {
        bool jumpDown = input.RetrieveJumpInputDown();
        jumpPressed = input.RetrieveJumpInput();
        if (jumpDown) {
            jumpDesired = true;
            endJumpBuffer = Time.timeSinceLevelLoad + jumpBufferTime;
        }
        else if (Time.timeSinceLevelLoad > endJumpBuffer) {
            jumpDesired = false;
        }
    }

    void HandleJump() 
    {
        // had to check if player just jumped because in the next frame,
        // the player would usually still be on the ground and would get
        // jump count reset to 0. This solution is probably not robust.
        if (ground.OnGround && !justJumped) {
            endGroundTime = Time.timeSinceLevelLoad + coyoteTime;
            jumpsUsed = 0;
        }
        else {
            justJumped = false;
        }

        if (jumpDesired) {
            if (ground.OnGround || Time.timeSinceLevelLoad <= endGroundTime) {
                if (jumpsUsed < jumpsAllowed) {
                    performJump();
                }
            }
            else {
                if (jumpsUsed == 0) {
                    jumpsUsed = 1;
                }
                if (jumpsUsed < jumpsAllowed) {
                    performJump();
                }
            }
        }
    }

    void HandleGravity() {
        Vector2 velocity = body.velocity;
        if (ground.OnGround) {
            body.gravityScale = restingGravity;
        }
        else if (isDashing) {
            body.velocity = new Vector2(body.velocity.x, 0.0f);
            body.gravityScale = dashingGravity;
        }
        else if (body.velocity.y >= 0f) {
            if (jumpPressed) {
                body.gravityScale = risingGravity;
            }
            else {
                body.gravityScale = stopRisingGravity;
            }
        }
        else {
            body.gravityScale = fallingGravity;
        }
    }

    void performJump() 
    {
        justJumped = true;
        jumpsUsed += 1;
        jumpDesired = false;
        float jumpSpeed = Mathf.Sqrt(19.6f * risingGravity * jumpHeight);
        if (jumpSpeed > body.velocity.y) {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }
    }
}