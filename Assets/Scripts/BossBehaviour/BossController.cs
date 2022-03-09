using System;
using System.Collections.Generic;
using UnityEngine;
using YaotomeBehaviour;

namespace BossBehaviour
{
    public class BossController : MonoBehaviour
    {
        public static BossController instance { get;private set; }
        private Animator animator;
        public AnimationLib animationLib;
        private Collider2D hitBox;
        public int currentPhaseMaxHp;
        [SerializeField] private int currentHp;
        private bool hpDepleted = false;

        public delegate void BossMotion();

        public BossMotion bossMotion { get; set; }

        public Action onHpDepleted;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            animationLib = new YaotomeAnimLib();
            animator = GetComponent<Animator>();
            hitBox = GetComponent<Collider2D>();
            animationLib.animator = animator;
        }

        private void Update()
        {
            bossMotion?.Invoke();
            if (hpDepleted) return;
            if (currentHp >= 0) return;
            hpDepleted = true;
            onHpDepleted?.Invoke();

        }

        public void SetUpHp(int maxHp)
        {
            hpDepleted = false;
            currentPhaseMaxHp = maxHp;
            currentHp = maxHp;
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            //TODO: use this method to decrease hp
        }
    }
}