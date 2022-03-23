using AirFishLab.ScrollingList;
using CardSystem;
using UnityEngine.UI;

namespace Utils
{
    public class CircularCardListItem : ListBox
    {
        private Image image;

        protected override void UpdateDisplayContent(object content)
        {
            image = GetComponent<Image>();
            var asset = (Card)content;
            image.sprite = asset.commonMetadata.cardSprite;
        }
    }
}