using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour
{
    public AudioClip btnSound;
    public Slider volumeSlider;

    AudioSource audioSource;
    FadeScene fadeScene;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fadeScene = FindObjectOfType<FadeScene>();
        Time.timeScale = 1;

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }
    public void startGame()
    {
        soundBtn();
        fadeScene.SetTriggerFadeOutMenu2Game();
    }
    
    public void quitGame()
    {
        soundBtn();
        Debug.Log("Quit Game !!!");
    }

    public void soundBtn()
    {
        audioSource.PlayOneShot(btnSound);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }
    void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
