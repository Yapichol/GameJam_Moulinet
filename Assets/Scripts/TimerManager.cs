using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerPanel;
    [SerializeField] private float startTime;
    private float remainingTime;

    [SerializeField] private GameObject clock;


    // Start is called before the first frame update
    void Start()
    {
        remainingTime = startTime;
        UpdateTimerPanel();
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        if(remainingTime < 0)
        {
            ClickerEventSystem.EnterEndGame();
        }
        UpdateTimerPanel();
    }

    private void UpdateTimerPanel()
    {
        timerPanel.text = remainingTime.ToString("F2");
    }


    public void SetStartTimer(float newTimer)
    {
        startTime = newTimer;
        UpdateTimerPanel();
    }
}
