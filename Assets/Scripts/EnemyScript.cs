using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 3f;
    private Vector3 startPos;
    private float direction = 1f; // 1 = right, -1 = left
    private float walkTimer = 0f;
    public float walkDuration = 2f; // Time to walk in one direction

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //pega na posição inicial
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Caminha para um lado por um tempo, depois para o outro
        walkTimer += Time.deltaTime;
        if (walkTimer >= walkDuration)
        {
            direction *= -1f; // Inverte direção
            walkTimer = 0f;
            // Rotaciona para a direção correta
            if (direction > 0)
                transform.rotation = Quaternion.Euler(0, 0, 0); // Direita
            else
                transform.rotation = Quaternion.Euler(0, 180, 0); // Esquerda
        }

        // Move o inimigo para a esquerda ou direita
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);
    }
}