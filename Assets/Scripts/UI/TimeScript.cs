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
        time = (int)(timerReff.currentPhaseTimeout * 1000 - timerReff.phaseTimer * 1000);
        refresh();
    }

    private void refreshTarget()
    {
        targetTime = (int)timerReff.currentPhaseTimeout * 1000;
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
        timeText = string.Format("{0:D6}", time);
        targetTimeText = string.Format("{0:D6}", targetTime);
        timevalue.text = timeText;
        targetTimeValue.text = "/" + targetTimeText;
    }
}
