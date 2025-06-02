using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    private float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        TMP_Text text = FindFirstObjectByType<TextMeshPro>().GetComponent<TMP_Text>();
        text.text = timer.ToString();
        GameObject canva = GameObject.Find("Canvas");
        canva.SetActive(true);
        Destroy(gameObject);
    }
}
