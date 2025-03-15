using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private InputSystem_Actions inputActions;
    private float speed = 10f;
    private float jump_force = 15f;
    public Rigidbody2D rb;
    private bool isGrounded;
    private bool canClimb = false;
    public SpriteRenderer spriteRenderer;
    private float originalGravity;
    void Start()
    {
        originalGravity = rb.gravityScale;
    }

    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.Enable();
        inputActions.Player.Jump.Enable();
        inputActions.Player.Jump.performed += Onjump;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.Disable();
        inputActions.Player.Jump.Disable();
        inputActions.Player.Jump.performed -= Onjump;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move_input = inputActions.Player.Move.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(move_input.x * speed, rb.linearVelocityY);
        if (move_input.x > 0)
            spriteRenderer.flipX = false;
        else if (move_input.x < 0)
            spriteRenderer.flipX = true;

        if (canClimb)
        {
            rb.linearVelocity = new Vector2(move_input.x * speed, move_input.y * speed);
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = originalGravity;
        }
    }

    private void Onjump(InputAction.CallbackContext context)
    {
        if (isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jump_force);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Climb")
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Climb")
        {
            canClimb = false;
        }
    }
}
