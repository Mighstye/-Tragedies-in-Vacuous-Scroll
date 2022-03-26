using System.Collections.Generic;
using UnityEngine;

namespace CardSystem.DataContainers
{
    [CreateAssetMenu(fileName = "CardList", menuName = "Card/Card List", order = 1)]
    public class CardList : ScriptableObject
    {
        public List<GameObject> active;
        public List<GameObject> passive;
    }
}