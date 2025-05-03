using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
public enum MusicType {
    MenuMusic,
    InGameMusic
}
[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] musicList;
    private static MusicManager instance;
    private AudioSource audioSource;

    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else{
            Destroy(this.gameObject);
        }
    }
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        PlayMusic(MusicType.MenuMusic, 0.5f);
    }

    public static void PlayMusic(MusicType musicType, float volume = 1) {
        instance.StartCoroutine(instance.EaseBetween(instance.audioSource, musicType, volume));
    }

    private IEnumerator EaseBetween(AudioSource audioSource, MusicType musicType, float volume) {
        float elapsedTime = 0f;
        float initialVolume = audioSource.volume;

        while(audioSource.volume > 0.05f) {
            audioSource.volume = Mathf.Lerp(initialVolume, 0f, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        audioSource.clip = instance.musicList[(int) musicType];
        audioSource.Play();
        audioSource.loop = true;

        elapsedTime = 0f;
        while (audioSource.volume < volume) {
            audioSource.volume = Mathf.Lerp(0f, volume, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
