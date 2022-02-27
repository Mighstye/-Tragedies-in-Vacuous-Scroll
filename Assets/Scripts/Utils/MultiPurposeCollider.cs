using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletSystem;

namespace Utils
{
    public class MultiPurposeCollider : MonoBehaviour
    {
        private Collider2D collider;
        protected List<Bullet> bullets = new List<Bullet>();

        private void Start()
        {
            collider = gameObject.GetComponentInChildren<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("EnemyBullet")) return;
            var bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullets.Add(bullet);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("EnemyBullet")) return;
            var bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullets.Remove(bullet);
            }
        }

        public List<Bullet> Get()
        {
            return bullets;
        }
    }
}