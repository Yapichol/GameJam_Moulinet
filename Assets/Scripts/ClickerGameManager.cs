using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickerGameManager : MonoBehaviour
{
    [SerializeField] private TimerManager timeManager;
    [SerializeField] private ClickManager clickManager;

    [SerializeField] private Professor professor;
    [SerializeField] private List<SpriteRenderer> warningProfessor;
    [SerializeField] private Vector2 professorNotLookingDelay;
    [SerializeField] private Vector2 professorLookingDelay;
    [SerializeField] private Vector2 professorWarningDelay;

    private bool professorCanLook;

    [SerializeField] private List<SpriteRenderer> testsSubjectBoard;
    [SerializeField] private List<string> testsSubjects;
    private string currentTestSubjects;

    bool studentCanThink = false;
    [SerializeField] private Vector2 studentThinksDelay;
    [SerializeField] private float studentThinkTime;

    [SerializeField] private Bubble bubbleLeft;
    [SerializeField] private List<Transform> bubbleSpawnLeft;
    [SerializeField] private Bubble bubbleRight;
    [SerializeField] private List<Transform> bubbleSpawnRight;

    [SerializeField] private List<string> histoirePhrases;
    [SerializeField] private List<string> infoPhrases;
    [SerializeField] private List<string> francaisPhrases;
    [SerializeField] private List<string> mathsPhrases;
    [SerializeField] private List<string> anglaisPhrases;



    // Start is called before the first frame update
    void Start()
    {
        ClickerEventSystem.TriggerEndGame += StopClickSession;
        HideBoardsName();
        currentTestSubjects = ChooseRandomTestSubject();
        GetCurrentTestSubjectSpriteRenderer().enabled = true;
        StartClickSession();
        ShowWarningProfessorLooking(-1);
        professorCanLook = true;
        professor.SetIsAwake(false);
        studentCanThink = false;
        StartCoroutine(DelayBeforeNextStudentBubble(Random.Range(studentThinksDelay.x, studentThinksDelay.y)));
    }


    private string ChooseRandomTestSubject()
    {
        return testsSubjects[Random.Range(0, testsSubjects.Count)];
    }

    private void HideBoardsName()
    {
        foreach (SpriteRenderer sprite in testsSubjectBoard)
        {
            sprite.enabled = false;
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
        if (studentCanThink)
        {
            studentCanThink = false;
            ShowRandomPhrase();
            StartCoroutine(DelayBeforeNextStudentBubble(Random.Range(studentThinksDelay.x, studentThinksDelay.y)));
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
        OnDesable();
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
        professor.SetIsAwake(true);
        float timer = 0;

        while(timer < delay)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        clickManager.SetProfessorLooking(false);
        professor.SetIsAwake(false);
        professorCanLook = true;
    }



    private void ShowRandomPhrase()
    {
        float sideOfTheClassRoom = Random.Range(0.0f, 1.0f);
        List<string> subjectPhrases = GetCurrentTestSubjectPhrases();

        if(sideOfTheClassRoom > 0.5f)
        {
            // RIGHT
            if (subjectPhrases.Count > 0)
            {
                int randomIndex = Random.Range(0, subjectPhrases.Count);
                string newPhrase = subjectPhrases[randomIndex];

                randomIndex = Random.Range(0, bubbleSpawnLeft.Count);
                Bubble newBubble = Instantiate(bubbleLeft, bubbleSpawnLeft[randomIndex]);
                newBubble.SetText(newPhrase);
                StartCoroutine(DelayBeforeDeath(studentThinkTime, newBubble.gameObject));
            }

        }
        else
        {
            // LEFT
            if (subjectPhrases.Count > 0)
            {
                int randomIndex = Random.Range(0, subjectPhrases.Count);
                string newPhrase = subjectPhrases[randomIndex];

                randomIndex = Random.Range(0, bubbleSpawnRight.Count);
                Bubble newBubble = Instantiate(bubbleRight, bubbleSpawnRight[randomIndex]);
                newBubble.SetText(newPhrase);
                StartCoroutine(DelayBeforeDeath(studentThinkTime, newBubble.gameObject));
            }

        }
    }


    private List<string> GetCurrentTestSubjectPhrases()
    {
        if (currentTestSubjects == "anglais")
        {
            return anglaisPhrases;
        }
        else if (currentTestSubjects == "francais")
        {
            return francaisPhrases;
        }
        else if (currentTestSubjects == "histoire")
        {
            return histoirePhrases;
        }
        else if (currentTestSubjects == "informatique")
        {
            return infoPhrases;
        }
        else if (currentTestSubjects == "math")
        {
            return mathsPhrases;
        }
        return anglaisPhrases;
    }

    private SpriteRenderer GetCurrentTestSubjectSpriteRenderer()
    {
        if(currentTestSubjects == "anglais")
        {
            return testsSubjectBoard[0];
        }
        else if(currentTestSubjects == "francais")
        {
            return testsSubjectBoard[1];
        }
        else if (currentTestSubjects == "histoire")
        {
            return testsSubjectBoard[2];
        }
        else if (currentTestSubjects == "informatique")
        {
            return testsSubjectBoard[3];
        }
        else if (currentTestSubjects == "math")
        {
            return testsSubjectBoard[4];
        }
        return testsSubjectBoard[0];
    }


    private IEnumerator DelayBeforeNextStudentBubble(float delay)
    {
        float timer = 0;

        while (timer < delay)
        {

            timer += Time.deltaTime;
            yield return null;
        }

        studentCanThink = true;
    }

    private IEnumerator DelayBeforeDeath(float delay, GameObject gObject)
    {
        float timer = 0;

        while (timer < delay)
        {

            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gObject);
    }


    private void OnDesable()
    {
        ClickerEventSystem.TriggerEndGame -= StopClickSession;
    }
}
