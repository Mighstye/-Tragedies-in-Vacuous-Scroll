using BossBehaviour;
using Logic_System;
using UnityEngine;

namespace UI
{
    public class BossSpellAnim : MonoBehaviour
    {
        [SerializeField] private Sprite bossSprite;

        private Animation anim;

        private BattleOutcome battleOutcomeRef;

        private void Start()
        {
            anim = GetComponent<Animation>();
            battleOutcomeRef = LogicSystemAPI.instance.battleOutcome;
            battleOutcomeRef.onPhaseStart += (s, statistics) =>
            {
                if (s is null) return;
                anim.Play();
            };
        }

        public void Init(BossAsset asset)
        {
            bossSprite = asset.spellSprite;
        }
    }
}