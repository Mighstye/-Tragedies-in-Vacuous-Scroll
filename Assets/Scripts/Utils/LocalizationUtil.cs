using System;
using System.Collections;
using Ink.Parsed;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

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
            else
            {
                op.Completed += (o) => str = o.Result;
            }

            return str;
        }
    }
        
        
}
