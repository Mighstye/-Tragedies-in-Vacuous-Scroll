using System.Collections.Generic;
using CardSystem;
using UnityEngine;
using Utils;

namespace PlayerInfosAPI
{
    public class PlayerInfos : Singleton<PlayerInfos>
    {
        public List<GameObject> UnlockedActiveCard;
        public List<GameObject> UnlockedPassiveCard;
        public List<GameObject> SelectedActiveCard;
        public List<GameObject> SelectedPassiveCard;
        public CardList cardList;

        private void Start()
        {
            UnlockedActiveCard = new List<GameObject>();
            UnlockedPassiveCard = new List<GameObject>();
            SelectedActiveCard = new List<GameObject>();
            SelectedPassiveCard = new List<GameObject>();
            unlockC(cardList.active[0]);
            select(cardList.active[0]);
        }

        public void unlockC(GameObject c)
        {
            if (c.GetComponent<ActiveCard>() != null)
                if (!UnlockedActiveCard.Contains(c))
                    UnlockedActiveCard.Add(c);
            if (c.GetComponent<PassiveCard>() != null)
                if (!UnlockedPassiveCard.Contains(c))
                    UnlockedPassiveCard.Add(c);
        }

        public void lockC(GameObject c)
        {
            if (c.GetComponent<ActiveCard>() != null)
                UnlockedActiveCard.Remove(c);
            if (c.GetComponent<PassiveCard>() != null)
                UnlockedPassiveCard.Remove(c);
        }

        public void select(GameObject c)
        {
            if (c.GetComponent<ActiveCard>() != null)
                if (!SelectedActiveCard.Contains(c))
                    SelectedActiveCard.Add(c);
            if (c.GetComponent<PassiveCard>() != null)
                if (!SelectedPassiveCard.Contains(c))
                    SelectedPassiveCard.Add(c);
        }

        public void unselect(GameObject c)
        {
            if (c.GetComponent<ActiveCard>() != null)
                SelectedActiveCard.Remove(c);
            if (c.GetComponent<PassiveCard>() != null)
                SelectedPassiveCard.Remove(c);
        }
    }
}