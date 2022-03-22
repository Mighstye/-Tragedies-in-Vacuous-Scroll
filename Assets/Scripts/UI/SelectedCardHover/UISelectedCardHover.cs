using CardSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.SelectedCardHover
{
    public class UISelectedCardHover : MonoBehaviour
    {
        private Image cardImage;
        private int maxFill;
        private void Awake()
        {
            cardImage = GetComponent<Image>();
        }

        public void Fade()
        {
            var color = cardImage.color;
            color=new Color(color.r,color.g,color.b,0.35f);
            cardImage.color = color;
        }

        public void Restore()
        {
            var color = cardImage.color;
            color=new Color(color.r,color.g,color.b,1.0f);
            cardImage.color = color;
        }

        public void Charge(float currentGrazeSeg)
        {
            var color = cardImage.color;
            var amount = Mathf.Clamp(currentGrazeSeg / maxFill, 0f, 1f);
            color = amount >= 1 ? new Color(255, 255, 255, color.a) : new Color(255,0,0,color.a);
            cardImage.fillAmount = amount;
            cardImage.color = color;
        }

        public void UpdateSelectedCard(ActiveCard activeCard)
        {
            cardImage.sprite = activeCard.gameObject.GetComponent<Image>().sprite;
            maxFill = activeCard.grazeCostSegment;
        }

    }
}
