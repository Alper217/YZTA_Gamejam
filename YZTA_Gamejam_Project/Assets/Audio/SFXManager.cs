using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;
using Unity.VisualScripting;

public enum SoundType {
    Walk1,
    Walk2,
    Jump1,
    ButtonClick,
    ButtonHover,
    PlatformMove,
    PlatformStop,
    Door,
    Teleport1,
    Teleport2
}

public class SFXManager : MonoBehaviour{
    public static SFXManager Instance;
    public GameObject oneShotClip;
    public AudioSource audioSource; 
    [SerializeField] public SFXClips[] clips;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else{
            Destroy(this.gameObject);
        }
    }
    
    public static void PlaySound(SoundType sound) {
        if(Instance.oneShotClip == null) {
            Instance.oneShotClip = new GameObject("OneShotSound");
            Instance.oneShotClip.transform.parent = Instance.transform;
            Instance.audioSource = Instance.oneShotClip.AddComponent<AudioSource>();
        }
        SFXClips clip = Instance.clips.FirstOrDefault((clip) => clip.soundType == sound);
        if(clip == null) {
            Debug.LogError("Sound not found: " + sound);
            return;
        }
        Instance.audioSource.volume = clip.volume;
        Instance.audioSource.PlayOneShot(clip.clip);
    }
}

[System.Serializable]
public class SFXClips {
    public SoundType soundType;
    public AudioClip clip;
    [Range(0f,1f)] public float volume = 0.5f;
}
