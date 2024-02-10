using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerPanel;
    [SerializeField] private float startTime;

    [SerializeField] private GameObject needle;
    [SerializeField] private Image fillCircle;
    private float remainingTime;

    [SerializeField] private GameObject clock;


    // Start is called before the first frame update
    void Start()
    {
        remainingTime = startTime;
        //UpdateTimerPanel();

        fillCircle.fillAmount = 0.0f;
        needle.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        if(remainingTime < 0)
        {
            ClickerEventSystem.EnterEndGame();
        }
        //UpdateTimerPanel();
        UpdateTimerClock();
    }

    private void UpdateTimerPanel()
    {
        timerPanel.text = remainingTime.ToString("F2");
    }


    private void UpdateTimerClock()
    {
        float progression = (startTime - remainingTime) / startTime;

        fillCircle.fillAmount = progression;
        if (progression < 0.999f)
        {
            needle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -360 * progression));
        }
        else
        {
            needle.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }


    public void SetStartTimer(float newTimer)
    {
        startTime = newTimer;
        UpdateTimerPanel();
    }
}
