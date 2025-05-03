using System.Collections.Generic;
using UnityEngine;

public class MovementRecorder : MonoBehaviour
{
    public static Queue<TransformData> recordedPositions = new Queue<TransformData>(); //A queue for position and rotation

    [SerializeField] public float delayInSeconds = 5f;

    private float timer = 0f;
    private float recordInterval = 0.02f; // 50 FPS (FixedUpdate)

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        //Position and rotation register to queue
        if (timer >= recordInterval)
        {
            recordedPositions.Enqueue(new TransformData(transform.position, transform.rotation));
            timer = 0f;
        }

        //If queue count is bigger than max frames, remove the oldest register from queue.
        int maxFrames = Mathf.CeilToInt(delayInSeconds / recordInterval);
        if (recordedPositions.Count > maxFrames)
        {
            recordedPositions.Dequeue();
        }
    }
}
