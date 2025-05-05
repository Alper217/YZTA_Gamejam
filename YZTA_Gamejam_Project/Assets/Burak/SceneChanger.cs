using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManager namespace for scene management

public class SceneChanger : MonoBehaviour
{
    public float cutsceneDuration = 5f; // Duration of the fade effect
    private float elapsedTime = 0f; // Time elapsed since the cutscene started

    // Start is called before the first frame update
    void Start()
    {
            
    }

    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime; // Increment elapsed time by the time since the last frame
        if (elapsedTime >= cutsceneDuration)
        {
            // Load the next scene or perform any other action after the cutscene duration
            Debug.Log("Cutscene finished. Loading next scene...");
            // Example: Load the next scene using Unity's SceneManager
            SceneManager.LoadScene("Level1");
        }
    }
}
