using System.Collections.Generic;
using BossBehaviour;
using Logic_System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.TopUIPanel
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class BossPhaseCountUI : MonoBehaviour
    {
        [SerializeField] private int testPhaseCount;

        [SerializeField] private GameObject bossPhaseIconPrefab;

        [SerializeField] private int phasePointer;
        private BattleOutcome battleOutcomeRef;

        private List<GameObject> icons;


        private void Start()
        {
            Init(testPhaseCount);
            battleOutcomeRef = LogicSystemAPI.instance.battleOutcome;
            battleOutcomeRef.onPhaseStart += (s, phaseStatistics) => { ConsumePhase(s); };
        }

        public void InitWithAsset(BossAsset asset)
        {
            Init(asset.spellCount);
        }

        private void Init(int bossPhaseCount)
        {
            icons ??= new List<GameObject>();
            phasePointer = bossPhaseCount - 1;
            if (icons.Count < bossPhaseCount)
                for (var i = icons.Count; i < bossPhaseCount; i++)
                {
                    var icon = Instantiate(bossPhaseIconPrefab, transform);
                    icons.Add(icon);
                }

            for (var i = 0; i <= phasePointer; i++) icons[i].SetActive(true);
        }

        private void ConsumePhase(string phaseName)
        {
            if (phaseName is null) return;
            if (phasePointer <= 0) return;
            phasePointer--;
            icons[phasePointer].SetActive(false);
        }
    }
}