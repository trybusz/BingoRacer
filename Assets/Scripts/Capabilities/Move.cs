using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{

    // objects
    //[SerializeField] private InputController input = null;
    Rigidbody2D body;
    Ground ground;
    
    // physics characteristics
    float maxSpeed = 10f;
    float maxGroundAcceleration = 90f;
    float maxAirAcceleration = 75f;
    float dashDuration = .15f;
    float dashDistance = 3f;
    bool dashEnabled = true;
    float dashBufferTime = .1f;
    float dashCooldown = .44f;

    // state
    public float inputDirection = 0f;
    public float facing = 1f;
    public float dashDirection = 1f;
    public bool groundedSinceDash = true;
    public bool hasDash = true;
    public bool dashDesired = false;
    public float endDashBuffer = -1f;
    public float endDashTime = -1f;
    public float dashDeceleration;
    public bool inDash = false;
    public float refreshDashTime = -1;

    //new input stuff
    private PlayerActionControls playerActionControls;

    private void Awake() {
        playerActionControls = new PlayerActionControls();
    }

    private void OnEnable() {
        playerActionControls.Enable();
    }

    private void OnDisable() {
        playerActionControls.Disable();
    }
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
        if (!HandleDash()) {
            HandleMovement();
        }
    }

    void GetInput()
    {
        //inputDirection = input.RetrieveMoveInput(); //Old version
        inputDirection = playerActionControls.Game.Move.ReadValue<float>();
        if (inputDirection != 0 && !inDash) {
            facing = inputDirection;
        }
        if (dashEnabled) {
            //if (input.RetrieveDashInput()) { //Old Version
            if (playerActionControls.Game.Dash.ReadValue<float>() == 1) {
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
        if (dashDesired && hasDash && body.velocity.y <= .3) {
            dashDesired = false;
            hasDash = false;
            inDash = true;
            dashDirection = facing;
            groundedSinceDash = false;
            endDashTime = Time.timeSinceLevelLoad + dashDuration;
            refreshDashTime = Time.timeSinceLevelLoad + dashCooldown;
            float dashVelocity = Mathf.Max((dashDistance / dashDuration) * 2 - maxSpeed, maxSpeed);
            dashDeceleration = (dashVelocity - maxSpeed) / dashDuration * Time.deltaTime;
            body.velocity = new Vector2(facing * dashVelocity, body.velocity.y);
            return true;
        }
        else if (inDash) {
            body.velocity = new Vector2(Mathf.MoveTowards(body.velocity.x, maxSpeed * facing, dashDeceleration), body.velocity.y);
            if (Time.timeSinceLevelLoad >= endDashTime) {
                inDash = false;
                groundedSinceDash = ground.OnGround;
            }
            return true;
        }
        groundedSinceDash |= ground.OnGround;
        hasDash |= groundedSinceDash && Time.timeSinceLevelLoad >= refreshDashTime;
        return false;
    }

    void HandleMovement() {
        float desiredHorizontalSpeed = inputDirection * Mathf.Max(maxSpeed - ground.Friction, 0f);
        float horizontalAcceleration = ground.OnGround ? maxGroundAcceleration : maxAirAcceleration;
        float maxSpeedChange = horizontalAcceleration * Time.deltaTime;
        body.velocity = new Vector2(Mathf.MoveTowards(body.velocity.x, desiredHorizontalSpeed, maxSpeedChange), body.velocity.y);
    }
}
