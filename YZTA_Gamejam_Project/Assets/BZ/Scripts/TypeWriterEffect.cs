using TMPro;
using UnityEngine;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    [TextArea]
    public string fullText = ""; //"Sistem başlatılıyor...\nVeriler yükleniyor...";
    public float typingSpeed = 0.05f;

    public AudioSource typingSound;

    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        textUI.text = "";

        // Ses başlasın ve loop yapsın
        if (typingSound != null)
        {
            typingSound.loop = true;
            typingSound.Play();
        }

        foreach (char c in fullText)
        {
            textUI.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Yazı bitti, ses durdurulsun
        if (typingSound != null && typingSound.isPlaying)
        {
            typingSound.Stop();
        }
    }
}
