using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace CardSystem
{
    public class Card : MonoBehaviour
    {
        [SerializeField] public CardCommonMetadata commonMetadata;
        [SerializeField] private LocalizedAsset<CardLocalizableMetadata> localizableMetadata;
        public CardLocalizableMetadata localizedMetadata { get; private set; }

        private void Start()
        {
            if (commonMetadata.cardSprite is not null) GetComponent<Image>().sprite = commonMetadata.cardSprite;
        }

        public IEnumerator LocalizeMetadata()
        {
            var loc = localizableMetadata.LoadAssetAsync();
            yield return loc;
            if (loc.IsDone) localizedMetadata = loc.Result;
        }
    }
}