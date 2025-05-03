using System.Collections;
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
