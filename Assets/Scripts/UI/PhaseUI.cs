using System;
using System.Collections;
using Logic_System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace UI
{
    public class PhaseUI : MonoBehaviour
    {
        [SerializeField] private BattleOutcome battleOutcomeRef;
        [SerializeField] private TextMeshProUGUI phaseNameText;
        [SerializeField] private TextMeshProUGUI historyText;
        [SerializeField] private LocalizedString localizedPhaseName;


        private void Start()
        {
            battleOutcomeRef = LogicSystemAPI.instance.battleOutcome;
            battleOutcomeRef.onPhaseStart += UpdatePhaseUI;
            localizedPhaseName.TableReference = "Phase Names";
            localizedPhaseName.StringChanged += UpdatePhaseName;
        }

        private void UpdatePhaseUI(string phaseName, PhaseStatistics statistics)
        {
            if (phaseName is null)
            {
                SetEmpty();
                return;
            }
            localizedPhaseName.TableEntryReference = phaseName;
            StartCoroutine(Localize());
            historyText.text = statistics.spellGetCount + "/" + statistics.encounterCount;
        }

        private void UpdatePhaseName(string s)
        {
            phaseNameText.text = s;
        }

        private void SetEmpty()
        {
            historyText.text = "--/--";
            phaseNameText.text = "";
        }

        private IEnumerator Localize()
        {
            var localizedString = localizedPhaseName.GetLocalizedStringAsync();
            yield return localizedString;
            if(localizedString.IsDone)UpdatePhaseName(localizedString.Result);
        }
    }
}