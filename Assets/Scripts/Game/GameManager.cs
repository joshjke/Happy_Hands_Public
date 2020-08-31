using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton implementation
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public enum GameRound {Tutorial, VanillaRound, StealthRound, Shield1Round, DoubleRound, BringYourKidRound, CourtCutscene}
    public GameRound currentRound;
    private GameRound nextRound;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }


    public EnemyManager enemyManager;
    public HeadMovement headMovement;
    public WASD_UI wasd;
    public WASD_UI arrowKeys;
    public GameObject baseFace;
    public WinScreen winScreen;

    public bool paused = false;


    private void Start()
    {
        wasd.gameObject.SetActive(false);
        arrowKeys.gameObject.SetActive(false);
        winScreen.EndWinScreen();
        RestartRound();
    }
    
    // Round Management
    public void StartNextRound()
    {
        StartRound(nextRound);
    }
    public void RestartRound()
    {
        if (enemyManager != null)
            enemyManager.DeactivateAllFlies();
        StartRound(currentRound);
    }
    // To add more rounds, add the round to the enum, then add a case in the switch statement
    private void StartRound(GameRound round)
    {
        switch (round)
        {
            case GameRound.Tutorial:
                StartTutorial();
                break;
            case GameRound.VanillaRound:
                StartVanillaRound();
                break;
            case GameRound.StealthRound:
                StartStealthRound();
                break;
            case GameRound.Shield1Round:
                StartShield1Round();
                break;
            case GameRound.DoubleRound:
                StartDoubleRound();
                break;
            case GameRound.BringYourKidRound:
                StartBringYourKidRound();
                break;
            case GameRound.CourtCutscene:
                StartCourtCutscene();
                break;
            default:
                StartTutorial();
                break;
        }
    }

    public void StartTutorial()
    {
        currentRound = GameRound.Tutorial;
        nextRound = GameRound.VanillaRound;
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Playground"))
            SceneManager.LoadScene("Playground");
        wasd.gameObject.SetActive(true);
        wasd.StartTutorial();

        arrowKeys.gameObject.SetActive(true);
        arrowKeys.StartTutorial();
        wasd.smackUI.StartTutorial();

        StartRoundDefaults();
        enemyManager.DeactivateAllFlies();
        baseFace.transform.position = Vector3.zero;
    }

    public void StartVanillaRound()
    {
        currentRound = GameRound.VanillaRound;
        nextRound = GameRound.BringYourKidRound;
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Playground"))
            SceneManager.LoadScene("Playground");
        else
        {
            StartRoundDefaults();
        }
    }
    private void StartBringYourKidRound()
    {
        currentRound = GameRound.BringYourKidRound;
        nextRound = GameRound.CourtCutscene;
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Playground"))
            SceneManager.LoadScene("Playground");
        else
        {
            StartRoundDefaults();
            enemyManager.StartBringYourKidMode();
        }
    }
    private void StartCourtCutscene()
    {
        currentRound = GameRound.CourtCutscene;
        nextRound = GameRound.StealthRound;
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("CourtScene"))
            SceneManager.LoadScene("CourtScene");
    }
    private void StartStealthRound()
    {
        currentRound = GameRound.StealthRound;
        nextRound = GameRound.Shield1Round;
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("PostCourt1"))
            SceneManager.LoadScene("PostCourt1");
        else
        {
            StartRoundDefaults();
            enemyManager.StartStealthMode();
        }
    }
    private void StartShield1Round()
    {
        currentRound = GameRound.Shield1Round;
        nextRound = GameRound.DoubleRound;
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("PostCourt1"))
            SceneManager.LoadScene("PostCourt1");
        else
        {
            StartRoundDefaults();
            enemyManager.StartShield1Mode();
        }
    }
    private void StartDoubleRound()
    {
        currentRound = GameRound.DoubleRound;
        nextRound = GameRound.VanillaRound;
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("PostCourt1"))
            SceneManager.LoadScene("PostCourt1");
        else
        {
            StartRoundDefaults();
            enemyManager.StartDoubleFlyMode();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        paused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        paused = false;
    }

    public void StartRoundDefaults()
    {
        Time.timeScale = 1f;
        if (enemyManager != null)
            enemyManager.ResetEnemies();
        paused = false;
        winScreen.EndWinScreen();
    }

    public void GameOver()
    {
        Invoke("StartNewGame", 1f);
    }

    public void GameWon()
    {
        winScreen.gameObject.SetActive(true);
        winScreen.ReviveMenuFly();
    }
}
