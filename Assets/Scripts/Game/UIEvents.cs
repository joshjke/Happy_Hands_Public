using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public GameObject pauseMenu;

    private GameState state;
    bool justResumed = false;

    enum GameState { Active, Paused, Photomode }

    private AudioSource menuSubmitSource;
    private AudioSource menuMoveSource;

    public AudioClip menuSubmitClip;
    public AudioClip menuMoveClip;


    private void Start()
    {
        pauseMenu.SetActive(false);

        AudioSource[] sources = GetComponents<AudioSource>();
        menuSubmitSource = sources[0];
        menuSubmitSource.clip = menuSubmitClip;

        menuMoveSource = sources[1];
        menuMoveSource.clip = menuMoveClip;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Vertical") && state == GameState.Paused)
            menuMoveSource.Play();

        if (Input.GetButtonDown("Submit") && !justResumed) 
        {
            menuSubmitSource.Play();
            if (state == GameState.Active)
            {
                state = GameState.Paused;
                pauseMenu.SetActive(true);
                GameManager.Instance.PauseGame();
            }
            else if (state == GameState.Photomode)
            {
                EndPhotoMode();
            }
        }
        justResumed = false;

    }

    public void Resume()
    {
        DeactivatePause();
        GameManager.Instance.ResumeGame();
    }

    public void StartNextRound()
    {
        DeactivatePause();
        GameManager.Instance.StartNextRound();
    }

    public void RestartEntireGame()
    {
        DeactivatePause();
        GameManager.Instance.StartTutorial();
    }

    public void RestartRound()
    {
        DeactivatePause();
        GameManager.Instance.RestartRound();
    }

    private void DeactivatePause()
    {
        justResumed = true;
        state = GameState.Active;
        pauseMenu.SetActive(false);
    }

    public void StartPhotoMode()
    {
        justResumed = true;
        state = GameState.Photomode;
        pauseMenu.SetActive(false);
    }
    public void EndPhotoMode()
    {
        state = GameState.Paused;
        pauseMenu.SetActive(true);
    }

    
}
