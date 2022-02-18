using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic_System
{
    public class Graze
    {
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
            if (graze == maxGraze)
            {
                return false;
            }

            if (graze + g > maxGraze)
            {
                graze = maxGraze;
                LogicSystemAPI.instance.onNeedGrazeRefresh?.Invoke();
                return true;
            }

            graze += g;
            LogicSystemAPI.instance.onNeedGrazeRefresh?.Invoke();
            return true;
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
            LogicSystemAPI.instance.onNeedGrazeRefresh?.Invoke();
            return true;
        }
    }
}
