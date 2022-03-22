using System.Collections.Generic;
using BulletSystem;
using CardSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace BossBehaviour
{
    public class TempSpecialCard : MonoBehaviour
    { 
        private ActiveCardManager activeCardManagerRef;
        [SerializeField] private List<ActiveCard> specialActiveCards;
        private void Start()
        {
            activeCardManagerRef = CardSystemManager.instance.activeCardManager;
            // Activation de la special card
            foreach (var card in specialActiveCards)
            {
                activeCardManagerRef.Add(Instantiate(card),PoolType.Extra);
            }
            activeCardManagerRef.SwapActivePool(PoolType.Extra);
        }
    }
}
