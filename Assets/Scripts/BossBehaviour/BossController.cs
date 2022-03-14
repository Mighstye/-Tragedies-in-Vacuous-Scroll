using System;
using System.Collections.Generic;
using BulletSystem;
using UI;
using UnityEngine;
using YaotomeBehaviour;

namespace BossBehaviour
{
    public abstract class BossController : MonoBehaviour
    {
        private Animator animator;
        public AnimationLib animationLib;
        private Collider2D hitBox;
        public int currentPhaseMaxHp;
        [SerializeField] private int currentHp;
        private bool hpDepleted = false;

        public delegate void BossMotion();

        public BossMotion bossMotion { get; set; }

        public Action onHpDepleted;
        
        private void Start()
        {
            animationLib = new YaotomeAnimLib();
            animator = GetComponent<Animator>();
            hitBox = GetComponent<Collider2D>();
            AssignAnimationLib();
            animationLib.animator = animator;
        }

        protected abstract void AssignAnimationLib();

        private void Update()
        {
            bossMotion?.Invoke();
        }

        public void SetUpHp(int maxHp)
        {
            hpDepleted = false;
            currentPhaseMaxHp = maxHp;
            currentHp = maxHp;
            PhaseHP.instance.SetGaugeFill(1f);
        }

        private void TakeDamage(int amount)
        {
            if (hpDepleted) return;
            if (currentHp <= 0) return;
            currentHp -= amount;
            PhaseHP.instance.SetGaugeFill(currentHp/(float)currentPhaseMaxHp);
            if (currentHp > 0) return;
            hpDepleted = true;
            onHpDepleted?.Invoke();
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("FriendlyBullet")) return;
            var bullet = col.gameObject.GetComponent<ISimpleParry>();
            TakeDamage(bullet.damage);
            ((Bullet)bullet).InvokeBulletDeath();
        }
    }
}