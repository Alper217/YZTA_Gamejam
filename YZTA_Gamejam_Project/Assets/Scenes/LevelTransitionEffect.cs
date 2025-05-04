using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LevelTransitionEffect : MonoBehaviour
{
    public Volume volume;
    private Vignette vignette;
    private ColorAdjustments colorAdjust;

    public float duration = 1.5f;

    private void Start()
    {
        // Efekt referanslarýný al
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out colorAdjust);
    }

    public void PlayFadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeEffect(0f, 0.6f, 0f, -100f)); // Fade to black
    }

    public void PlayFadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadeEffect(0.6f, 0f, -100f, 0f)); // Fade from black
    }

    private System.Collections.IEnumerator FadeEffect(float startVignette, float endVignette, float startExposure, float endExposure)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float normalizedTime = t / duration;

            if (vignette != null)
                vignette.intensity.value = Mathf.Lerp(startVignette, endVignette, normalizedTime);

            if (colorAdjust != null)
                colorAdjust.postExposure.value = Mathf.Lerp(startExposure, endExposure, normalizedTime);

            yield return null;
        }
    }
}
