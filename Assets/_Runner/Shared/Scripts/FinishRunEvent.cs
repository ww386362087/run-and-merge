using HyperCasual.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HyperCasual.Gameplay
{
    [CreateAssetMenu(fileName = nameof(LevelCompletedEvent),
       menuName = "Runner/" + nameof(LevelCompletedEvent))]
    public class FinishRunEvent : AbstractGameEvent
    {
        public int NumberCharacterAdd;

        public override void Reset()
        {
        }
    }

}