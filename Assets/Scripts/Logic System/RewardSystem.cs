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

        public string DrawCard(DropDeckType type)
        {
            return dropDecks.ContainsKey(type) ? dropDecks[type].PopFirst() : null;
        }

        public void Reinsert(string cardID)
        {
            foreach (var type in dropDeckFeed.Select(item => item.TryGetTypeFromCard(cardID)).
                         Where(type => type is not DropDeckType.NullType))
            {
                dropDecks[type].Add(cardID);
            }
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
                CardFactory.instance.Make(dropDecks[DropDeckType.NormalActive].PopFirst()),
                CardFactory.instance.Make(dropDecks[DropDeckType.NormalPassive].PopFirst())
            };
            return GenerateSpecialReward(list);
        }

        private List<Card> GenerateSpecialReward(List<Card> list)
        {
            if (stats.HitCount() < 1)
            {
                var cardKey = dropDecks[DropDeckType.Special1].PopFirst();
                list.Add(CardFactory.instance.Make(cardKey));
            }

            if (stats.HitCount() < 1 && stats.SpellUseCount() < 1)
            {
                var cardKey = dropDecks[DropDeckType.Special2].PopFirst();
                list.Add(CardFactory.instance.Make(cardKey));
            }

            return list;
        }
    }
}