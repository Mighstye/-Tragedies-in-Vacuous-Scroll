using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardSystem;

[CreateAssetMenu(fileName = "DropDeck", menuName = "Scripts/ScriptableObjectsCardDropDeck", order = 1)]
public class CardDropDeck : ScriptableObject
{
    public Dictionary<string, List<Card>> dropTable;
}
