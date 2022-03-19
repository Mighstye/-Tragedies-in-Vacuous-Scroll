using System;
using DialogueSystem;
using UI;
using UnityEngine;

namespace BossBehaviour
{
    public class StageInitializer : MonoBehaviour
    {
        public static StageInitializer instance { get; private set; }
        [SerializeField]private BossBehaviourSystemProxy proxyRef;
        [SerializeField]private DialogueSystemManager dialogueSystemManagerRef;
        [SerializeField]private BossNameUI bossNameUIRef;
        [SerializeField]private BossSpellAnim bossSpellAnimRef;
        [SerializeField]private BossPhaseCountUI phaseCountUIRef;
        [SerializeField]private GameObject bossObjectRef;

        private void Awake()
        {
            instance ??= this;
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void Init(BossAsset bossAsset, bool createNew = false)
        {
            if(createNew)bossObjectRef=Instantiate(bossAsset.bossPrefab);
            proxyRef.bossController = bossObjectRef.GetComponent<BossController>();
            dialogueSystemManagerRef.AssignUI(bossAsset);
            bossNameUIRef.UpdateBossName(bossAsset.characterID);
            bossSpellAnimRef.Init(bossAsset.spellSprite);
            phaseCountUIRef.Init(bossAsset.spellCount);
        }
    }
}