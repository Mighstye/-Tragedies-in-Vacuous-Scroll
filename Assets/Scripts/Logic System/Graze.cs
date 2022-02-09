using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graze : MonoBehaviour
{
    public static Graze instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private int graze = 0;

    public int defaultGrazeGain;
    public int grazeSegmentsNb;
    public int maxGraze;

    public int get()
    {
        return graze;
    }

    //ajoute de la graze si possible, renvoie true si la graze a été ajoutée, false sinon
    public bool AddGraze(int g)
    {
        if(graze == maxGraze)
        {
            return false;
        }

        if (graze + g > maxGraze)
        {
            graze = maxGraze;
            return true;
        }

        graze += g;
        return true;
    }
    public bool AddGraze()
    {
        return AddGraze(defaultGrazeGain);
    }

    //consomme la graze si possible, renvoie true si la graze a été consommé, false sinon
    public bool UseGraze(int nbSegments)
    {
        int g = (maxGraze / grazeSegmentsNb) * nbSegments;
        if (graze - g < 0)
        {
            return false;
        }

        graze -= g;
        return true;
    }
    public bool UseGraze()
    {
        return UseGraze(1);
    }
}
