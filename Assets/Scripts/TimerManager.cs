using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerPanel;
    [SerializeField] private float startTime;
    private float remainingTime;


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
        UpdateTimerPanel();
    }

    private void UpdateTimerPanel()
    {
        timerPanel.text = remainingTime.ToString("F2");
    }
}
