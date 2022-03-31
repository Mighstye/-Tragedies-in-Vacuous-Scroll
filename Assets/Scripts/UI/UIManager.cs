using GameManager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public GameObject pauseObject;
        public GameObject gameOverObject;
        public GameObject winObject;
        public GameObject rewardCardMenu;
        private bool gameFinished;

        private bool paused;

        // Start is called before the first frame update
        private void Start()
        {
            DisableAllMenu();
            paused = false;
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            Debug.Log("Onpause");
            if (context.phase is not InputActionPhase.Performed) return;
            if (gameFinished) return;
            if (paused)
                UnPause();
            else
                Pause();
        }

        public void BackToMenu()
        {
            if (paused)
            {
                UnPause();
            }
            else
            {
                Time.timeScale = 1.0f;
                DisableAllMenu();
            }

            GameManagerAPI.MainMenu();
        }

        public void Continue()
        {
            if (paused)
            {
                UnPause();
            }
            else
            {
                Time.timeScale = 1.0f;
                DisableAllMenu();
            }

            //gameManagerAPI.NextFight();
        }

        public void Restart()
        {
            if (paused)
            {
                UnPause();
            }
            else
            {
                Time.timeScale = 1.0f;
                DisableAllMenu();
            }

            //gameManagerAPI.Restart();
        }

        private void DisableAllMenu()
        {
            foreach (Transform child in gameObject.transform) child.gameObject.SetActive(false);
        }

        public void WinContinueButton()
        {
            winObject.SetActive(false);
            rewardCardMenu.SetActive(true);
        }

        private void GameOver()
        {
            gameOverObject.SetActive(true);
        }

        private void Win()
        {
            winObject.SetActive(true);
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            pauseObject.SetActive(true);
            paused = true;
        }

        public void UnPause()
        {
            Time.timeScale = 1.0f;
            pauseObject.SetActive(false);
            paused = false;
        }
    }
}