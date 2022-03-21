using System.Collections;
using System.Collections.Generic;
using System;
using BossBehaviour;
using UnityEngine;
using UnityEngine.SceneManagement;
using BulletSystem;
using Logic_System;
using PlayerInfosAPI;
using TMPro;
using CardSystem;

namespace Game_Manager
{
    public class GameManagerAPI : MonoBehaviour
    {
        public static GameManagerAPI instance { get; private set; }

        private GameObject boss = null;
        public GameObject activeCard;
        public GameObject passiveCard;
        public Action onWin;
        public Action onLoose;
        public GameObject parriedPool;
        public GameObject ActiveCardManagerGameObject;
        public GameObject PassiveCardManagerGameObject;
        public GameObject SelectedCardControl;
        private UISelectedCardControl UISCC;
        private ActiveCardManager ACM;
        private PassiveCardManager PCM;
        public List<GameObject> rewards;
        private string currentFightName;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            boss ??= BossBehaviourSystemProxy.instance.bossController.gameObject;
            UISCC = SelectedCardControl.GetComponent<UISelectedCardControl>();
            ACM = ActiveCardManagerGameObject.GetComponent<ActiveCardManager>();
            PCM = PassiveCardManagerGameObject.GetComponent<PassiveCardManager>();
            //SetActive for cards selected by the player
            foreach (GameObject obj in PlayerInfos.instance.SelectedActiveCard)
            {
                Debug.Log("New Card instance " + obj.name);
                var newObj = Instantiate(obj, activeCard.transform, false);
                ACM.Add(newObj.GetComponent<ActiveCard>());
                ACM.SelectNext();
                UISCC.UpdateSelectedCard(newObj.GetComponent<ActiveCard>());
                newObj.SetActive(true);
            }
            foreach (GameObject obj in PlayerInfos.instance.SelectedPassiveCard)
            {
                Debug.Log("New Card instance " + obj.name);
                var newObj = Instantiate(obj, passiveCard.transform, false);
                PCM.Add(newObj.GetComponent<PassiveCard>());
                newObj.SetActive(true);
            }

            LogicSystemAPI.instance.health.onPlayerDeath += () =>
            {
                EndFight(false);
            };
            currentFightName = gameObject.scene.name;
            // ^ This get the next scene name for the next fight
        }

        public void EndFight(bool victory)
        {
            //Time.timeScale = 0f;
            boss.SetActive(false);
            ActiveBulletManager.instance.Wipe();
            if (victory) onWin?.Invoke();
            else onLoose?.Invoke();

            rewards = generateReward();
        }

        public List<GameObject> generateReward()
        {
            //Rewards
            List<GameObject> reward = LogicSystemAPI.instance.rewardSystem.getReward();
            return reward;
        }

        public void selectCard(GameObject obj)
        {
            PlayerInfos.instance.select(obj);
        }

        public void unSelectCard(GameObject obj)
        {
            PlayerInfos.instance.unselect(obj);
        }

        public void unlockCard(GameObject obj)
        {
            PlayerInfos.instance.unlockC(obj);
        }

        public void lockCard(GameObject obj)
        {
            PlayerInfos.instance.lockC(obj);
        }

        public void NextFight()
        {
            SceneManager.LoadScene(currentFightName + "1");
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
