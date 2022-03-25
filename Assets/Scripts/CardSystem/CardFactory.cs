using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace CardSystem
{
    [Serializable]
    public struct CardDatabaseEntry
    {
        [SerializeField]public string key;
        [SerializeField] public Card prefab;
    }
    public class CardFactory : Singleton<CardFactory>
    {
        [SerializeField] private CardDatabase databaseSource;
        [SerializeField] private List<CardDatabaseEntry> database;
        
        public Card Make(string key)
        {
            var card = database.FirstOrDefault(o => o.key == key).prefab;
            return card is not null ? Instantiate(card).GetComponent<Card>() : null;
        }
        
        [ContextMenu("Generate database")]
        public void Generate()
        {
            database = new List<CardDatabaseEntry>();
            foreach (var entry in databaseSource.database)
            {
                var card = entry.GetComponent<Card>();
                if (card is null) continue;
                var item = new CardDatabaseEntry
                {
                    key = card.commonMetadata.key,
                    prefab = card
                };
                database.Add(item);
            }
        }
   
    }
}