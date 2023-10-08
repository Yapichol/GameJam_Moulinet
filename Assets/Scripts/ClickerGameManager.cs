using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickerGameManager : MonoBehaviour
{
    [SerializeField] private TimerManager timeManager;
    [SerializeField] private ClickManager clickManager;
    


    // Start is called before the first frame update
    void Start()
    {
        ClickerEventSystem.TriggerEndGame += StopClickSession;
        StartClickSession();
    }

    // Update is called once per frame
    void Update()
    {
        
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


    private void OnDesable()
    {
        ClickerEventSystem.TriggerEndGame -= StopClickSession;
    }
}
