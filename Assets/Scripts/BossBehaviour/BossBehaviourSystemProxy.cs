using System;
using UnityEngine;

namespace BossBehaviour
{
    public class BossBehaviourSystemProxy : MonoBehaviour
    {
        public static BossBehaviourSystemProxy instance { get;private set; }

        public BossController bossController;
        

        public void Awake()
        {
            instance ??= this;
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        
    }
}