using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax_Controller : MonoBehaviour
{
    public Transform target; // Karakter
    private Vector3 startTargetPos;
    private float distance;

    private GameObject[] backgrounds;
    private Material[] materials;

    [Tooltip("Arka plan katmanlarının hız oranları. Her katman için bir değer girin.")]
    public float[] backspeed;

    [Range(0.01f, 5f)]
    public float parallaxSpeed = 0.2f;

    private void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
                target = player.transform;
            else
                Debug.LogError("Parallax_Controller: Karakter bulunamadı! Tag 'Player' atanmış mı?");
        }

        startTargetPos = target.position;

        int backCount = transform.childCount;

        if (backspeed.Length != backCount)
        {
            Debug.LogError("Parallax_Controller: backspeed dizisi ile arka plan sayısı uyuşmuyor!");
            return;
        }

        materials = new Material[backCount];
        backgrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;

            Renderer renderer = backgrounds[i].GetComponent<Renderer>();
            if (renderer == null)
            {
                Debug.LogError($"Parallax_Controller: {backgrounds[i].name} nesnesinde Renderer yok!");
                continue;
            }

            materials[i] = renderer.material;
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        distance = target.position.x - startTargetPos.x;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (materials[i] == null) continue;

            float speed = backspeed[i] * parallaxSpeed;
            materials[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed / 10f);
        }
    }
}