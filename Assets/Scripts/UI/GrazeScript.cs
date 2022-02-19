using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Logic_System;

public class GrazeScript : MonoBehaviour
{

    private Slider slider;

    private Graze grazeRef;

    // Start is called before the first frame update
    void Start()
    {
        grazeRef = LogicSystemAPI.instance.Graze;

        slider = gameObject.GetComponent<Slider>();
        slider.value = grazeRef.get();

        grazeRef.onNeedGrazeRefresh += () =>
        {
            refreshDisplay();
        };
    }

    private void refreshDisplay()
    {
        slider.value = grazeRef.get();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
