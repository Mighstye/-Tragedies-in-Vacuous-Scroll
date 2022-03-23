using System;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Utils
{
    [Obsolete]
    public class LocalizationUtil : MonoBehaviour
    {
        [SerializeField] private LocalizedStringTable localizedStringTable;

        public string GetPhaseLocalizedString(string phaseKey)
        {
            var str = "";
            var op = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Phase Names", phaseKey);
            if (op.IsDone) return op.Result;
            op.Completed += o => str = o.Result;

            return str;
        }
    }
}