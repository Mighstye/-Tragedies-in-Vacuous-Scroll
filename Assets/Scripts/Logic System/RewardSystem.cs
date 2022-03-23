using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Logic_System
{
    public class RewardSystem : MonoBehaviour
    {
        public CardDropDeck dropTable;

        public GameObject bonusCardNoSpell;
        public GameObject bonusCardNoHit;
        private BattleOutcome stats;

        private void Start()
        {
            stats = LogicSystemAPI.instance.battleOutcome;
        }

        public List<GameObject> getReward()
        {
            var random = new Random();
            var reward1 = dropTable.dropDeck[random.Next(dropTable.dropDeck.Count)];
            var reward2 = dropTable.dropDeck[random.Next(dropTable.dropDeck.Count)];
            var rewards = new List<GameObject>
            {
                reward1,
                reward2
            };
            var hit = false;
            var spell = false;
            foreach (var entry in stats.GetAllStatistics())
                if (entry.Value.hit) hit = true;
                else if (entry.Value.spellUse) spell = true;
            if (!hit) rewards.Add(bonusCardNoHit);
            if (!spell) rewards.Add(bonusCardNoSpell);
            return rewards;
        }
    }
}