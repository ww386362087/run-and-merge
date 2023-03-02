using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using UnityEngine;
using System;


/// <summary>
/// The event is triggered when the player picks up an item (like coin and keys)
/// </summary>
[CreateAssetMenu(fileName = nameof(AdsEvent), menuName = "Runner/" + nameof(AdsEvent))]
public class AdsEvent : AbstractGameEvent
{
    public Action adsReward;
    public override void Reset()
    {
       
    }
}
