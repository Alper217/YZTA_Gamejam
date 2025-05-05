/*using System.Collections;
using UnityEngine;

public class ShadowFollower : MonoBehaviour
{
    [SerializeField] private float shadowDelaySec;
    void FixedUpdate()
    {
        if (MovementRecorder.recordedPositions.Count > 0)
        {
            TransformData data = MovementRecorder.recordedPositions.Peek(); //Get the transform data
            StartCoroutine(shadowDelay(data));

            MovementRecorder.recordedPositions.Dequeue(); //Remove the oldest register.
        }
    }
    //Data will be defined with a delay.
    IEnumerator shadowDelay(TransformData data)
    {
        yield return new WaitForSeconds(shadowDelaySec);
        transform.position = data.position;
        transform.rotation = data.rotation;
    }

}
*/

using System.Collections.Generic;
using UnityEngine;

public class ShadowFollower : MonoBehaviour
{
    public Animator shadowAnimator;
    public Transform shadowTransform;
    public SpriteRenderer shadowSpriteRenderer;
    public float delayInSeconds = 5f;

    private class AnimationFrame
    {
        public float timestamp;
        public float speed;
        public bool ground;
        public bool isClimbing;
        public bool jumpTriggered;
        public bool flipX;
    }

    private Queue<AnimationFrame> animationQueue = new Queue<AnimationFrame>();
    private Animator playerAnimator;
    private SpriteRenderer playerSpriteRenderer;
    private Queue<TransformData> positionQueue => MovementRecorder.recordedPositions;

    private bool wasGroundLastFrame = true;
    private Vector3 initPos;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerAnimator = player.GetComponent<Animator>();
            playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        }
        shadowAnimator = GetComponent<Animator>();
        shadowTransform = GetComponent<Transform>();
        shadowSpriteRenderer = GetComponent<SpriteRenderer>();

        Debug.Log("shadowAnimator" + shadowAnimator);
        SceneResetter.Instance.OnScreenReset += ResetShadowPosition;

        
    }

    public void ResetShadowPosition()
    {
        // Debug.Log("Shadow position reset triggered.");
        // // Reset shadow position to the initial spawn point
        shadowTransform.position = initPos; // Replace with your initial spawn point
        shadowAnimator.SetBool("ground", true); // Reset animator state
        shadowAnimator.SetFloat("speed", 0);
        positionQueue.Clear();
        animationQueue.Clear();
    }

    void Update()
    {
        if (playerAnimator == null || playerSpriteRenderer == null) return;

        // Ana karakterin animasyon ve yön durumunu sıraya ekle
        float speed = playerAnimator.GetFloat("speed");
        Debug.Log("ilk speed:" + speed);
        bool ground = playerAnimator.GetBool("ground");
        bool isClimbing = playerAnimator.GetBool("isClimbing");
        bool jumpTriggered = wasGroundLastFrame && !ground;
        wasGroundLastFrame = ground;
        bool flipX = playerSpriteRenderer.flipX;

        animationQueue.Enqueue(new AnimationFrame
        {
            timestamp = Time.time,
            speed = speed,
            ground = ground,
            isClimbing = isClimbing,
            jumpTriggered = jumpTriggered,
            flipX = flipX
        });

        // Eski kaydı uygulamak için bekleme süresine bak
        while (animationQueue.Count > 0 && Time.time - animationQueue.Peek().timestamp >= delayInSeconds)
        {
            var frame = animationQueue.Dequeue();

            shadowAnimator.SetFloat("speed", frame.speed);
            shadowAnimator.SetBool("ground", frame.ground);
            shadowAnimator.SetBool("isClimbing", frame.isClimbing);
            shadowSpriteRenderer.flipX = frame.flipX;

            if (frame.jumpTriggered)
                shadowAnimator.SetTrigger("jump");
        }

        // Pozisyonu uygula
        if (positionQueue.Count > 0)
        {
            TransformData data = positionQueue.Peek();
            shadowTransform.position = data.position;
            shadowTransform.rotation = data.rotation;
        }
    }
}
