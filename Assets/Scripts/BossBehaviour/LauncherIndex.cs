using System;
using System.Collections.Generic;
using BulletSystem;
using UnityEngine;
using UnityEngine.XR.WSA;

namespace BossBehaviour
{
    public class LauncherIndex : MonoBehaviour
    {
        [SerializeField] public List<BulletLauncher> launchers;

        private void OnEnable()
        {
            DisableAllLaunchers();
        }

        private void OnDisable()
        {
            DisableAllLaunchers();
        }

        private void DisableAllLaunchers()
        {
            foreach (var launcher in launchers)
            {
                launcher.gameObject.SetActive(false);
            }
        }
    }
    
}