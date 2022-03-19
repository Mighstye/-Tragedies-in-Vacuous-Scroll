using System.Collections;
using System.Collections.Generic;
using System;
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

        public GameObject boss;
        public GameObject textRewardList;
        public GameObject activeCard;
        public GameObject passiveCard;
        public Action onWin;
        public Action onLoose;
        public GameObject ActiveCardManagerGameObject;
        public GameObject PassiveCardManagerGameObject;
        public GameObject SelectedCardControl;
        public GameObject ActiveCardSelection_ButtonSection;
        public GameObject PassiveCardSelection_ButtonSection;
        public GameObject CardButtonPrefab;

        private UISelectedCardControl UISCC;
        private ActiveCardManager ACM;
        private PassiveCardManager PCM;
        private string nextFightName;
        private string currentFightName;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
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
            Time.timeScale = 0f;
            boss.SetActive(false);
            ActiveBulletManager.instance.Wipe();
            if (victory) onWin?.Invoke();
            else onLoose?.Invoke();

            var rewards = generateReward();
            foreach(GameObject c in rewards)
            {
                PlayerInfos.instance.unlockC(c);
            }
            generateArrangeCardSection();
            
        }

        public List<GameObject> generateReward()
        {
            //Rewards
            List<GameObject> rewards = LogicSystemAPI.instance.rewardSystem.getReward();
            foreach (GameObject reward in rewards)
            {
                textRewardList.GetComponent<TextMeshProUGUI>().text = textRewardList.GetComponent<TextMeshProUGUI>().text + reward.name + "\n";
            }
            return rewards;
        }

        public void generateArrangeCardSection()
        {
            int index = 0;
            foreach(GameObject obj in PlayerInfos.instance.UnlockedActiveCard)
            {
                GameObject newObj = Instantiate(CardButtonPrefab, ActiveCardSelection_ButtonSection.transform, false);
                newObj.transform.position += new Vector3(0, -0.5f*index, 0);
                CardButtonScript script = newObj.GetComponent<CardButtonScript>();
                script.associatedGameObject = obj;
                index++;
            }
            index = 0;
            foreach (GameObject obj in PlayerInfos.instance.UnlockedPassiveCard)
            {
                GameObject newObj = Instantiate(CardButtonPrefab, PassiveCardSelection_ButtonSection.transform, false);
                newObj.transform.position += new Vector3(0, -0.5f*index, 0);
                CardButtonScript script = newObj.GetComponent<CardButtonScript>();
                script.associatedGameObject = obj;
                index++;
            }
        }

        public void selectCard(GameObject obj)
        {
            PlayerInfos.instance.select(obj);
        }

        public void unSelectCard(GameObject obj)
        {
            PlayerInfos.instance.unselect(obj);
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
