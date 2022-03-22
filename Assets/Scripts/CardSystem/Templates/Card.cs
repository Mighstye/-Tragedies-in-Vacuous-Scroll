using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;

namespace CardSystem
{
    public class Card: MonoBehaviour
    {
        [SerializeField] public CardCommonMetadata commonMetadata;
        [SerializeField] private LocalizedAsset<CardLocalizableMetadata> localizableMetadata;
        public CardLocalizableMetadata localizedMetadata { get; private set; }
        public IEnumerator LocalizeMetadata()
        {
            var loc = localizableMetadata.LoadAssetAsync();
            yield return loc;
            if (loc.IsDone)
            {
                localizedMetadata = loc.Result;
            }
        }
    }
}