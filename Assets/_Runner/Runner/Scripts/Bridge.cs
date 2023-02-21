using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasual.Runner
{
    public class Bridge : Spawnable, IBridge
    {
        [SerializeField] float LengthBridge;

        public float Length
        {
            get
            {
                return LengthBridge;
            }
        }

        /*Renderer[] m_Renderers;

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
        }*/
    }
}
