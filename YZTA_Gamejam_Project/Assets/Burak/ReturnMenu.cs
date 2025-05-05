using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMenu : MonoBehaviour
{
    private float elapsedTime = 0f;
    private float timeToWait = 10f; // Time to wait before returning to the main menu

    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= timeToWait)
        {
            SceneManager.LoadScene("MainMenu1"); // Replace with your main menu scene name
        }
    }
}
