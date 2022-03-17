using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CardSystem;
using ActiveCardImplementation;
using PassiveCardImplementation;

public static class PlayerInfos
{
    public static List<ActiveCard> UnlockedActiveCard { get; private set; }
    public static List<PassiveCard> UnlockedPassiveCard { get; private set; }
    public static List<ActiveCard> SelectedActiveCard { get; private set; }
    public static List<PassiveCard> SelectedPassiveCard { get; private set; }

    static PlayerInfos()
    {
        unlockC(new ArcParry());
        unlockC(new BeamParry());
        select(new ArcParry());
    }

    public static void unlockC(Card c)
    {
        if (c is ActiveCard) 
            if(!UnlockedActiveCard.Contains((ActiveCard)c)) UnlockedActiveCard.Add((ActiveCard)c);
        if (c is PassiveCard)
            if(!UnlockedPassiveCard.Contains((PassiveCard)c)) UnlockedPassiveCard.Add((PassiveCard)c);
    }

    public static void lockC(Card c)
    {
        if (c is ActiveCard) 
            UnlockedActiveCard.Remove((ActiveCard)c);
        if (c is PassiveCard) 
            UnlockedPassiveCard.Remove((PassiveCard)c);
    }

    public static void select(Card c)
    {
        if (c is ActiveCard) 
            if(!SelectedActiveCard.Contains((ActiveCard)c)) SelectedActiveCard.Add((ActiveCard)c);
        if (c is PassiveCard) 
            if(!SelectedPassiveCard.Contains((PassiveCard)c)) SelectedPassiveCard.Add((PassiveCard)c);
    }

    public static void unselect(Card c)
    {
        if (c is ActiveCard) 
            SelectedActiveCard.Remove((ActiveCard)c);
        if (c is PassiveCard) 
            SelectedPassiveCard.Remove((PassiveCard)c);
    }
}
