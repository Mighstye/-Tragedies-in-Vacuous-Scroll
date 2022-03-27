using BossBehaviour;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class BackGround : Singleton<BackGround>
    {
        [SerializeField] private Image backGroundImage;

        public void UpdateBackGround(BossAsset asset)
        {
            backGroundImage.sprite = asset.background;
        }
    }
}