using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private float speed = 10f;
    private float jump_force = 17f;
    public Rigidbody2D rb;
    private bool isGrounded;
    private bool canClimb = false;
    private bool UsedTarantula = false;
    public SpriteRenderer spriteRenderer;
    private float originalGravity;
    void Start()
    {
        //guarda a gravidade original
        originalGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        // lê valor das setas/joystick
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 move_input = new Vector2(moveX, moveY);
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            TarantulaAbility();
        }
    }

    private void Jump()
    {
        //verifica se está no chão, se sim, saltar
        if (isGrounded && !canClimb)
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jump_force);
        isGrounded = false;
    }

    private void TarantulaAbility()
    {
        //verifica se pode usar a habilidade
        if (!UsedTarantula)
        {
            UsedTarantula = true;
            //recolhe todos os objetos com tag laser
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Laser");
            //desativa lasers
            foreach (GameObject go in gameObjectArray)
            {
                go.SetActive(false);
            }
            //ativa timer de dois segundos para reativar
            StartCoroutine(ReactivateLasers(gameObjectArray, 2f));
        }
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

        yield return new WaitForSeconds(1);

        UsedTarantula = false;
    }
}
