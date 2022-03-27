using System;
using BulletSystem;
using DG.Tweening;
using UI;
using UnityEngine;
using YaotomeBehaviour;

namespace BossBehaviour
{
    public abstract class BossController : MonoBehaviour
    {
        public delegate void BossMotion();

        public int currentPhaseMaxHp;
        [SerializeField] private int currentHp;
        [SerializeField] public Animator phaseFlow;
        public AnimationLib animationLib;
        public Sequence motionSequence;
        private Animator animator;
        private Collider2D hitBox;
        private bool hpDepleted;

        public Action onHpDepleted;

        public BossMotion bossMotion { get; set; }

        private void Start()
        {
            animationLib = new YaotomeAnimLib();
            animator = GetComponent<Animator>();
            hitBox = GetComponent<Collider2D>();
            AssignAnimationLib();
            animationLib.animator = animator;
        }

        private void Update()
        {
            bossMotion?.Invoke();
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("FriendlyBullet")) return;
            var bullet = col.gameObject.GetComponent<ISimpleParry>();
            TakeDamage(bullet.damage);
            ((Bullet)bullet).InvokeBulletDeath();
        }

        protected abstract void AssignAnimationLib();


        public void SetUpHp(int maxHp)
        {
            hpDepleted = false;
            currentPhaseMaxHp = maxHp;
            currentHp = maxHp;
            PhaseHP.instance.SetGaugeFill(maxHp == 0 ? 0f : 1f);
        }

        private void TakeDamage(int amount)
        {
            if (hpDepleted) return;
            if (currentHp <= 0) return;
            currentHp -= amount;
            PhaseHP.instance.SetGaugeFill(currentHp / (float)currentPhaseMaxHp);
            if (currentHp > 0) return;
            hpDepleted = true;
            onHpDepleted?.Invoke();
        }
    }
}