using BossBehaviour;
using TMPro;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    public TextMeshProUGUI timeValue;
    public TextMeshProUGUI fractionalValue;
    private Animation anim;
    private float targetTime;
    private float time;
    private PhaseTimer timerRef;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animation>();
        targetTime = 0;
        time = 0;

        timerRef = PhaseTimer.instance;

        timerRef.onNeedTimeoutRefreshDisplay += RefreshTarget;

        timerRef.onNeedTimerRefreshDisplay += RefreshTimer;

        timerRef.onPhaseEndDisplay += EndTimer;
    }

    private void RefreshTimer()
    {
        time = timerRef.phaseTimer;
        Refresh();
    }

    private void RefreshTarget()
    {
        anim.Play("TimerAnim");
        targetTime = timerRef.currentPhaseTimeout;
        Refresh();
    }

    private void EndTimer()
    {
        time = 0;
        targetTime = 0;
        Refresh();
    }

    private void Refresh()
    {
        var decPart = (int)time;
        var fractionalPart = (int)((time - decPart) * 100);
        if (decPart == 3) anim.Play("TimerAnimRev");
        timeValue.text = decPart.ToString("00") + ".";
        fractionalValue.text = fractionalPart.ToString("00");
    }
}