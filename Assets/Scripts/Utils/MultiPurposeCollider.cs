using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletSystem;

namespace Utils
{
    public class MultiPurposeCollider : MonoBehaviour
    {
        public static MultiPurposeCollider instance { get; private set; }
        public new Collider2D collider { get; private set; }
        protected List<Bullet> bullets = new List<Bullet>();

        private void Awake()
        {
            instance ??= this;
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            collider = gameObject.GetComponent<Collider2D>();
            this.gameObject.SetActive(false);
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