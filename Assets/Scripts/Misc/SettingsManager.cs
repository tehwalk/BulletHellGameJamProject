using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    //public GameObject settingsPanel;
    public AudioMixer musicMixer, soundMixer;
    // Start is called before the first frame update
    private void Start()
    {
        gameObject.SetActive(false);
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

    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("Volume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        soundMixer.SetFloat("Volume", volume);
    }
}
