using System;
using BossBehaviour;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BossTracker : MonoBehaviour
    {
        private BossController bossControllerRef;
        private void Start()
        {
            bossControllerRef = BossBehaviourSystemProxy.instance.bossController;
        }

        private void Update()
        {
            var transform1 = transform;
            var position = transform1.position;
            position = new Vector3(bossControllerRef.transform.position.x, position.y,
                position.z);
            transform1.position = position;
        }
    }
}