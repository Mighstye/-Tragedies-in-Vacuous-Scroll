using System;
using CardSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.SelectedCardHover
{
    public class CoolDownUI : MonoBehaviour
    {
        [SerializeField] public Image brightnessMask;

        private Image coolDownGauge;
        [NonSerialized] public ActiveCard trackedCard;

        private void Awake()
        {
            coolDownGauge = GetComponent<Image>();
        }

        private void Update()
        {
            coolDownGauge.fillAmount = trackedCard.coolDownProgress;
            brightnessMask.gameObject.SetActive(trackedCard.coolDownProgress > 0);
        }
    }
}