using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using UnityEngine;

namespace HyperCasual.Gameplay
{
    /// <summary>
    /// The event is triggered when the player collides with an obstacle.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ObstacleHitEvent),
        menuName = "Runner/" + nameof(ObstacleHitEvent))]

    public class ObstacleHitEvent : AbstractGameEvent
    {
        [HideInInspector]
        public int Count = -1;

        public override void Reset()
        {
            Count = -1;
        }
    }
}
