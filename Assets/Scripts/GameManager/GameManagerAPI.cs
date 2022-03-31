using System;
using System.Collections.Generic;
using BossBehaviour;
using BulletSystem;
using CardSystem;
using Logic_System;
using PlayerInfosAPI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Utils.Events;

namespace GameManager
{
    public class GameManagerAPI : Singleton<GameManagerAPI>
    {
        //public GameObject activeCard;
        //public GameObject passiveCard;
        //public List<Card> rewards
        //private GameObject boss;
        //private CardSystemManager cardSystemManagerRef;
        private string currentFightName;
        public GameEvent onZeroLife;
        public GameEvent onBossDefeat;

        private void Start()
        {
            //boss ??= BossBehaviourSystemProxy.instance.bossController.gameObject;
            //cardSystemManagerRef = CardSystemManager.instance;
            //SetActive for cards selected by the player
            /*
            foreach (var obj in PlayerInfos.instance.selectedActiveCard)
            {
                Debug.Log("New Card instance " + obj.name);
                var newObj = Instantiate(obj, activeCard.transform, false);
                CardSystemManager.instance.activeCardManager.Add(newObj.GetComponent<ActiveCard>());
                newObj.SetActive(true);
            }

            foreach (var obj in PlayerInfos.instance.selectedPassiveCard)
            {
                Debug.Log("New Card instance " + obj.name);
                var newObj = Instantiate(obj, passiveCard.transform, false);
                cardSystemManagerRef.passiveCardManager.Add(newObj.GetComponent<PassiveCard>());
                newObj.SetActive(true);
            }
            */
            
            currentFightName = SceneManager.GetActiveScene().name;
            // ^ This get the next scene name for the next fight
        }

        [Obsolete("Refactored",true)]
        public void EndFight(bool victory)
        {
            //Time.timeScale = 0f;
            //boss.SetActive(false);
            if (victory)
            {
                onBossDefeat.Invoke();
            }
            else onZeroLife.Invoke();

            //rewards = GenerateReward();
        }

        [Obsolete("Refactored",true)]
        public List<Card> GenerateReward()
        {
            //Rewards
            var reward = LogicSystemAPI.instance.rewardSystem.GetReward();
            return reward;
        }

        public void SelectCard(GameObject obj)
        {
            PlayerInfos.instance.Select(obj);
        }

        public void UnSelectCard(GameObject obj)
        {
            PlayerInfos.instance.Unselect(obj);
        }

        public void UnlockCard(GameObject obj)
        {
            PlayerInfos.instance.UnlockC(obj);
        }

        public void LockCard(GameObject obj)
        {
            PlayerInfos.instance.LockC(obj);
        }

        public void NextFight()
        {
            SceneManager.LoadScene(currentFightName + " 1");
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