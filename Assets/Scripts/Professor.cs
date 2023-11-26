using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Professor : MonoBehaviour
{
    [SerializeField] GameObject awakeProfessor;
    [SerializeField] GameObject asleepProfessor;

    private bool isAwake;

    public bool GetIsAwake()
    {
        return isAwake;
    }

    public void SetIsAwake(bool newIsAwake)
    {
        isAwake = newIsAwake;
        UpdateProfessorAwakeAsleep();
    }

    private void UpdateProfessorAwakeAsleep()
    {
        if(isAwake == true)
        {
            awakeProfessor.SetActive(true);
            asleepProfessor.SetActive(false);
        }
        else 
        {
            awakeProfessor.SetActive(false);
            asleepProfessor.SetActive(true);
        }
    }
}
