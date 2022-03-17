using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic_System
{
    public class RewardSystem : MonoBehaviour
    {
        private BattleOutcome stats;

        public GameObject rewardCardOne;
        public GameObject rewardCardTwo;

        public GameObject bonusCardNoSpell;
        public GameObject bonusCardNoHit;

        private void Start()
        {
            stats = LogicSystemAPI.instance.battleOutcome;
        }

        void CreateDropTable()
        {
            
        }

        public List<GameObject> getReward()
        {
            var rewards = new List<GameObject>
            {
                rewardCardOne,
                rewardCardTwo
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