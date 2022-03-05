using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BossBehaviour;

public class TimeScript : MonoBehaviour
{
    public TextMeshProUGUI timevalue;
    public TextMeshProUGUI targetTimeValue;
    private int targetTime;
    private int time;
    private string timeText;
    private string targetTimeText;
    private PhaseTimer timerReff;

    // Start is called before the first frame update
    void Start()
    {
        targetTime = 0;
        time = 0;

        timerReff = PhaseTimer.instance;

        timerReff.onNeedTimeoutRefreshDisplay += () =>
        {
            refreshTarget();
        };

        timerReff.onNeedTimerRefreshDisplay += () =>
        {
            refreshTimer();
        };

        timerReff.onPhaseEndDisplay += () =>
        {
            endTimer();
        };
    }

    private void refreshTimer()
    {
        time = (int)(timerReff.currentPhaseTimeout - timerReff.phaseTimer);
        refresh();
    }

    private void refreshTarget()
    {
        targetTime = (int)timerReff.currentPhaseTimeout;
        refresh();
    }

    private void endTimer()
    {
        time = 0;
        targetTime = 0;
        refresh();
    }

    private void refresh()
    {
        timeText = string.Format("{0:D5}", time);
        targetTimeText = string.Format("{0:D5}", targetTime);
        timevalue.text = timeText;
        targetTimeValue.text = "/" + targetTimeText;
    }
}
