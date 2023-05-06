using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using BulletFury;

public enum GameState { Playing, Paused, Won, Lost }
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    [Header("In Game Properties")]
    [SerializeField] float maxTimeInSeconds;
    float timeRemaining;
    GameState gameState;
    [Space]
    [Header("GUI Properties")]
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI wonScoreText;
    [SerializeField] GameObject pauseGUI, lostGUI, wonGUI;
    AudioManager audioManager;
    PlayerLevel playerLevel;

    private void Awake()
    {
        if (instance != null && instance != this) instance = null;
        instance = this;
    }
    void Start()
    {
        audioManager = AudioManager.Instance;
        playerLevel = PlayerLevel.Instance;
        Time.timeScale = 1;
        //globalManager = GetComponent<BulletManager>();
        timeRemaining = maxTimeInSeconds;
        pauseGUI.SetActive(false);
        lostGUI.SetActive(false);
        wonGUI.SetActive(false);
        gameState = GameState.Playing;
        audioManager.PlayBattleTrack();

    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Playing:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Pause();
                }
                if (timeRemaining > 0)
                {
                    float minutes = Mathf.FloorToInt(timeRemaining / 60);
                    float seconds = Mathf.FloorToInt(timeRemaining % 60);
                    timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                    timeRemaining -= Time.deltaTime;
                }
                else
                {
                    Won();
                }
                break;
            case GameState.Paused:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Resume();
                }
                break;
            case GameState.Won:
                break;
            case GameState.Lost:
                break;
            default:
                break;
        }
    }

    public void Lost()
    {
        Time.timeScale = 0;
        lostGUI.SetActive(true);
        gameState = GameState.Lost;
        audioManager.PlayLostTrack();
    }

    public void Won()
    {
        Time.timeScale = 0;
        wonGUI.SetActive(true);
        wonScoreText.text = "Level Reached: " + playerLevel.Level.ToString();
        gameState = GameState.Won;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseGUI.SetActive(true);
        gameState = GameState.Paused;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pauseGUI.SetActive(false);
        gameState = GameState.Playing;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene((int)SceneIndex.MainMenu);
    }
}
