using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject creditsPanel;
    [SerializeField] TextMeshProUGUI highScoreText;

    private void Start()
    {
        HideCredits();
        if (PlayerPrefs.GetInt("High") > 0)
        {
            highScoreText.gameObject.SetActive(true);
            highScoreText.text = "Highest Level: " + PlayerPrefs.GetInt("High").ToString();
        }
        else
        {
            highScoreText.gameObject.SetActive(false);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene((int)SceneIndex.Level1);
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
