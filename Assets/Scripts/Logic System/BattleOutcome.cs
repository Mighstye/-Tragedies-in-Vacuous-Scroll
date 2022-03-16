using System;
using System.Collections.Generic;
using System.Linq;
using Control;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.Localization;

namespace Logic_System
{
    
    public class BattleOutcome: MonoBehaviour
    {
        private Spell spellRef;
        public Dictionary<string, PhaseStatistics> outcome { get; private set; }
        public string currentPhaseName { get; private set; }
        public PhaseStatistics currentPhaseStatistics { get; private set; }

        public Action<string,PhaseStatistics> onPhaseStart;
        private void Start()
        {
            outcome = new Dictionary<string, PhaseStatistics>();
            spellRef = LogicSystemAPI.instance.spell;
            spellRef.onSpellUse += RecordSpellUse;
            YoumuController.instance.onYoumuHit += RecordHealthLost;
        }

        public void RecordNewPhase(string phaseName)
        {
            currentPhaseName = phaseName;
            currentPhaseStatistics = new PhaseStatistics();
            if (outcome.ContainsKey(phaseName)) currentPhaseStatistics = outcome[phaseName];
            currentPhaseStatistics.IncreaseEncounterCount();
            onPhaseStart?.Invoke(phaseName,currentPhaseStatistics);
        }

        public void RegisterCurrentPhase()
        {
            if (currentPhaseStatistics.SpellGet())
            {
                currentPhaseStatistics.IncreaseSpellGetCount();
            }
            if (outcome.ContainsKey(currentPhaseName))
            {
                outcome[currentPhaseName] = currentPhaseStatistics;
            }
            else
            {
                outcome.Add(currentPhaseName,currentPhaseStatistics);
            }
            Debug.Log(currentPhaseName+"      " +GetStatistics(currentPhaseName));
        }

        public PhaseStatistics GetStatistics(string phaseName)
        {
            return outcome[phaseName];
        }

        private void RecordSpellUse()
        {
            currentPhaseStatistics.RecordSpellUse();
        }
        
        private void RecordHealthLost()
        {
            Debug.Log("HP lost recorded");
            currentPhaseStatistics.RecordHealthLost();
        }

        public bool AllSpellGet()
        {
            var phases = outcome.Keys;
            return phases.All(phase => outcome[phase].SpellGet());
        }
        
        
        
    }
}