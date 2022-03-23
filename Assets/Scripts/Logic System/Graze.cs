using System;
using UnityEngine;

namespace Logic_System
{
    public class Graze : MonoBehaviour
    {
        public int defaultGrazeGain;
        public int grazeSegments;
        public int maxGraze;
        private int graze;

        public Action onNeedGrazeRefresh { get; set; }

        public int get()
        {
            return graze;
        }

        public float GetSegment()
        {
            return graze / (maxGraze / (float)grazeSegments);
        }

        public bool AddGraze(int g)
        {
            if (graze == maxGraze) return false;

            if (graze + g > maxGraze)
            {
                graze = maxGraze;
                onNeedGrazeRefresh?.Invoke();
                return true;
            }

            graze += g;
            onNeedGrazeRefresh?.Invoke();
            return true;
        }

        public bool AddGraze()
        {
            return AddGraze(defaultGrazeGain);
        }

        public bool UseGraze(int nbSegments = 1)
        {
            var g = maxGraze / grazeSegments * nbSegments;
            if (graze - g < 0) return false;

            graze -= g;
            onNeedGrazeRefresh?.Invoke();
            return true;
        }
    }
}