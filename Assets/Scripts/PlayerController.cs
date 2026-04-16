using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float gravityMultiplier = 1f;
    // 1.15 handle game over state
    public bool gameOver = false;
    private Rigidbody rb;
    private InputAction jumpAction;

    // 1.7 Add a boolean to check if the player is on the ground
    private bool isOnGround = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 1.6 Apply gravity multiplier
        Physics.gravity *= gravityMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        // 1.16 if game over, stop the player from moving or jumping
        if (gameOver)
        {
            return;
        }

        // 1.7 add isOnGround check before allowing the player to jump
        if (jumpAction.triggered && isOnGround)
        {
            // 1.5 make it jump
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            // 1.7 set isOnGround to false when the player jumps
            isOnGround = false;
        }
    }

    // 1.7 Set isOnGround to true when the player collides with the ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        // 1.15 handle game over state when colliding with an obstacle
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            gameOver = true;
        }
    }
}
