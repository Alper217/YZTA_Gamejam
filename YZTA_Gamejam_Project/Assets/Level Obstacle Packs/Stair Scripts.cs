using System.Collections;
using TMPro;
using UnityEngine;

public class StairScripts : MonoBehaviour
{
    [SerializeField] GameObject stair;
    [SerializeField] TextMeshProUGUI text;

    private bool isIn = false;
    private bool stop = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isIn = true;
            text.gameObject.SetActive(true);
            text.text = text.text.ToUpper();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isIn = false;
            text.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (isIn && Input.GetKeyDown(KeyCode.E) && !stop)
        {
            SpriteRenderer sr = stair.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Color color = sr.color;
                color.a = 1f;
                sr.color = color;
            }
            Collider2D col = stair.GetComponent<Collider2D>();
            if (col != null)
            {
                col.enabled = true;
            }
            stop = true; 
        }
    }
}
