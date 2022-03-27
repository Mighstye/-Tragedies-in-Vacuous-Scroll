using System;
using Control;
using UnityEngine;
using UnityEngine.VFX;

namespace VFX
{
    public class GrazeVFX : MonoBehaviour
    {
        [SerializeField] private VisualEffect visualEffect;

        [SerializeField] private YoumuController youmuController;

        [SerializeField] private float stayTime = 1;

        private float timer;
        private void Start()
        {
            visualEffect ??= GetComponent<VisualEffect>();
        }

        public void StartEffect()
        {
            if (visualEffect.aliveParticleCount < 1)
            {
                visualEffect.Play();
            }

            timer = stayTime;
        }

        private void FixedUpdate()
        {
            timer -= Time.fixedDeltaTime;
            visualEffect.transform.position = youmuController.transform.position;
            if (timer <= 0)
            {
                visualEffect.Stop();
            }
        }
    }
}