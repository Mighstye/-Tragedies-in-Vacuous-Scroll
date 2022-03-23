using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    public enum PoolType
    {
        Normal,
        Extra
    }

    [Serializable]
    public class ActiveCardManager : MonoBehaviour
    {
        internal Action onSelectedCardChangeInternal;
        private Dictionary<PoolType, List<ActiveCard>> labeledPools;
        [SerializeField] private Transform cardContainer;
        public ActiveCard selectedCard;
        private PoolType currentActivatedPoolType = PoolType.Normal;
        private int selectedCardIndex;
        

        private void Start()
        {
            InitializeDict();
            if (labeledPools[PoolType.Normal].Count <= 0) return;
            selectedCardIndex = 0;
            selectedCard = labeledPools[PoolType.Normal][selectedCardIndex];
        }

        private void InitializeDict()
        {
            labeledPools ??= new Dictionary<PoolType, List<ActiveCard>>();
            foreach (PoolType poolType in Enum.GetValues(typeof(PoolType)))
            {
                if (!labeledPools.ContainsKey(poolType))
                {
                    labeledPools.Add(poolType,new List<ActiveCard>());
                }
            }
            
        }

        public void Add(ActiveCard activeCard, PoolType poolType = PoolType.Normal)
        {
            labeledPools[poolType].Add(activeCard);
            activeCard.gameObject.transform.SetParent(cardContainer,worldPositionStays:false) ;
            if (selectedCard is null)
            {
                SelectNext();
            }
        }

        public void Remove(ActiveCard activeCard, PoolType poolType = PoolType.Normal)
        {
            labeledPools[poolType].Remove(activeCard);
        }

        public void Clear(PoolType poolType)
        {
            labeledPools[poolType].Clear();
        }

        public void SetActiveStateAll(bool state, PoolType poolType = PoolType.Normal)
        {
            foreach (var card in labeledPools[poolType])
            {
                card.gameObject.SetActive(state);
            }
        }

        public void Arrange(PoolType poolType=PoolType.Normal)
        {
            var index = 0;
            foreach (var card in labeledPools[poolType])
            {
                card.transform.SetSiblingIndex(index);
                index++;
            }
        }

        public void SwapActivePool(PoolType poolType)
        {
            currentActivatedPoolType = poolType;
            foreach (var pool in labeledPools.Keys)
            {
                SetActiveStateAll(pool==poolType,pool);
            }

            selectedCardIndex = 0;
            selectedCard = labeledPools[currentActivatedPoolType][selectedCardIndex];
            onSelectedCardChangeInternal?.Invoke();
        }

        public void SelectNext()
        {
            if (labeledPools[currentActivatedPoolType].Count < 1) return;
            selectedCardIndex++;
            if (selectedCardIndex >= labeledPools[currentActivatedPoolType].Count) selectedCardIndex = 0;
            selectedCard = labeledPools[currentActivatedPoolType][selectedCardIndex];
            onSelectedCardChangeInternal?.Invoke();
        }

        public void RunTest()
        { 
            //TODO: following code is for testing, delete the fragment afterwards
            if (labeledPools is null)
            {
                InitializeDict();
            }
            var cards = cardContainer.gameObject.GetComponentsInChildren<ActiveCard>();
            foreach (var card in cards)
            {
                labeledPools[PoolType.Normal].Add(card);
            }
            if (labeledPools[PoolType.Normal].Count <= 0) return;
            selectedCardIndex = 0;
            selectedCard = labeledPools[PoolType.Normal][selectedCardIndex];
            Debug.Log(selectedCard.gameObject.name);
        }
        
        
    }
}