using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 3f;
    private Vector3 startPos;
    private bool isChasing = false;
    private float lastX;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //pega na posição inicial
        startPos = transform.position;
        lastX = startPos.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChasing)
        {
            //multiplica o tempo com a velocidade e depois a distancia vezes 2, subtraindo a distancia normal
            float movement = Mathf.PingPong(Time.time * speed, distance * 2) - distance;
            float newX = startPos.x + movement;
            if (newX > lastX)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0); // Facing right
            }
            else if (newX < lastX)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0); // Facing left
            }
            //move o x com a variavel de cima
            transform.position = new Vector3(newX, startPos.y, startPos.z);
            lastX = newX;
        }
    }
}
