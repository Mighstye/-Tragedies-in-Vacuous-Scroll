using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Control;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using Utils;

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
        private readonly List<string> activeKeys = new List<string>();

        private string saveLoc;
        private void Start()
        {
            saveLoc = Application.persistentDataPath + "/PhaseStats.json";
            outcome = Load();
            spellRef = LogicSystemAPI.instance.spell;
            spellRef.onSpellUse += RecordSpellUse;
            YoumuController.instance.onYoumuHit += RecordHealthLost;
        }

        public void RecordNewPhase(string phaseName)
        {
            currentPhaseName = phaseName;
            if (phaseName is not null)
            {
                currentPhaseStatistics = new PhaseStatistics { phaseID = phaseName };
                if (outcome.ContainsKey(phaseName)) currentPhaseStatistics = outcome[phaseName];
                currentPhaseStatistics.IncreaseEncounterCount();
            }

            onPhaseStart?.Invoke(phaseName, currentPhaseStatistics);
        }

        public void RegisterCurrentPhase()
        {
            if (currentPhaseName is null) return;
            activeKeys.Add(currentPhaseName);
            if (currentPhaseStatistics.SpellGet()) currentPhaseStatistics.IncreaseSpellGetCount();
            if (outcome.ContainsKey(currentPhaseName))
                outcome[currentPhaseName] = currentPhaseStatistics;
            else
                outcome.Add(currentPhaseName, currentPhaseStatistics);
            Debug.Log(currentPhaseName + "      " + GetStatistics(currentPhaseName));
            onPhaseEnd?.Invoke(currentPhaseName, currentPhaseStatistics);
            Save();
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

        public void ClearVerificationScope()
        {
            activeKeys.Clear();
        }

        public int SpellGetCount()
        {
            return activeKeys.Sum(phase => outcome[phase].SpellGet() ? 1 : 0);
        }

        public int SpellUseCount()
        {
            return activeKeys.Sum(phase => outcome[phase].spellUse ? 1 : 0);
        }

        public int HitCount()
        {
            return activeKeys.Sum(phase => outcome[phase].hit ? 1 : 0);
        }

        #region JSON
        
        private void Save()
        {
            var json = JsonHelper.ToJson(outcome.Values.ToArray());
            Debug.Log(json);
            File.WriteAllText(Application.persistentDataPath + "/PhaseStats.json", json);
            Debug.Log(Application.persistentDataPath);
        }

        private Dictionary<string,PhaseStatistics> Load()
        {
            var dic = new Dictionary<string, PhaseStatistics>();
            var val= File.Exists(saveLoc) ? 
                JsonHelper.FromJson<PhaseStatistics>(File.ReadAllText(saveLoc)).ToList() : null;
            if (val is null) return dic;
            foreach (var stat in val)
            {
                dic.Add(stat.phaseID,stat);
            }
            return dic;
        }
        
        #endregion
    }
}