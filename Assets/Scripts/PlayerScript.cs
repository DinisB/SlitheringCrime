using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    private float speed = 10f;
    private float jump_force = 15f;
    public Rigidbody2D rb;
    private bool isGrounded;
    private bool canClimb = false;
    private bool UsedTarantula = false;
    private bool CanUseWolfAbility = true;
    public SpriteRenderer spriteRenderer;
    private float originalGravity;
    void Start()
    {
        //guarda a gravidade original
        originalGravity = rb.gravityScale;
    }

    void Awake()
    {
        //lê os inputs
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        //verifica se o jogador clicou nas setas/joystick ou espaço/cruz
        inputActions.Player.Enable();
        inputActions.Player.Move.Enable();
        inputActions.Player.Jump.Enable();
        inputActions.Player.Jump.performed += Onjump;
        inputActions.Player.Wolf.Enable();
        inputActions.Player.Wolf.performed += WolfAbility;
        inputActions.Player.Tarantula.Enable();
        inputActions.Player.Tarantula.performed += TarantulaAbility;
    }

    private void OnDisable()
    {
        //idem 
        inputActions.Player.Move.Disable();
        inputActions.Player.Jump.Disable();
        inputActions.Player.Jump.performed -= Onjump;
        inputActions.Player.Wolf.Disable();
        inputActions.Player.Wolf.performed -= WolfAbility;
        inputActions.Player.Tarantula.Disable();
        inputActions.Player.Tarantula.performed -= TarantulaAbility;
    }

    // Update is called once per frame
    void Update()
    {
        // lê valor das setas/joystick
        Vector2 move_input = inputActions.Player.Move.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(move_input.x * speed, rb.linearVelocityY);
        // isto vira os sprites
        if (move_input.x > 0)
            spriteRenderer.flipX = false;
        else if (move_input.x < 0)
            spriteRenderer.flipX = true;

        //verifica se pode escalar, se sim, deixar escalar
        if (canClimb)
        {
            rb.linearVelocity = new Vector2(move_input.x * speed, move_input.y * speed);
            rb.gravityScale = 0;
        }
        else
        {
            //revertem gravidade
            rb.gravityScale = originalGravity;
        }
    }

    private void Onjump(InputAction.CallbackContext context)
    {
        //verifica se está no chão, se sim, saltar
        if (isGrounded & !canClimb)
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jump_force);
        isGrounded = false;
    }

    private void WolfAbility(InputAction.CallbackContext context)
    {
        //verifica se está no chão, se sim, saltar
        if (isGrounded & CanUseWolfAbility & !canClimb)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, 25f);
            isGrounded = false;
            CanUseWolfAbility = false;
            StartCoroutine(ReactivateWolfAbility(2f));
        }
    }

    private void TarantulaAbility(InputAction.CallbackContext context)
    {
        //recolhe todos os objetos com tag laser
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Laser");
        foreach (GameObject go in gameObjectArray)
            //verifica se pode usar a habilidade
            if (!UsedTarantula)
            {
                {
                    //desativa lasers
                    go.SetActive(false);
                    UsedTarantula = true;
                    StartCoroutine(ReactivateLasers(gameObjectArray, 2f));
                }
            }
        //ativa timer de dois segundos para reativar
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //se colidir com o chão, ativa "isGrounded"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //morre se colidir com lasers
        if (collision.gameObject.tag == "Laser")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //se colidir com escalagem, ativar "canClimb"
        if (collision.gameObject.tag == "Climb")
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //o contrário
        if (collision.gameObject.tag == "Climb")
        {
            canClimb = false;
        }
    }

    private IEnumerator ReactivateLasers(GameObject[] lasers, float delay)
    {
        //espera os segundos
        yield return new WaitForSeconds(delay);

        foreach (GameObject laser in lasers)
        {
            //reativa lasers
            laser.SetActive(true);
        }

        yield return new WaitForSeconds(4);

        UsedTarantula = false;
    }

    private IEnumerator ReactivateWolfAbility(float delay)
    {
        //espera os segundos
        yield return new WaitForSeconds(delay);

        CanUseWolfAbility = true;
    }
}
