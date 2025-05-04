using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StairScripts : MonoBehaviour
{
    [SerializeField] GameObject stair;

    private bool isIn = false;
    private bool stop = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isIn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isIn = false;
            //text.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (isIn && Input.GetKeyDown(KeyCode.E) && !stop)
        {
            Tilemap stairTile = stair.GetComponent<Tilemap>();
            if (stairTile != null)
            {
                Color color = stairTile.color;
                color.a = 1f;
                stairTile.color = color;
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
