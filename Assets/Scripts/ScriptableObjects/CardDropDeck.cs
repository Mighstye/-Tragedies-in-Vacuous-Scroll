using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardSystem;

[CreateAssetMenu(fileName = "DropDeck", menuName = "DropDeck", order = 1)]
public class CardDropDeck : ScriptableObject
{
    public List<GameObject> dropDeck;
}
