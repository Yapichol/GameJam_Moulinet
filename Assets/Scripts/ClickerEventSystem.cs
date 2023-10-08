using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClickerEventSystem
{ 
    public static event Action TriggerEndGame;
    public static void EnterEndGame()
    {
        TriggerEndGame();
    }
    
}
