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
    [SerializeField] private GameObject clickEffectPlus;
    [SerializeField] private SceneData sceneData;

    void Start()
    {
        score = 0;
        UpdateScorePanel();
    }

    public void ClickAction()
    {
        if (canClick)
        {
            score = score + clickValue;
            MakeSpawnClickEffect();
            UpdateScorePanel();
        }
    }

    private void MakeSpawnClickEffect()
    {
        GameObject newEffect = Instantiate(clickEffectPlus);
        Vector3 v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        newEffect.transform.position = v3;

    }

    private void UpdateScorePanel()
    {
        scorePanel.text = score.ToString("F2");
        sceneData.Score = score;
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
