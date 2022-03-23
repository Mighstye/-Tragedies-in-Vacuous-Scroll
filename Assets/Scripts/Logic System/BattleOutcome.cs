using System;
using System.Collections.Generic;
using System.Linq;
using Control;
using UnityEngine;

namespace Logic_System
{
    public class BattleOutcome : MonoBehaviour
    {
        public Action<string, PhaseStatistics> onPhaseEnd;

        public Action<string, PhaseStatistics> onPhaseStart;
        private Spell spellRef;
        public Dictionary<string, PhaseStatistics> outcome { get; private set; }
        public string currentPhaseName { get; private set; }
        public PhaseStatistics currentPhaseStatistics { get; private set; }

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
            if (phaseName is not null)
            {
                currentPhaseStatistics = new PhaseStatistics();
                if (outcome.ContainsKey(phaseName)) currentPhaseStatistics = outcome[phaseName];
                currentPhaseStatistics.IncreaseEncounterCount();
            }

            onPhaseStart?.Invoke(phaseName, currentPhaseStatistics);
        }

        public void RegisterCurrentPhase()
        {
            if (currentPhaseName is null) return;
            if (currentPhaseStatistics.SpellGet()) currentPhaseStatistics.IncreaseSpellGetCount();
            if (outcome.ContainsKey(currentPhaseName))
                outcome[currentPhaseName] = currentPhaseStatistics;
            else
                outcome.Add(currentPhaseName, currentPhaseStatistics);
            Debug.Log(currentPhaseName + "      " + GetStatistics(currentPhaseName));
            onPhaseEnd?.Invoke(currentPhaseName, currentPhaseStatistics);
        }

        public PhaseStatistics GetStatistics(string phaseName)
        {
            return outcome[phaseName];
        }

        public Dictionary<string, PhaseStatistics> GetAllStatistics()
        {
            return outcome;
        }

        private void RecordSpellUse()
        {
            if (currentPhaseName is null) return;
            currentPhaseStatistics.RecordSpellUse();
        }

        private void RecordHealthLost()
        {
            if (currentPhaseName is null) return;
            currentPhaseStatistics.RecordHealthLost();
        }

        public bool AllSpellGet()
        {
            var phases = outcome.Keys;
            return phases.All(phase => outcome[phase].SpellGet());
        }
    }
}