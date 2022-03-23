using DialogueSystem;
using UI;
using UI.TopUIPanel;
using UnityEngine;
using Utils;

namespace BossBehaviour
{
    public class StageInitializer : Singleton<StageInitializer>
    {
        [SerializeField] private BossBehaviourSystemProxy proxyRef;
        [SerializeField] private DialogueSystemManager dialogueSystemManagerRef;
        [SerializeField] private BossNameUI bossNameUIRef;
        [SerializeField] private BossSpellAnim bossSpellAnimRef;
        [SerializeField] private BossPhaseCountUI phaseCountUIRef;
        [SerializeField] private GameObject bossObjectRef;

        public void Init(BossAsset bossAsset, bool createNew = false)
        {
            if (createNew) bossObjectRef = Instantiate(bossAsset.bossPrefab);
            proxyRef.bossController = bossObjectRef.GetComponent<BossController>();
            dialogueSystemManagerRef.AssignUI(bossAsset);
            bossNameUIRef.UpdateBossName(bossAsset.characterID);
            bossSpellAnimRef.Init(bossAsset.spellSprite);
            phaseCountUIRef.Init(bossAsset.spellCount);
        }
    }
}