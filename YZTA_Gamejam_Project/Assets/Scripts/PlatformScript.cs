using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public Transform endPoint;
    public Transform endPoint2;
    public Transform endPoint3;
    public Transform platform;
    public Transform platform2;
    public Transform platform3;

    private Rigidbody2D buttonRb;


    public float speed;
    private void Start()
    {
        buttonRb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)//butona temas
    {
        if (collision.CompareTag("Character"))
        {
            if (platform != null)
                PositionAdjuster(platform, endPoint);
            if (platform2 != null)
                PositionAdjuster(platform2, endPoint2);
            if (platform3 != null)
                PositionAdjuster(platform3, endPoint3);
        }
    }
    private void Update()
    {

    }
    private void PositionAdjuster(Transform mainPlatform, Transform endPos)
    {
        Vector2 targetPos = endPos.position;
        mainPlatform.position = Vector2.Lerp(mainPlatform.position, targetPos, speed * Time.deltaTime);
    }
    
}
