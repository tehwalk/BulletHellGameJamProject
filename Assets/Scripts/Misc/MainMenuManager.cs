using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene((int)SceneIndex.Level1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
