using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using BulletSystem;
using Logic_System;

namespace Game_Manager
{
    public class GameManagerAPI : MonoBehaviour
    {
        public static GameManagerAPI instance { get; private set; }

        public GameObject boss;
        public Action onWin;
        public Action onLoose;

        private string nextFightName;
        private string currentFightName;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            LogicSystemAPI.instance.Health.onPlayerDeath += () =>
            {
                EndFight(false);
            };
            currentFightName = gameObject.scene.name;
            nextFightName = currentFightName.Remove(currentFightName.Length - 1) + (Convert.ToInt32(currentFightName[5].ToString()) + 1).ToString();
            // ^ This get the next scene name for the next fight
        }

        public void EndFight(bool victory)
        {
            Time.timeScale = 0f;
            boss.SetActive(false);
            ActiveBulletManager.instance.Wipe();
            if (victory) onWin?.Invoke();
            else onLoose?.Invoke();
        }

        public void NextFight()
        {
            try
            {
                SceneManager.LoadScene(nextFightName);
            }catch(Exception e)
            {
                // If there is no next scene -> Means the player reached the end of the game
                // Ending cinematic + game end + credits ...
                Debug.Log(nextFightName + " not found !");
                SceneManager.LoadScene("MainMenu");
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(currentFightName);
        }

        public static void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
