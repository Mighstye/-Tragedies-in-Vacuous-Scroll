using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Logic_System;

public class UIManager : MonoBehaviour
{

    public GameObject pauseObject;
    public GameObject gameOverObject;
    bool paused;
    // Start is called before the first frame update
    void Start()
    {
        pauseObject.SetActive(false);
        gameOverObject.SetActive(false);
        paused = false;

        LogicSystemAPI.instance.Health.onPlayerDeath += () =>
        {
            GameOver();
        };
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        Debug.Log("Onpause");
        if (context.phase is not InputActionPhase.Performed) return;
        if (LogicSystemAPI.instance.Health.currentHealth <= 0) return;
        if (paused)
            unPause();
        else
            Pause();
    }

    public void BackToMenu()
    {
        if(paused) { unPause(); }
        else
        {
            Time.timeScale = 1.0f;
            gameOverObject.SetActive(false);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverObject.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseObject.SetActive(true);
        paused = true;
    }

    public void unPause()
    {
        Time.timeScale = 1.0f;
        pauseObject.SetActive(false);
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
