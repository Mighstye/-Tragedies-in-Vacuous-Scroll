using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using BulletSystem;
using Logic_System;
using CardSystem;
using TMPro;

namespace Game_Manager
{
    public class GameManagerAPI : MonoBehaviour
    {
        public static GameManagerAPI instance { get; private set; }

        public GameObject boss;
        public GameObject textRewardList;
        public GameObject activeCard;
        public GameObject passiveCard;
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
            //SetActive for cards selected by the player
            /*
            foreach(Transform child in activeCard.transform)
            {
                Debug.Log(child.name);
                Card card = child.GetComponent<Card>();
                if (card is ActiveCard)
                    if (PlayerInfos.SelectedActiveCard.Contains((ActiveCard)card)) child.gameObject.SetActive(true);
            }
            foreach(Transform child in passiveCard.transform)
            {
                Debug.Log(child.name);
                Card card = child.GetComponent<Card>();
                if (card is PassiveCard)
                    if (PlayerInfos.SelectedPassiveCard.Contains((PassiveCard)card)) child.gameObject.SetActive(true);
            }
            */

            LogicSystemAPI.instance.health.onPlayerDeath += () =>
            {
                EndFight(false);
            };
            currentFightName = gameObject.scene.name;
            // ^ This get the next scene name for the next fight
        }

        public void EndFight(bool victory)
        {
            Time.timeScale = 0f;
            boss.SetActive(false);
            ActiveBulletManager.instance.Wipe();
            if (victory) onWin?.Invoke();
            else onLoose?.Invoke();

            /*
            foreach(GameObject c in rewards)
            {
                PlayerInfos.unlockC(c.GetComponent<Card>());
            }
            */
        }

        public void generateReward()
        {
            //Rewards
            List<GameObject> rewards = LogicSystemAPI.instance.rewardSystem.getReward();
            foreach (GameObject reward in rewards)
            {
                textRewardList.GetComponent<TextMeshProUGUI>().text = textRewardList.GetComponent<TextMeshProUGUI>().text + reward.name + "\n";
            }
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
