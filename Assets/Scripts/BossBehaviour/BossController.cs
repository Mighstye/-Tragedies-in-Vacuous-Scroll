using System;
using System.Collections.Generic;
using UnityEngine;

namespace BossBehaviour
{
    public class BossController : MonoBehaviour
    {
        public static BossController instance { get;private set; }
        private Animator animator;
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
            animator = GetComponent<Animator>();
            hitBox = GetComponent<Collider2D>();
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