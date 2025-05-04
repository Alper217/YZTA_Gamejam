using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorHistory : MonoBehaviour
{
    public Animator shadowAnimator;            // Gölge animatörü
    public float delaySeconds = 5f;            // Gecikme süresi
    private float recordInterval = 0.02f; // 50 FPS (FixedUpdate)

    private Animator playerAnimator;

    private class AnimationFrame
    {
        public float timestamp;
        public float speed;
        public bool ground;
        public bool isClimbing;
        public bool jumpTriggered;
    }

    private Queue<AnimationFrame> animationQueue = new Queue<AnimationFrame>();

    private bool wasGroundLastFrame = true;    // Jump trigger kontrolü için

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // Gerekli animasyon parametrelerini al
        float speed = playerAnimator.GetFloat("speed");
        bool ground = playerAnimator.GetBool("ground");
        bool isClimbing = playerAnimator.GetBool("isClimbing");

        // Jump trigger'ı tespiti (ground → false geçişinde tetikleniyor)
        bool jumpTriggered = wasGroundLastFrame && !ground;
        wasGroundLastFrame = ground;

        // Kuyruğa ekle
        animationQueue.Enqueue(new AnimationFrame
        {
            timestamp = Time.time,
            speed = speed,
            ground = ground,
            isClimbing = isClimbing,
            jumpTriggered = jumpTriggered
        });

        // Gecikmeyi geçen frame'leri uygula
        while (animationQueue.Count > 0 && Time.time - animationQueue.Peek().timestamp >= delaySeconds)
        {
            var frame = animationQueue.Dequeue();

            shadowAnimator.SetFloat("speed", frame.speed);
            shadowAnimator.SetBool("ground", frame.ground);
            shadowAnimator.SetBool("isClimbing", frame.isClimbing);

            if (frame.jumpTriggered)
            {
                shadowAnimator.SetTrigger("jump");
            }
        }
    }
}
