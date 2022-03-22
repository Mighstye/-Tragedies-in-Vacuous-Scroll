using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic_System
{
    public class RewardSystem : MonoBehaviour
    {
        private BattleOutcome stats;

        public CardDropDeck dropTable;

        public GameObject bonusCardNoSpell;
        public GameObject bonusCardNoHit;

        private void Start()
        {
            stats = LogicSystemAPI.instance.battleOutcome;
        }

        public List<GameObject> getReward()
        {
            var random = new System.Random();
            var reward1 = dropTable.dropDeck[random.Next(dropTable.dropDeck.Count)];
            var reward2 = dropTable.dropDeck[random.Next(dropTable.dropDeck.Count)];
            var rewards = new List<GameObject>
            {
                reward1,
                reward2
            };
            bool hit = false;
            bool spell = false;
            foreach(KeyValuePair<string, PhaseStatistics> entry in stats.GetAllStatistics())
            {
                if (entry.Value.hit == true) hit = true;
                else if (entry.Value.spellUse == true) spell = true;
            }
            if (!hit) rewards.Add(bonusCardNoHit);
            if (!spell) rewards.Add(bonusCardNoSpell);
            return rewards;
        }
    }
}