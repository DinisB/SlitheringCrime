using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    private float timer = 0;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject canva;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        text.text = timer.ToString();
        canva.SetActive(true);
        Destroy(gameObject);
    }
}
