using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsManager : MonoBehaviour
{
    //public GameObject settingsPanel;
    public AudioMixer musicMixer, soundMixer;
    [SerializeField] GameObject fullScreenToggle;

    // Start is called before the first frame update
    private void Start()
    {
        gameObject.SetActive(false);
        #if UNITY_ANDROID
        fullScreenToggle.SetActive(false);
        #else 
        fullScreenToggle.SetActive(true);
        #endif
    }
    public void OpenSettingsPanel()
    {
        gameObject.SetActive(true);
    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ClosedSettingsPanel()
    {
        gameObject.SetActive(false);
    }

    public void SetMusicVolume(System.Single volume)
    {
        musicMixer.SetFloat("Volume", volume);
    }
    public void SetSFXVolume(System.Single volume)
    {
        soundMixer.SetFloat("Volume", volume);
    }
}
