using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{

    [SerializeField] private SceneData sceneData;
    public Animator BubbleAnimator;
    private bool StartBubble = true;

    void Update()
    {
        if (StartBubble)
        {
            BubbleAnimator.SetTrigger("Enter");
            StartBubble = false;
        }
        else
        {
            BubbleAnimator.SetTrigger("Exit");
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void BuyItem1()
    {
        if (sceneData.Bourse >= sceneData.Price[0])
        {
            sceneData.Bourse = sceneData.Bourse - sceneData.Price[0];
            sceneData.ActiveItems.Add("Item1");
        }
    }

        public void BuyItem2()
    {
        if (sceneData.Bourse >= sceneData.Price[1])
        {
            sceneData.Bourse = sceneData.Bourse - sceneData.Price[1];
            sceneData.ActiveItems.Add("Item2");
        }
    }

        public void BuyItem3()
    {
        if (sceneData.Bourse >= sceneData.Price[2])
        {
            sceneData.Bourse = sceneData.Bourse - sceneData.Price[2];
            sceneData.ActiveItems.Add("Item3");
        }
    }

        public void BuyItem4()
    {
        if (sceneData.Bourse >= sceneData.Price[3])
        {
            sceneData.Bourse = sceneData.Bourse - sceneData.Price[3];
            sceneData.ActiveItems.Add("Item4");
        }
    }

        public void BuyItem5()
    {
        if (sceneData.Bourse >= sceneData.Price[4])
        {
            sceneData.Bourse = sceneData.Bourse - sceneData.Price[4];
            sceneData.ActiveItems.Add("Item5");
        }
    }

        public void BuyItem6()
    {
        if (sceneData.Bourse >= sceneData.Price[5])
        {
            sceneData.Bourse = sceneData.Bourse - sceneData.Price[5];
            sceneData.ActiveItems.Add("Item6");
        }
    }
}
