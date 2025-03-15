using UnityEngine;

public class CamScript : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;
    public float deadZoneX = 2f;
    public float deadZoneY = 1.5f;
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void FixedUpdate()
    {
        float playerX = player.position.x;
        float playerY = player.position.y;
        float cameraX = transform.position.x;
        float cameraY = transform.position.y;

        float targetX = cameraX;
        float targetY = cameraY;

        //se o teu x e a subtração do da camara for maior que a deadzone, permitir mover camara
        if (Mathf.Abs(playerX - cameraX) > deadZoneX)
        {
            targetX = playerX - Mathf.Sign(playerX - cameraX) * deadZoneX;
            targetX = Mathf.Clamp(targetX, minX, maxX);
        }

        //idem, só que para o x
        if (Mathf.Abs(playerY - cameraY) > deadZoneY)
        {
            targetY = playerY - Mathf.Sign(playerY - cameraY) * deadZoneY;
            targetY = Mathf.Clamp(targetY, minY, maxY);
        }

        //mover camara
        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}

