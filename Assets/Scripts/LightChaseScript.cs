using UnityEngine;

public class LightChaseScript : MonoBehaviour
{
    public EnemyScript enemy;
    private float originalSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalSpeed = enemy.speed;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.speed = originalSpeed * 1.25f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.speed = originalSpeed;
        }
    }
}
