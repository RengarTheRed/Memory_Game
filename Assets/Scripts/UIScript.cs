using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public float timerDuration = 60f;
    public TMP_Text timerText;
    
    private bool bTimerActive = true;
    private float currTimer = 0f;

    void Start()
    {
        currTimer = timerDuration;
    }
    void Update()
    {
        if (bTimerActive)
        {
            currTimer -= Time.deltaTime;
            if (currTimer < 0)
            {
                bTimerActive = false;
                TimerEnded();
            }
            timerText.SetText(currTimer.ToString("n2"));
        }
    }

    void TimerEnded()
    {
        Debug.Log("Timer ended");
    }
}
