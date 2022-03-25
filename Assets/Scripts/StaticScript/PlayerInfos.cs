using System.Collections.Generic;
using CardSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace PlayerInfosAPI
{
    public class PlayerInfos : Singleton<PlayerInfos>
    {
        public List<GameObject> unlockedActiveCard;
        public List<GameObject> unlockedPassiveCard;
        public List<GameObject> selectedActiveCard;
        public List<GameObject> selectedPassiveCard;
        public CardList cardList;

        private void Start()
        {
            unlockedActiveCard = new List<GameObject>();
            unlockedPassiveCard = new List<GameObject>();
            selectedActiveCard = new List<GameObject>();
            selectedPassiveCard = new List<GameObject>();
            UnlockC(cardList.active[0]);
            Select(cardList.active[0]);
        }

        public void UnlockC(GameObject c)
        {
            if (c.GetComponent<ActiveCard>() != null)
                if (!unlockedActiveCard.Contains(c))
                    unlockedActiveCard.Add(c);
            if (c.GetComponent<PassiveCard>() == null) return;
            if (!unlockedPassiveCard.Contains(c))
                unlockedPassiveCard.Add(c);
        }

        public void LockC(GameObject c)
        {
            if (c.GetComponent<ActiveCard>() != null)
                unlockedActiveCard.Remove(c);
            if (c.GetComponent<PassiveCard>() != null)
                unlockedPassiveCard.Remove(c);
        }

        public void Select(GameObject c)
        {
            if (c.GetComponent<ActiveCard>() != null)
                if (!selectedActiveCard.Contains(c))
                    selectedActiveCard.Add(c);
            if (c.GetComponent<PassiveCard>() != null)
                if (!selectedPassiveCard.Contains(c))
                    selectedPassiveCard.Add(c);
        }

        public void Unselect(GameObject c)
        {
            if (c.GetComponent<ActiveCard>() != null)
                selectedActiveCard.Remove(c);
            if (c.GetComponent<PassiveCard>() != null)
                selectedPassiveCard.Remove(c);
        }
    }
}