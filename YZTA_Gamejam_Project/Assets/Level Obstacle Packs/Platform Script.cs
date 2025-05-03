using System.Collections;
using TMPro;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [SerializeField] GameObject platform;
    [SerializeField] TextMeshProUGUI text;

    private bool isIn = false;
    private bool isMoving = false;
    private bool stop=false;
    private Vector3 targetPosition;
    private float moveSpeed = 2f;
    [SerializeField] float platformDistance = 5;

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
        if (isIn && !isMoving && Input.GetKeyDown(KeyCode.E)&& !stop)
        {
            targetPosition = platform.transform.position + new Vector3(0, platformDistance, 0);
            isMoving = true;
        }
        if (isMoving)
        {
            platform.transform.position = Vector3.MoveTowards(
                platform.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            if (Vector3.Distance(platform.transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                stop = true;
            }
        }
    }
}
