using System.Collections;
using TMPro;
using UnityEngine;

public class PlatformScriptAlper : MonoBehaviour
{
    [SerializeField] GameObject platform;
    [SerializeField] private float platformDelaySec = 1f;
    private Vector3 initPlatformPos;

    private bool isIn = false;
    private bool isMoving = false;
    private bool stop=false;
    private Vector3 targetPosition;
    private float moveSpeed = 2f;
    [SerializeField] float platformDistance = 5;

    void OnDisable()
    {
        SceneResetter.Instance.OnScreenReset -= ResetPlatformPosition;
    }

    void Start()
    {
        initPlatformPos = platform.transform.position;
        SceneResetter.Instance.OnScreenReset += ResetPlatformPosition;

    }

    void ResetPlatformPosition() {
        Debug.Log("asd1");
        platform.transform.position = initPlatformPos;
        isMoving = false;
        stop = false;
        isIn = false;
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isIn = true;
            SFXManager.PlaySound(SoundType.ButtonHover);
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
        if (isIn && !isMoving && Input.GetKeyDown(KeyCode.E)&& !stop)
        {
            if(platform.CompareTag("HorizontalPlatform"))
                targetPosition = platform.transform.position + new Vector3(platformDistance, 0, 0);

            else if (platform.CompareTag("VerticalPlatform"))
                targetPosition = platform.transform.position + new Vector3(0, platformDistance, 0);
            isMoving = true;
            SFXManager.PlaySound(SoundType.ButtonClick);
            
        }
        if (isMoving)
        {
            StartCoroutine(platformDelay());

            if (Vector3.Distance(platform.transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                stop = true;
            }
        }
    }
    IEnumerator platformDelay()
    {
        yield return new WaitForSeconds(platformDelaySec);
            SFXManager.PlaySound(SoundType.PlatformMove);
        
        platform.transform.position = Vector3.MoveTowards(
                platform.transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
        );
            SFXManager.PlaySound(SoundType.PlatformStop);

    }
}
