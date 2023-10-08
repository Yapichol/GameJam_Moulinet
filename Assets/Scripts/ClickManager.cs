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

    private bool professorLooking = false;

    [SerializeField] private List<SpriteRenderer> losingEffect;
    [SerializeField] private float losingEffectDelay;
    [SerializeField] private float losingEachEffectDelay;
    private bool losingEffectActivated;

    [SerializeField] private SceneData sceneData;


    void Start()
    {
        score = 0;
        UpdateScorePanel();
        losingEffectActivated = false;
        HideLosingVisualEffects();
    }

    public void ClickAction()
    {
        if (canClick)
        {
            if (professorLooking)
            {
                score = score - clickValue * 3;
                UpdateScorePanel();
                if (losingEffectActivated == false)
                {
                    StartCoroutine(LoosingEffect(losingEffectDelay));
                }
            }
            else
            {
                score = score + clickValue;
                MakeSpawnClickEffect();
                UpdateScorePanel();
            }
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


    public void SetProfessorLooking(bool looking)
    {
        professorLooking = looking;
    }


    private IEnumerator LoosingEffect(float delay)
    {
        losingEffectActivated = true;
        float timer = 0;
        float timerEffect = 0;
        int currentEffectIndex = 0;
        AppearLosingVisualEffect(currentEffectIndex);
        while (timer < delay)
        {
            timerEffect += Time.deltaTime;
            if (timerEffect > losingEachEffectDelay)
            {
                currentEffectIndex++;
                if (currentEffectIndex >= losingEffect.Count)
                {
                    currentEffectIndex = 0;
                }
                AppearLosingVisualEffect(currentEffectIndex);
                timerEffect = 0;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        HideLosingVisualEffects();
        losingEffectActivated = false;
    }


    private void AppearLosingVisualEffect(int effectId)
    {
        HideLosingVisualEffects();
        losingEffect[effectId].enabled = true;
    }


    private void HideLosingVisualEffects()
    {
        foreach (SpriteRenderer effect in losingEffect)
        {
            effect.enabled = false;
        }
    }



}
