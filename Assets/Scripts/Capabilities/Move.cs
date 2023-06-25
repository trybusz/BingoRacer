using UnityEngine;

public class Move : MonoBehaviour
{
    // objects
    [SerializeField] private InputController input = null;
    Rigidbody2D body;
    Ground ground;
    
    // physics characteristics
    float maxSpeed = 9f;
    float maxGroundAcceleration = 50f;
    float maxAirAcceleration = 40f;

    // state
    public float direction;
    public Vector2 velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
    }

    void Update()
    {
        direction = input.RetrieveMoveInput();
    }

    void FixedUpdate()
    {
        float desiredHorizontalSpeed = direction * Mathf.Max(maxSpeed - ground.Friction, 0f);
        float horizontalAcceleration = ground.OnGround ? maxGroundAcceleration : maxAirAcceleration;
        float maxSpeedChange = horizontalAcceleration * Time.deltaTime;
        body.velocity = new Vector2(Mathf.MoveTowards(body.velocity.x, desiredHorizontalSpeed, maxSpeedChange), body.velocity.y);
    }
}
