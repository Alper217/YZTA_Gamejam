using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightSwing : MonoBehaviour
{
    [Header("Sallanma Ayarlarý")]
    public float swingAngle = 15f; 
    public float swingSpeed = 2f;

    private float initialZRotation;

    void Start()
    {
        // Baþlangýç Z rotasyonunu kaydet
        initialZRotation = transform.eulerAngles.z;
    }

    void Update()
    {
        float angle = Mathf.Sin(Time.time * swingSpeed) * swingAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, initialZRotation + angle);
    }
}

