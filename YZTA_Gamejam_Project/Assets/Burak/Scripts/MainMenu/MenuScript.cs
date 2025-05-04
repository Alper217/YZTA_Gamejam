using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject creditsMenu;
    public GameObject settingsMenu;
    public GameObject volumeSlider;
    public List<Sprite> volumeIcons = new List<Sprite>();
    void Start()
    {
        Slider slider = volumeSlider.GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("Volume", 5f);
        AudioListener.volume = slider.value / 10f;

        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }
    public void StartGame()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetSceneByName("GameScene") != null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
    }
    public void OpenCredits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        creditsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume / 10f;

        if (volume == 1)
        {
            volumeSlider.transform.Find("Background").GetComponent<Image>().sprite = volumeIcons[1];
        }
        else if (volume == 2)
        {
            volumeSlider.transform.Find("Background").GetComponent<Image>().sprite = volumeIcons[2];
        }
        else if (volume == 3)
        {
            volumeSlider.transform.Find("Background").GetComponent<Image>().sprite = volumeIcons[3];
        }
        else if (volume == 4)
        {
            volumeSlider.transform.Find("Background").GetComponent<Image>().sprite = volumeIcons[4];
        }
        else if (volume == 5)
        {
            volumeSlider.transform.Find("Background").GetComponent<Image>().sprite = volumeIcons[5];
        }
        else if (volume == 6)
        {
            volumeSlider.transform.Find("Background").GetComponent<Image>().sprite = volumeIcons[6];
        }
        else if (volume == 7)
        {
            volumeSlider.transform.Find("Background").GetComponent<Image>().sprite = volumeIcons[7];
        }
        else if (volume == 8)
        {
            volumeSlider.transform.Find("Background").GetComponent<Image>().sprite = volumeIcons[8];
        }
        else if (volume == 9)
        {
            volumeSlider.transform.Find("Background").GetComponent<Image>().sprite = volumeIcons[9];
        }
        else if (volume == 10)
        {
            volumeSlider.transform.Find("Background").GetComponent<Image>().sprite = volumeIcons[10];
        }
        Debug.Log("Volume set to: " + volume);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
