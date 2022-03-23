using Control;
using Logic_System;
using UnityEngine;
using UnityEngine.UI;

public class GrazeScript : MonoBehaviour
{
    private Image graze;

    private Graze grazeRef;

    // Start is called before the first frame update
    private void Start()
    {
        grazeRef = LogicSystemAPI.instance.graze;

        graze = gameObject.GetComponent<Image>();
        graze.fillAmount = (float)grazeRef.get() / 100;

        grazeRef.onNeedGrazeRefresh += RefreshDisplay;
    }

    // Update is called once per frame
    private void Update()
    {
        gameObject.transform.position = YoumuController.instance.transform.position;
    }

    private void RefreshDisplay()
    {
        graze.fillAmount = (float)grazeRef.get() / 100;
    }
}