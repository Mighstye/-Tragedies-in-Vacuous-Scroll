using System.Collections;
using UnityEngine;
using UnityEngine.Localization;

namespace CardSystem
{
    public class Card: MonoBehaviour
    {
        [SerializeField] private LocalizedAsset<CardMetadata> metadata;
        public CardMetadata localizedMetadata { get; private set; }
        public IEnumerator LocalizeMetadata()
        {
            var loc = metadata.LoadAssetAsync();
            yield return loc;
            if (loc.IsDone)
            {
                localizedMetadata = loc.Result;
            }
        }
    }
}