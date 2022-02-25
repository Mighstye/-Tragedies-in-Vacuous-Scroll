using System;
using System.Collections.Generic;
using BulletSystem;
using UnityEngine;
using UnityEngine.WSA;

namespace BossBehaviour
{
    public class LauncherIndex : MonoBehaviour
    {
        [SerializeField] public List<BulletLauncher> launchers;

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            foreach (var launcher in launchers)
            {
                launcher.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (animator.enabled) return;
            foreach (var launcher in launchers)
            {
                launcher.gameObject.SetActive(false);
            }
        }
    }
    
}