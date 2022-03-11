using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Game_Manager;

public class UIManager : MonoBehaviour
{

    public GameObject pauseObject;
    public GameObject gameOverObject;
    public GameObject WinObject;
    public GameObject WinPostMenuObject;
    bool paused;
    bool gameFinished = false;
    private GameManagerAPI gameManagerAPI;
    // Start is called before the first frame update
    void Start()
    {
        pauseObject.SetActive(false);
        gameOverObject.SetActive(false);
        WinObject.SetActive(false);
        WinPostMenuObject.SetActive(false);
        gameManagerAPI = GameManagerAPI.instance;
        paused = false;

        gameManagerAPI.onLoose += () =>
        {
            gameFinished = true;
            GameOver();
        };

        gameManagerAPI.onWin += () =>
        {
            gameFinished = true;
            Win();
        };
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        Debug.Log("Onpause");
        if (context.phase is not InputActionPhase.Performed) return;
        if (gameFinished) return;
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
            DisableAllMenu();
        }
        gameManagerAPI.mainMenu();
    }

    public void Continue()
    {
        if (paused) { unPause(); }
        else
        {
            Time.timeScale = 1.0f;
            DisableAllMenu();
        }
        gameManagerAPI.nextFight();
    }

    public void Restart()
    {
        if (paused) { unPause(); }
        else
        {
            Time.timeScale = 1.0f;
            DisableAllMenu();
        }
        gameManagerAPI.restart();
    }

    private void DisableAllMenu()
    {
        pauseObject.SetActive(false);
        gameOverObject.SetActive(false);
        WinObject.SetActive(false);
        WinPostMenuObject.SetActive(false);
    }

    public void WinContinueButton()
    {
        WinObject.SetActive(false);
        WinPostMenuObject.SetActive(true);
    }

    private void GameOver()
    {
        gameOverObject.SetActive(true);
    }

    private void Win()
    {
        WinObject.SetActive(true);
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
}
