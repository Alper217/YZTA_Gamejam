using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUp : MonoBehaviour
{
    [SerializeField] private int nextSceneIndex;
    [SerializeField] private LevelTransitionEffect transitionEffect;
    [SerializeField] private float transitionDuration = 1.5f;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(LoadWithFadeCoroutine());
        }
    }

    private IEnumerator LoadWithFadeCoroutine()
    {
        if (transitionEffect != null)
        {
            transitionEffect.PlayFadeOut();
            Debug.Log("Fade out started.");
            yield return new WaitForSeconds(transitionDuration*Time.deltaTime);
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        Debug.Log("Scene loaded: " + SceneManager.GetActiveScene().buildIndex+1);
        
        yield return new WaitForSeconds(0.5f); // You can adjust this wait time if necessary
        if (transitionEffect != null)
        {
            transitionEffect.PlayFadeIn();
        }
    }
}
