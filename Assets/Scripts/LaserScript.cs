using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 3f;
    private Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //pega na posição inicial
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //multiplica o tempo com a velocidade e depois a distancia vezes 2, subtraindo a distancia normal
        float movement = Mathf.PingPong(Time.time * speed, distance * 2) - distance;
        //move o x com a variavel de cima
        transform.position = new Vector3(startPos.x + movement, startPos.y, startPos.z);
    }
}