using System.Collections.Generic;
using UnityEngine;

namespace CardSystem.DataContainers
{
    [CreateAssetMenu(fileName = "Card Database", menuName = "Card/Card Database", order = 0)]
    public class CardDatabase : ScriptableObject
    {
        public List<GameObject> database;
    }
    
   
}