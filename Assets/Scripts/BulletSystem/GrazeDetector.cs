using Logic_System;
using UnityEngine;
using Utils.Events;

namespace BulletSystem
{
    public class GrazeDetector : MonoBehaviour
    {
        private Graze grazeRef;
        [SerializeField] private GameEvent onGraze;

        private void Start()
        {
            grazeRef = LogicSystemAPI.instance.graze;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("EnemyBullet")) return;
            var bullet = col.gameObject.GetComponent<Bullet>();
            if (bullet == null)
            {
                Debug.LogWarning("Found bullet without Bullet script component!");
                return;
            }

            if (!bullet.grazeable) return;
            bullet.grazeable = false;

            if (grazeRef.AddGraze())
            {
                onGraze.Invoke();
                //TODO: Call graze VFX here
            }
        }
    }
}