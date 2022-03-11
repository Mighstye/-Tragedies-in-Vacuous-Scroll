using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Logic_System;
using Control;

public class GrazeScript : MonoBehaviour
{
    private Image graze;

    private Graze grazeRef;

    // Start is called before the first frame update
    void Start()
    {
        grazeRef = LogicSystemAPI.instance.Graze;

        graze = gameObject.GetComponent<Image>();
        graze.fillAmount = (float)grazeRef.get() / 100;

        grazeRef.onNeedGrazeRefresh += RefreshDisplay;
    }

    private void RefreshDisplay()
    {
        graze.fillAmount = (float)grazeRef.get() / 100;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = YoumuController.instance.transform.position;
    }
}
