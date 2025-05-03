using UnityEngine;

public class Parallax : MonoBehaviour
{
    Material mat;
    float offset;
    public float scrollSpeed = 0.1f;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        offset += Time.deltaTime * scrollSpeed;
        mat.mainTextureOffset = new Vector2(offset, 0);
    }
}
