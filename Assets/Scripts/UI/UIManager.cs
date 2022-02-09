using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{

    GameObject pauseObject;
    bool paused;
    // Start is called before the first frame update
    void Start()
    {
        pauseObject = GameObject.Find("PauseMenu");
        pauseObject.SetActive(false);
        paused = false;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase is not InputActionPhase.Performed) return;
        if (paused)
            unPause();
        else
            Pause();
    }

    public void BackToMenu()
    {
        unPause();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
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
