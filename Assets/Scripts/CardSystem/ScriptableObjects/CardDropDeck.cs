using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DropDeck", menuName = "DropDeck", order = 1)]
public class CardDropDeck : ScriptableObject
{
    public List<string> dropDeck;

    public List<string> ToDynamicList()
    {
        return new List<string>(dropDeck);
    }
}