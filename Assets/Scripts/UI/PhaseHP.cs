using System;
using BossBehaviour;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class PhaseHP : Singleton<PhaseHP>
    {

        [SerializeField] private float targetFillRate = 1;
        [SerializeField] private float delayedFillSpeed = 0.01f;
        
       private Image gauge;

       private void Start()
       {
           gauge = GetComponent<Image>();
       }

       public void SetGaugeFill(float fillAmount)
       {
           if (fillAmount is < 0 or > 1)
           {
               Debug.LogWarning("Phase fill amount out of bound [0,1]");
               return;
           }

           targetFillRate = fillAmount;
       }


       private void Update()
       {
           transform.position = BossBehaviourSystemProxy.instance.bossController.transform.position;
           if (gauge.fillAmount > targetFillRate)
           {
               gauge.fillAmount -= delayedFillSpeed;
           }

           if (gauge.fillAmount < targetFillRate)
           {
               gauge.fillAmount += delayedFillSpeed;
           }
       }
    }
}