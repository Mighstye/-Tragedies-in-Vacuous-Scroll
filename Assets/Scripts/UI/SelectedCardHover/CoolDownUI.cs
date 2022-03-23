using System;
using CardSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.SelectedCardHover
{
    public class CoolDownUI : MonoBehaviour
    {
        [NonSerialized]public ActiveCard trackedCard;

        private Image coolDownGauge;
        private void Awake()
        {
            coolDownGauge=GetComponent<Image>();
        }

        private void Update()
        {
            coolDownGauge.fillAmount = trackedCard.coolDownProgress;
        }
        
    }
}