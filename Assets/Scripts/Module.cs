using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Module 
{

    public static event Action Event_StartGame;
    public static void Action_StartGame()
    {
        if (Event_StartGame!=null)
        {
            Event_StartGame();
        }
     
    }


    public static bool isGodMod = false;
}
