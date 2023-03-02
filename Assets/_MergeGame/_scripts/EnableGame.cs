using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGame : MonoBehaviour
{
    private void OnEnable()
    {
        if(GameController.Instance)
            GameController.Instance.game_play = false;
    }
}
