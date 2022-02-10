using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeScript : MonoBehaviour
{
    private TextMeshProUGUI timevalue;
    private TextMeshProUGUI targetTimeValue;
    private int time;
    public int targetTime;
    private string timeText;
    private string targetTimeText;

    // Start is called before the first frame update
    void Start()
    {
        //Demo mode :
        if (targetTime > 99999) targetTime = 99999;
        time = 0;

        timevalue = GameObject.Find("TimeValue").GetComponent<TextMeshProUGUI>();
        targetTimeValue = GameObject.Find("TargetTimeValue").GetComponent<TextMeshProUGUI>();
    }

    private void refresh()
    {
        timeText = string.Format("{0:D5}", time);
        targetTimeText = string.Format("{0:D5}", targetTime);
        timevalue.text = timeText;
        targetTimeValue.text = "/" + targetTimeText;
    }

    // Update is called once per frame
    void Update()
    {
        //Demo mode
        if(time >= targetTime)
        {
            time = 0;
        }
        else
        {
            time++;
        }
        refresh();
    }
}
