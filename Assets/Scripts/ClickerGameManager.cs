using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickerGameManager : MonoBehaviour
{
    [SerializeField] private TimerManager timeManager;
    [SerializeField] private ClickManager clickManager;

    [SerializeField] private List<SpriteRenderer> warningProfessor;
    [SerializeField] private Vector2 professorNotLookingDelay;
    [SerializeField] private Vector2 professorLookingDelay;
    [SerializeField] private Vector2 professorWarningDelay;

    private bool professorCanLook;

    [SerializeField] private List<SpriteRenderer> boardsNames;
    private SpriteRenderer currentBoardName;

    


    // Start is called before the first frame update
    void Start()
    {
        ClickerEventSystem.TriggerEndGame += StopClickSession;
        HideBoardsName();
        currentBoardName = ChooseRandomName();
        currentBoardName.enabled = true;
        StartClickSession();
        ShowWarningProfessorLooking(-1);
        professorCanLook = true;
    }


    private SpriteRenderer ChooseRandomName()
    {
        return boardsNames[Random.Range(0, boardsNames.Count)];
    }

    private void HideBoardsName()
    {
        foreach (SpriteRenderer name in boardsNames)
        {
            name.enabled = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (professorCanLook)
        {
            professorCanLook = false;
            StartCoroutine(DelayBeforeProfessorLook(Random.Range(professorNotLookingDelay.x, professorNotLookingDelay.y)));
        }
    }

    public void StartClickSession()
    {
        clickManager.ActiveClick();
    }

    public void StopClickSession()
    {
        clickManager.DeactiveClick();
        timeManager.SetStartTimer(0f);
        timeManager.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



    private IEnumerator DelayBeforeProfessorLook(float delay)
    {
        float timer = 0;
        while(timer < delay)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(WarningProfessorLooking(Random.Range(professorWarningDelay.x, professorWarningDelay.y)));
    }


    private IEnumerator WarningProfessorLooking(float delay)
    {
        int stepCounter = 0;
        float timer = 0;
        ShowWarningProfessorLooking(stepCounter);
        while (timer <= delay)
        {

            if (timer > (stepCounter + 1) * (delay / 4))
            {
                stepCounter++;
                ShowWarningProfessorLooking(stepCounter);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        ShowWarningProfessorLooking(-1);

        StartCoroutine(ProfessorLooking(Random.Range(professorLookingDelay.x, professorLookingDelay.y)));
    }


    private void ShowWarningProfessorLooking(int warningId)
    {
        if(warningId > 0)
        {
            warningProfessor[warningId].enabled = true;
            warningProfessor[warningId - 1].enabled = false;
        }
        else if (warningId == 0)
        {
            warningProfessor[warningId].enabled = true;
        }
        else
        {
            foreach(SpriteRenderer warning in warningProfessor)
            {
                warning.enabled = false;
            }
        }
    }


    private IEnumerator ProfessorLooking(float delay)
    {
        clickManager.SetProfessorLooking(true);
        float timer = 0;

        while(timer < delay)
        {

            timer += Time.deltaTime;
            yield return null;
        }

        clickManager.SetProfessorLooking(false);
        professorCanLook = true;
    }




    private void OnDesable()
    {
        ClickerEventSystem.TriggerEndGame -= StopClickSession;
    }
}
