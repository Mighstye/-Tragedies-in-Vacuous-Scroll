using System;
using UnityEngine;
using UnityEngine.VFX;

namespace VFX
{
    public class BulletDeathVFX : MonoBehaviour
    {
        private VisualEffect vfx;

        private float delay = 1f;
        private float delayTimer;
        public Action onEffectEnd;
        private void Start()
        {
            vfx = GetComponent<VisualEffect>();
        }

        private void Update()
        {
            if (delayTimer > 0)
            {
                delayTimer -= Time.deltaTime;
                return;
            }
            if (vfx.aliveParticleCount > 0) return;
            onEffectEnd?.Invoke();
        }

        private void OnEnable()
        {
            if (vfx == null)
            {
                vfx = GetComponent<VisualEffect>();
            }

            delayTimer = delay;
            vfx.Play();
        }

        private void OnDisable()
        {
            vfx.Reinit();
        }
    }
}