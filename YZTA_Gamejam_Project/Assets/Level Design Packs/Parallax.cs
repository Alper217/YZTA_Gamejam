using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Material mat;
    float distance;
    [Range(0f, 5f)]
    public float speed = .2f;
    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }
    private void Update()
    {
        distance += Time.deltaTime*speed;
        mat.SetTextureOffset("_MainTex", Vector2.right * distance);
    }
}

