using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DropDeck", menuName = "DropDeck", order = 1)]
public class CardDropDeck : ScriptableObject
{
    public List<GameObject> dropDeck;
}