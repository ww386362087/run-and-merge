using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Coins_parent : MonoBehaviour
{
    public TextMeshProUGUI txtCoin;

    void LateUpdate()
    {
        txtCoin.text = PlayerPrefs.GetInt("coin",0).ToString("00");
    }
}
