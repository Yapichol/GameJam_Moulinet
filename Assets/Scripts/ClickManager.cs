using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickManager : MonoBehaviour
{

    private float score;
    [SerializeField] private TextMeshProUGUI scorePanel;
    [SerializeField] private float clickValue;
    private bool canClick;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScorePanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ClickAction()
    {
        if (canClick)
        {
            score = score + clickValue;

            UpdateScorePanel();
        }
    }

    private void UpdateScorePanel()
    {
        scorePanel.text = score.ToString("F2");
    }


    public void ActiveClick()
    {
        canClick = true;
    }

    public void DeactiveClick()
    {
        canClick = false;
    }
}
