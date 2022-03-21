using System.Collections;
using AirFishLab.ScrollingList;
using CardSystem;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CardDetailUI : MonoBehaviour
    {
        [SerializeField] private CircularScrollingList scrollingList;
        [SerializeField] private GeneralCounterUI costUI;
        [SerializeField] private TextMeshProUGUI cardName;
        [SerializeField] private TextMeshProUGUI effectTextField;
        [SerializeField] private TextMeshProUGUI loreTextField;

        
        public void OnListCenteredContentChanged(int centeredContentID)
        {
            var content = (Card)scrollingList.listBank.GetListContent(centeredContentID);
            StartCoroutine(UpdateContent(content));
        }

        private IEnumerator UpdateContent(Card card)
        {
            yield return StartCoroutine(card.LocalizeMetadata());
            var meta = card.localizedMetadata;
            costUI.UpdateIcons(meta.cost);
            cardName.text = meta.cardName;
            effectTextField.text = meta.effectText;
            loreTextField.text = meta.loreText;
        }
    }
}