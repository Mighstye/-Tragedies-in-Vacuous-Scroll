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

        void Start()
        {
            LogicSystemAPI.instance.Health.onPlayerDeath += () =>
            {
                endFight(false);
            };
            currentFightName = gameObject.scene.name;
            nextFightName = currentFightName.Remove(currentFightName.Length - 1) + (Convert.ToInt32(currentFightName[5].ToString()) + 1).ToString();
            // ^ This get next scene name for the next fight
        }

        public void endFight(bool victory)
        {
            Time.timeScale = 0f;
            boss.SetActive(false);
            ActiveBulletManager.instance.Wipe();
            if (victory) onWin?.Invoke();
            else onLoose?.Invoke();
        }

        public void nextFight()
        {
            try
            {
                SceneManager.LoadScene(nextFightName);
            }catch(Exception e)
            {
                Debug.Log(nextFightName + " not found !");
                SceneManager.LoadScene("MainMenu");
            }
        }

        public void restart()
        {
            SceneManager.LoadScene(currentFightName);
        }

        public void mainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
