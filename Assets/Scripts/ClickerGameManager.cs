using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerGameManager : MonoBehaviour
{
    [SerializeField] private TimerManager timeManager;
    [SerializeField] private ClickManager clickManager;


    // Start is called before the first frame update
    void Start()
    {

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
    }
}
