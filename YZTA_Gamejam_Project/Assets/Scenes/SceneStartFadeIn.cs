using UnityEngine;

public class SceneStartFadeIn : MonoBehaviour
{
    [SerializeField] private LevelTransitionEffect transitionEffect;

    private void Start()
    {
        if (transitionEffect != null)
        {
            Debug.Log("Fade-in �al��t�!");
            transitionEffect.PlayFadeIn();
        }
        else
        {
            Debug.LogWarning("transitionEffect sahnede atanmad�!");
        }
    }

}
