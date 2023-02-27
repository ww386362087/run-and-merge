using System.Collections;
using System.Collections.Generic;
using HyperCasual.Gameplay;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Prize Preset", menuName = "Runner/Prize")]
public class PrizeData : ScriptableObject
{
    public Sprite Sprite;
    public int Quantity;
}
