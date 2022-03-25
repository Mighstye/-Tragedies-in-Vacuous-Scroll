using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class BackGround : Singleton<BackGround>
    {
        [SerializeField] private Image backGroundImage;

        public void UpdateBackGround(Sprite sprite)
        {
            backGroundImage.sprite = sprite;
        }
    }
}