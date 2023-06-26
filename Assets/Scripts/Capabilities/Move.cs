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
    float maxGroundAcceleration = 50f;
    float maxAirAcceleration = 40f;
    float dashDuration = .2f;
    float dashDistance = 4f;
    bool dashEnabled = true;
    bool dashResetsInAir = false;
    float dashCooldown = .5f;
    float dashBufferTime = .2f;

    // state
    public float direction = 0f;
    public float facing = 1f;
    public float endDashTime = -1f;
    public bool dashDesired = false;
    public float endDashBuffer = -1f;
    public float refreshDashTime = -1f;
    public bool hasDash = true;
    public float dashDeceleration;
    public bool inDash = false;

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
        if (!HandleDash()) {
            HandleMovement();
        }
    }

    void GetInput()
    {
        direction = input.RetrieveMoveInput();
        if (direction != 0 && !inDash) {
            facing = direction;
        }
        if (dashEnabled) {
            if (input.RetrieveDashInputDown()) {
                dashDesired = true;
                endDashBuffer = Time.timeSinceLevelLoad + dashBufferTime;
            }
            else if (Time.timeSinceLevelLoad >= endDashBuffer) {
                dashDesired = false;
            }
        }
    }

    bool HandleDash() {
        hasDash |= Time.timeSinceLevelLoad > refreshDashTime && (ground.OnGround || dashResetsInAir);
        if (dashDesired && hasDash) {
            dashDesired = false;
            hasDash = false;
            inDash = true;
            jumpScript.isDashing = true;
            refreshDashTime = Time.timeSinceLevelLoad + dashCooldown;
            endDashTime = Time.timeSinceLevelLoad + dashDuration;
            float dashVelocity = Mathf.Max((dashDistance / dashDuration) * 2 - maxSpeed, maxSpeed);
            dashDeceleration = (dashVelocity - maxSpeed) / dashDuration * Time.deltaTime;
            body.velocity = new Vector2(facing * dashVelocity, body.velocity.y);
            return true;
        }
        else if (inDash) {
            body.velocity = new Vector2(Mathf.MoveTowards(body.velocity.x, maxSpeed * facing, dashDeceleration), body.velocity.y);
            if (Time.timeSinceLevelLoad >= endDashTime) {
                inDash = false;
                jumpScript.isDashing = false;
            }
            return true;
        }
        return false;
    }

    void HandleMovement() {
        float desiredHorizontalSpeed = direction * Mathf.Max(maxSpeed - ground.Friction, 0f);
        float horizontalAcceleration = ground.OnGround ? maxGroundAcceleration : maxAirAcceleration;
        float maxSpeedChange = horizontalAcceleration * Time.deltaTime;
        body.velocity = new Vector2(Mathf.MoveTowards(body.velocity.x, desiredHorizontalSpeed, maxSpeedChange), body.velocity.y);
    }
}
