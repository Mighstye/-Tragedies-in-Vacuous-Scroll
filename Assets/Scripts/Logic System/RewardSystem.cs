using System;
using System.Collections.Generic;
using System.Linq;
using CardSystem;
using UnityEngine;
using Utils;
using Random = System.Random;

namespace Logic_System
{
    #region componentClasses
    public enum DropDeckType
    {
        NullType,
        NormalActive,
        NormalPassive,
        Special1,
        Special2
    }

    [Serializable]
    public struct DropDeckFeedItem
    {
        [SerializeField] public DropDeckType type;
        [SerializeField] public CardDropDeck deck;

        public DropDeckType TryGetTypeFromCard(string card)
        {
            return deck.dropDeck.Contains(card) ? type : DropDeckType.NullType;
        }
    }
    
    #endregion
    public class RewardSystem : MonoBehaviour
    {
        public List<DropDeckFeedItem> dropDeckFeed;
        private Dictionary<DropDeckType, List<string>> dropDecks;
        private BattleOutcome stats;

        private void Start()
        {
            stats = LogicSystemAPI.instance.battleOutcome;
            InitializeDecks();
        }
        
        #region deckControl

        private void InitializeDecks()
        {
            dropDecks ??= new Dictionary<DropDeckType, List<string>>();
            foreach (var feed in dropDeckFeed)
            {
                dropDecks.Add(feed.type,feed.deck.ToDynamicList().Shuffle());
            }
        }

        public void Reshuffle(DropDeckType type)
        {
            if (dropDecks.ContainsKey(type))
            {
                dropDecks[type].Shuffle();
            }
        }

        public void ReshuffleAll()
        {
            var types = dropDecks.Keys;
            foreach (var type in types)
            {
                dropDecks[type].Shuffle();
            }
        }

        public void ReplaceDeck(DropDeckType type, CardDropDeck replacement)
        {
            if (dropDecks.ContainsKey(type))
            {
                dropDecks[type] = replacement.ToDynamicList().Shuffle();
            }
        }

        private string PeekCard(DropDeckType type)
        {
            return dropDecks.ContainsKey(type) ? dropDecks[type][0] : null;
        }

        public string DrawCard(DropDeckType type)
        {
            return dropDecks.ContainsKey(type) ? dropDecks[type].PopFirst() : null;
        }

        public void RemoveCard(string cardID)
        {
            foreach (var type in dropDeckFeed.Select(item => item.TryGetTypeFromCard(cardID)).
                         Where(type => type is not DropDeckType.NullType))
            {
                dropDecks[type].Remove(cardID);
            }
        }

        public void Reinsert(string cardID)
        {
            foreach (var type in dropDeckFeed.Select(item => item.TryGetTypeFromCard(cardID)).
                         Where(type => type is not DropDeckType.NullType))
            {
                dropDecks[type].Add(cardID);
            }
        }

        private IEnumerable<string> PeekMultiple(DropDeckType type, int amount)
        {
            if (!dropDecks.ContainsKey(type)) return null;
            var list = new List<string>();
            amount = Mathf.Min(dropDecks[type].Count,amount);
            for (var i = 0; i < amount; i++)
            {
                list.Add(dropDecks[type].PeekAndTuck());
            }
            return list;
        }
        
        #endregion

        public List<Card> GetReward()
        {
            /*var random = new Random();
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
            return rewards;*/
            var list = new List<Card>
            {
                GenerateReward(DropDeckType.NormalActive,()=>true),
                GenerateReward(DropDeckType.NormalPassive,()=>true),
            };
            return GenerateSpecialReward(list);
        }

        private Card GenerateReward(DropDeckType type, Func<bool> condition)
        {
            return condition() ? CardFactory.instance.Make(PeekCard(type)) : null;
        }

        private List<Card> GenerateRewardMultiple(DropDeckType type, int amount, Func<bool> condition)
        {
            var list = new List<Card>();
            if (!condition()) return list;
            var keys = PeekMultiple(type, amount);
            list.AddRange(keys.Select(key => CardFactory.instance.Make(key)));
            return list;
        }
        
        private List<Card> GenerateSpecialReward(List<Card> list)
        {
            list.Add(GenerateReward(DropDeckType.Special1, 
                () => stats.HitCount() < 1));
            list.Add(GenerateReward(DropDeckType.Special2, 
                () => stats.HitCount() < 1 && stats.SpellUseCount()<1));
            return list;
        }
    }
}