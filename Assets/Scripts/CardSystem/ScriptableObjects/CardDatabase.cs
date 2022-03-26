using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardSystem
{
    [CreateAssetMenu(fileName = "Card Database", menuName = "Card/Card Database", order = 0)]
    public class CardDatabase : ScriptableObject
    {
        public List<GameObject> database;
    }
    
   
}