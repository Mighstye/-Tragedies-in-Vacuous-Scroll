using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrazeScript : MonoBehaviour
{

    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = Graze.instance.get();

        Graze.instance.onNeedGrazeRefresh += () =>
        {
            refreshDisplay();
        };
    }

    private void refreshDisplay()
    {
        slider.value = Graze.instance.get();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
