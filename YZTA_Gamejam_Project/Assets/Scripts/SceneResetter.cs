using System;
using UnityEngine;

public class SceneResetter : MonoBehaviour
{
    // Singleton instance
    public static SceneResetter Instance { get; private set; }

    private void Awake()
    {
        // Singleton kontrolü
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Sahne geçişinde kalmasını istiyorsan
    }

    // Events
    public event Action OnScreenReset;

    // Bu methodu çağırarak event'i tetikleyebilirsin
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnScreenReset?.Invoke();
        }       
    }
}
