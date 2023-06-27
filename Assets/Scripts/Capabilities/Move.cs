using UnityEngine;

public class Move : MonoBehaviour
{
    Jump jumpScript;

    // objects
    [SerializeField] private InputController input = null;
    Rigidbody2D body;
    Ground ground;
    
    // physics characteristics
    float maxSpeed = 10f;
    float maxGroundAcceleration = 90f;
    float maxAirAcceleration = 75f;
    float dashDuration = .15f;
    float dashDistance = 3f;
    bool dashEnabled = true;
    float dashBufferTime = .2f;

    // state
    public float inputDirection = 0f;
    public float facing = 1f;
    public float dashDirection = 1f;
    public bool hasDash = true;
    public bool dashPressed = false;
    public bool dashDesired = false;
    public float endDashBuffer = -1f;
    public float endDashTime = -1f;
    public float dashDeceleration;
    public bool inDash = false;
    public bool isSprinting = false;
    public float sprintVelocity = 0;
    public float speed = 0;
    public float fastedSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        jumpScript = GetComponent<Jump>();
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        speed = Mathf.Abs(body.velocity.x);
        if (speed > fastedSpeed) {
            fastedSpeed = speed;
        }
        if (!HandleDash()) {
            HandleMovement();
        }
    }

    void GetInput()
    {
        inputDirection = input.RetrieveMoveInput();
        if (inputDirection != 0 && !inDash) {
            facing = inputDirection;
        }
        if (dashEnabled) {
            dashPressed = input.RetrieveDashInput();
            if (input.RetrieveDashInputDown()) {
                dashDesired = true;
                endDashBuffer = Time.timeSinceLevelLoad + dashBufferTime;
            }
            else if (Time.timeSinceLevelLoad >= endDashBuffer) {
                dashDesired = false;
            }
        }
    }

    // This function needs to be broken down into smaller pieces. It is confusing
    bool HandleDash() {
        if (dashDesired && hasDash) {
            dashDesired = false;
            hasDash = false;
            inDash = true;
            jumpScript.isDashing = true;
            isSprinting = ground.OnGround;
            dashDirection = facing;
            endDashTime = Time.timeSinceLevelLoad + dashDuration;
            float dashVelocity = Mathf.Max((dashDistance / dashDuration) * 2 - maxSpeed, maxSpeed);
            sprintVelocity = maxSpeed + (dashVelocity - maxSpeed) * .55f;
            dashDeceleration = (dashVelocity - maxSpeed) / dashDuration * Time.deltaTime;
            body.velocity = new Vector2(facing * dashVelocity, body.velocity.y);
            return true;
        }
        else if (inDash) {
            isSprinting = isSprinting && dashPressed && ground.OnGround && dashDirection == facing;
            if (isSprinting) {
                body.velocity = new Vector2(Mathf.MoveTowards(body.velocity.x, sprintVelocity * dashDirection, dashDeceleration), body.velocity.y);
            }
            else {
                body.velocity = new Vector2(Mathf.MoveTowards(body.velocity.x, maxSpeed * facing, dashDeceleration), body.velocity.y);
            }
            if (Time.timeSinceLevelLoad >= endDashTime) {
                inDash = false;
                jumpScript.isDashing = false;
                hasDash |= !inDash && ground.OnGround;
            }
            return true;
        }
        else if (isSprinting) {
            isSprinting = isSprinting && dashPressed && ground.OnGround && dashDirection == facing;
            body.velocity = new Vector2(Mathf.MoveTowards(body.velocity.x, sprintVelocity * dashDirection, dashDeceleration), body.velocity.y);
            return true;
        }
        hasDash |= ground.OnGround;
        return false;
    }

    void HandleMovement() {
        float desiredHorizontalSpeed = inputDirection * Mathf.Max(maxSpeed - ground.Friction, 0f);
        float horizontalAcceleration = ground.OnGround ? maxGroundAcceleration : maxAirAcceleration;
        float maxSpeedChange = horizontalAcceleration * Time.deltaTime;
        body.velocity = new Vector2(Mathf.MoveTowards(body.velocity.x, desiredHorizontalSpeed, maxSpeedChange), body.velocity.y);
    }
}
