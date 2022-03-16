using System;
using System.Collections.Generic;
using Ink.Parsed;
using Logic_System;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class BossPhaseCountUI : MonoBehaviour
    {
        [SerializeField] private int testPhaseCount;
        private BattleOutcome battleOutcomeRef;
        
        [SerializeField] private GameObject bossPhaseIconPrefab;

        private List<GameObject> icons;

        [SerializeField]private int phasePointer;
        
        
        private void Start()
        {
            Init(testPhaseCount);
            battleOutcomeRef = LogicSystemAPI.instance.battleOutcome;
            battleOutcomeRef.onPhaseEnd += (s,phaseStatistics)=>
            {
                PassPhase();
            };
        }

        public void Init(int bossPhaseCount)
        {
            icons ??= new List<GameObject>();
            phasePointer = bossPhaseCount - 1;
            if (icons.Count < bossPhaseCount)
            {
                for (var i = icons.Count; i < bossPhaseCount; i++)
                {
                    var icon = Instantiate(bossPhaseIconPrefab, this.transform);
                    icons.Add(icon);
                }
            }

            for (var i = 0; i <= phasePointer; i++)
            {
                icons[i].SetActive(true);
            }
        }

        private void PassPhase()
        {
            if (phasePointer <= 0) return;
            phasePointer--;
            icons[phasePointer].SetActive(false);
        }
        
    }
}