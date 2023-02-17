using System.Collections;
using System.Collections.Generic;
using HyperCasual.Runner;
using UnityEngine;

namespace HyperCasual.Gameplay
{
    /// <summary>
    /// Fires an ObstacleHitEvent when the player enters the trigger collider attached to this game object.
    /// </summary>
    public class ObstacleHitTrigger : Spawnable
    {
        /// <summary>
        /// Player tag
        /// </summary>
        public string m_PlayerTag = "Player";

        /// <summary>
        /// The event to raise on trigger
        /// </summary>
        public ObstacleHitEvent m_Event;

        void OnTriggerEnter(Collider col)
        {
            if (!col.CompareTag(m_PlayerTag))
                return;

            //m_Event.Count = m_Count;
            m_Event.Raise();
            //gameObject.SetActive(false);
        }
    }
}
