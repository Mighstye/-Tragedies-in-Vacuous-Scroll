using System.Collections.Generic;
using CardSystem;
using UnityEngine;

namespace BossBehaviour
{
    public class TempSpecialCard : MonoBehaviour
    {
        [SerializeField] private List<ActiveCard> specialActiveCards;
        private ActiveCardManager activeCardManagerRef;

        private void Start()
        {
            activeCardManagerRef = CardSystemManager.instance.activeCardManager;
            // Activation de la special card
            foreach (var card in specialActiveCards) activeCardManagerRef.Add(Instantiate(card), PoolType.Extra);
            activeCardManagerRef.SwapActivePool(PoolType.Extra);
        }
    }
}