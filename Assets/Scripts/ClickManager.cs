using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickManager : MonoBehaviour
{

    private float score;
    //[SerializeField] private TextMeshProUGUI scorePanel;
    [SerializeField] private NoteSpriteManager noteScore;
    [SerializeField] private float clickValue;
    private bool canClick;
    [SerializeField] private GameObject clickEffectPlus;

    private bool professorLooking = false;

    [SerializeField] private List<SpriteRenderer> losingEffect;
    [SerializeField] private float losingEffectDelay;
    [SerializeField] private float losingEachEffectDelay;
    private bool losingEffectActivated;

    [SerializeField] private SceneData sceneData;

    [SerializeField] private GameObject characterArm;
    [SerializeField] private SpriteRenderer phoneArm;
    [SerializeField] private float phoneOutTime;
    private bool phoneOut;

    [SerializeField] private Transform armMovePoints;
    [SerializeField] private Vector2 armMoveAmplitude;


    void Start()
    {
        score = 0;
        UpdateScorePanel();
        losingEffectActivated = false;
        phoneOut = false;
        phoneArm.enabled = false;
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
            MoveArm();

            if (phoneOut == false)
            {
                StartCoroutine(UsePhone(phoneOutTime));
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


    private void MoveArm()
    {
        characterArm.transform.position = new Vector3(armMovePoints.position.x + Random.Range(-armMoveAmplitude.x, armMoveAmplitude.x), armMovePoints.position.y + Random.Range(-armMoveAmplitude.y, armMoveAmplitude.y), armMovePoints.position.z);
    }


    private IEnumerator UsePhone(float delay)
    {
        phoneOut = true;
        phoneArm.enabled = true;
        float timer = 0;
        while (timer < delay)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        phoneArm.enabled = false;
        phoneOut = false;
    }

    private void UpdateScorePanel()
    {
        //scorePanel.text = score.ToString("F2");
        noteScore.UpdateScore(score);
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
