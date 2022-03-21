using System;
using BossBehaviour;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PhaseHP : MonoBehaviour
    {

        [SerializeField] private float targetFillRate = 1;
        [SerializeField] private float delayedFillSpeed = 0.01f;
        
       public static PhaseHP instance { get; private set; }
       private Image gauge;
       private void Awake()
       {
           instance ??= this;
           if (instance != this)
           {
               Destroy(gameObject);
               return;
           }
           DontDestroyOnLoad(gameObject);
       }

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