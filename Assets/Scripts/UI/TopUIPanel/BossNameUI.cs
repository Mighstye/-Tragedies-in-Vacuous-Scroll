using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace UI.TopUIPanel
{
    public class BossNameUI : MonoBehaviour
    {
        [SerializeField] private LocalizedString localizedBossName;
        public string currentBossName = ""; //Test field
        private TextMeshProUGUI nameLabel;

        private void Start()
        {
            localizedBossName.TableReference = "Character Names";
            nameLabel = GetComponent<TextMeshProUGUI>();
            UpdateBossName(currentBossName); //Test
        }

        public void UpdateBossName(string bossName)
        {
            localizedBossName.TableEntryReference = bossName;
            StartCoroutine(Localize());
        }

        private IEnumerator Localize()
        {
            var localizedString = localizedBossName.GetLocalizedStringAsync();
            yield return localizedString;
            if (localizedString.IsDone) nameLabel.text = localizedString.Result;
        }
    }
}