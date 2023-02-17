using System.Collections;
using System.Collections.Generic;
using HyperCasual.Gameplay;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A class representing a Spawnable object.
    /// If a GameObject tagged "Player" collides
    /// with this object, the "Player" GameObject
    /// will be destroyed.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Collider))]
    public class Obstacle : Spawnable
    {
        [SerializeField]
        SoundID m_Sound = SoundID.None;

        const string k_PlayerTag = "Player";

        public ObstacleHitEvent m_Event;

        Renderer[] m_Renderers;

        /// <summary>
        /// Reset the Obstacle to its initial state. Called
        /// when a level is restarted by the GameManager.
        /// </summary>
        public override void ResetSpawnable()
        {
            for (int i = 0; i < m_Renderers.Length; i++)
            {
                m_Renderers[i].enabled = true;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            m_Renderers = gameObject.GetComponents<Renderer>();
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag(k_PlayerTag))
            {
                Collide();
            }
        }

        void Collide()
        {
            if (m_Event != null)
            {
                m_Event.Raise();
            }

            PlayerController.Instance.AdjustHealth(-1);

            if (PlayerController.Instance.Health < 1)
            {
                GameManager.Instance.Lose();
            }

            AudioManager.Instance.PlayEffect(m_Sound);
        }
    }
}