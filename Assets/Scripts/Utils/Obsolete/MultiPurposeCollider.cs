using System;
using System.Collections.Generic;
using BulletSystem;
using UnityEngine;

namespace Utils
{
    [Obsolete]
    public class MultiPurposeCollider : Singleton<MultiPurposeCollider>
    {
        protected List<Bullet> bullets = new();
        public new Collider2D collider { get; private set; }

        private void Start()
        {
            collider = gameObject.GetComponent<Collider2D>();
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("EnemyBullet")) return;
            var bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null) bullets.Add(bullet);
        }

        /*
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("EnemyBullet")) return;
            var bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullets.Remove(bullet);
            }
        }
        */

        public void ClearList()
        {
            bullets.Clear();
        }

        public List<Bullet> Get()
        {
            return bullets;
        }
    }
}